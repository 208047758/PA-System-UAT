using DataDLL.Sources.PASystem.Read;
using PA_System.Helper;
using PA_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Infrastructure
{
    public class ExcelFileUploadHelper
    {
        //The excel file is read from the provided file path, processes it and returns a list of all the valid questions that are in the spreadsheet
        public static List<QuestionnaireObject> ProcessFileUpload(string fileName, string fileExtension, string questionnairePeriodType, string questionnairePeriodValue)
        {
            //Store all the questions that are in the spreadsheet
            var questionList = new List<QuestionnaireObject>();

            var ratersInformation = new List<Rater>();            

            var conString = GetExcelConnectionString(fileExtension, fileName);

            //This ensures that only questions that have more than 3 raters are added proccessed and later saved into the database
            var minimumNumberOfRaters = 0;

            var excelSheetList = new List<string>();

            //Get employee information from AD, this is used to validate a sheet against empoloyee Names
            var ADEmpList = new Surveys().getSurveyTeamEmployeeList();

            var teamList = new Surveys().getSurveyTeams();

            using (OleDbConnection con = new OleDbConnection(conString))
            {
                try
                {
                    con.Open();

                    //Reading a spreadsheet sheets 
                    DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    
                  //Creating a datatable to store all the sheet names from survey excel spreadsheet
                  var  exceldt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (exceldt == null)
                    {
                        return null;
                    }

                    // Add the sheet name to the string array.
                    foreach (DataRow row in exceldt.Rows)
                    {
                        //Reading sheet names
                        var tempSheet = row["TABLE_NAME"].ToString();

                        if (tempSheet.ToString().Contains("$"))
                        {
                            tempSheet = tempSheet.ToString().Replace("'", "").Replace("$", "");

                            if (ADEmpList.Where(c => c.Employee.Trim().ToLower().Replace(" ", "") == tempSheet.Trim().ToLower().Replace(" ", "")).Count() > 0)
                            {
                                excelSheetList.Add(tempSheet);
                            }
                        }
                        
                    }

                    //A sheet name has to be the name of the person selected from upload survey view / screen
                    var sheetName = excelSheetList.Count() > 0 ? excelSheetList.First().ToString() : string.Empty;

                    using (OleDbCommand command = new OleDbCommand("SELECT * FROM [" + sheetName + "$]", con))
                    {

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            //Get column names from a spreadsheet
                            var columnNames = GetColumnNames(reader);

                            //Compares uploaded file header against predefined survey spreadsheet (which is stored in the web config file) header columns
                            //if the columns do not match then the operation will stop and an empty list will be returned
                            if (!VerifyUploadedTemplate(columnNames))
                            {
                                return new List<QuestionnaireObject>();
                            }

                            while (reader.Read())
                            {                            
                                DataTable dt = new DataTable();

                                //Get a question from the first col/row
                                var question = !string.IsNullOrWhiteSpace(reader[0].ToString()) ? reader[0].ToString() : string.Empty;

                                var questionRaterList = new List<Rater>();

                                for (int i = 1; i < reader.FieldCount; i++)
                                {
                                    //Ensure that rater's name is not empty and add the name to the list of raters
                                    if (!string.IsNullOrWhiteSpace(reader[i].ToString()))
                                    {
                                        questionRaterList.Add(new Rater() { Name = reader[i].ToString() });

                                        //if a rater is a team, get team size by team name and increment minimum number of raters by team size
                                        if (teamList.Where(c => c.Team.ToLower() == reader[i].ToString().ToLower()).Count() > 0)
                                        {
                                            var surveyTeamObject = new SurveyTeam();

                                            surveyTeamObject.Team = reader[i].ToString();

                                            var teamInfo = new Surveys().getSurveyTeamList(surveyTeamObject);

                                            minimumNumberOfRaters += teamInfo.Distinct().Count();
                                        }
                                        else
                                        {
                                            minimumNumberOfRaters++;
                                        }

                                    }
                                   
                                }
                                
                                //Ensure that a question has at least 3 raters
                                if (minimumNumberOfRaters >= 3 && !string.IsNullOrWhiteSpace(question))
                                {
                                    questionList.Add(new QuestionnaireObject()

                                    {
                                        Question = question, Raters = new List<Rater>(questionRaterList),

                                        QuestionnairePeriodType = questionnairePeriodType,

                                        QuestionnairePeriodValue = questionnairePeriodValue,

                                        RateeName = sheetName

                                    });
                                }
                          
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }

            //var xml = XMLHelper.Serialize(questionList);

            return questionList;
        }

        //A method to return a list of survey questions from the database
        public static List<Survey> GetSurveyQuestions(DataTable dt)
        {
            var surveyList = new List<Survey>();

            //Get a list of raters
            var employeeSurvey = GetLoadRaters(dt);

            foreach (var questionLineItem in employeeSurvey)
            {
                var surveyObject = new Survey();

                List<Rater> ratersInformation = new List<Rater>();

                var raterCounter = 0;

                //Loop through a list of raters and add each name to raters' list
                foreach (var rater in employeeSurvey)
                {
                    if (!string.IsNullOrWhiteSpace(rater.Value[raterCounter].Name))
                    {
                        var raterObject = new Rater();

                        raterObject.Name = rater.Value[raterCounter].Name;

                        ratersInformation.Add(raterObject);
                    }
                }

                    raterCounter++;

                    //Store a question to a question object
                    surveyObject.Question = questionLineItem.Key;

                    surveyObject.RaterList = new List<Rater>(ratersInformation);

                    surveyList.Add(surveyObject);

                    ratersInformation.Clear();                

            }

            return surveyList;
        }

        //Load Raters' information from the datatable
        public static Dictionary<string, List<Rater>> GetLoadRaters(DataTable dt)
        {
            var questionList = new Dictionary<string, List<Rater>>();

            var raterObject = new Rater();

            var question = string.Empty;

            var questionRaterList = new List<Rater>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Get a question from datatable row and store it to a question variable 
                question = !string.IsNullOrWhiteSpace(dt.Rows[i].ItemArray[0].ToString()) ? dt.Rows[i].ItemArray[0].ToString() : string.Empty;
                //Go through all the PA Upload template spreadsheet columns except first column (Question) to get all the raters
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    //get raters' information
                    raterObject.Name = !string.IsNullOrWhiteSpace(dt.Rows[i].ItemArray[i].ToString()) ? dt.Rows[i].ItemArray[i].ToString() : string.Empty;

                    if (!string.IsNullOrWhiteSpace(raterObject.Name))
                    {
                        questionRaterList.Add(new Rater() { Name = raterObject.Name });
                    }
                }

                //Add a question and its associated rater in the disctinery list
                questionList.Add(question, questionRaterList);
            }

            return questionList;
        }

        //Return column names which will later be used for verifying the validity if an uploaded spreadsheet
        public static string GetColumnNames(OleDbDataReader reader)
        {
            StringBuilder sb = new StringBuilder();

            var columnLength = reader.FieldCount;

            //Loop through column names create a comma separated string 
            for (int i = 0; i < columnLength; i++)
            {
                //Skip the first column
                //if (i == 2)
                //{
                sb.Append(reader.GetName(i));

                if (!(i == reader.FieldCount - 1)) sb.Append(",");
                //}
            }

            //return comma separated string consisting of all the column names from a spreadsheet file
            return sb.ToString();
        }

        //Verify survey spreadsheet file header / columns against uploaded columns
        //returns true if the column match otherwise return false if they do not match
        public static bool VerifyUploadedTemplate(string uploadedColumnNameString)
        {
            var surveyTemplate = Config.SurveyTemplateHeadings;

            //compare the columns and return false if they do not match
            if (!(uploadedColumnNameString.ToLower().Trim() == surveyTemplate.ToLower().Trim())) return false;

            return true;
        }

        //This method accepts a file extension and a file name - checks a file extension and return the correct excel connection string from the web config file
        public static string GetExcelConnectionString(string fileExtension, string fileName)
        {
            var conString = string.Empty;

            try
            {
                switch (fileExtension)
                {
                    case ".xls":
                        conString = Config.GetOffice2003ConnectionString(fileName);
                        break;
                    case ".xlsx":
                        conString = Config.GetOffice2007ConnectionString(fileName);
                        break;
                    case ".xlsm":
                        conString = Config.GetOfficeMacroConnectionString(fileName);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return conString;
        }

        //This method returns employee name and surname from active Directory
        public static List<Rater> GetEmployeeInformation()
        {
            var empList = new List<Employee>();

            var raterObject = new List<Rater>();

            var list = new ADUsers().GetAdGroupsForUser(Generic.GetCurrentLogonUserName(), new ADUsers().DomanName);

            var raterList = new List<Rater>();

            for (int i = 0; i < list.Count; i++)

            {
                //Pass DG-Futuregrowth as a group name and only returns all employees that have access to that group
                var empGroupList = new ADUsers().GetUserPerByAdGroup("DG-Futuregrowth");

                for (int j = 0; j < empGroupList.Count; j++)
                {
                    raterList.Add(new Rater() { Name = empGroupList[j].EmployeeFirstName + " " + empGroupList[j].EmployeeSurname });
                }

                break;

            }

            return raterList;
        }
    }
}