using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PA_System.Helper
{
    public class Config
    {
        public static string GetOffice2003ConnectionString(string filePath)
        {
            return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + @";Extended Properties='Excel 8.0;HDR=Yes;'";
        }

        public static string GetOffice2007ConnectionString(string filePath)
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
        }

        public static string GetOfficeMacroConnectionString(string filePath)
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties='Excel 12.0 Macro; HDR = YES'";
        }

        public static string GetFileUploadPath()
        {
            string filePath = ConfigurationManager.AppSettings["filePath"].ToString();

            return filePath;
        }

        public static string FileUploadPath
        {
            get
            {
                var excelFilePath = ConfigurationManager.AppSettings["filePath"];

                if (!string.IsNullOrWhiteSpace(excelFilePath)) return excelFilePath;

                throw new NotImplementedException("Web.Config key is missing.");

            }
        }

        public static string TempFileUploadPath
        {
            get
            {
                var excelFilePath = ConfigurationManager.AppSettings["tempfilePath"];

                if (!string.IsNullOrWhiteSpace(excelFilePath)) return excelFilePath;

                throw new NotImplementedException("Web.Config key is missing.");

            }
        }

        public static string SurveyTemplateHeadings
        {
            get
            {
                var surveyTemplate = ConfigurationManager.AppSettings["surveyTemplate"].ToString();

                if (!string.IsNullOrWhiteSpace(surveyTemplate))
                {
                    return surveyTemplate;
                }

                throw new NotImplementedException("Web.Config key is missing.");
            }
        }

        public static string EmployeeDataTemplateHeadings
        {
            get
            {
                var employeeDataTemplate = ConfigurationManager.AppSettings["employeeDataHeadingTemplate"].ToString();

                if (!string.IsNullOrWhiteSpace(employeeDataTemplate))
                {
                    return employeeDataTemplate;
                }

                throw new NotImplementedException("Web.Config key is missing.");
            }
        }

        public static string EmployeeDataCSVFilePath
        {
            get
            {
                var excelFilePath = ConfigurationManager.AppSettings["employeeDataCSVFilePath"];

                if (!string.IsNullOrWhiteSpace(excelFilePath)) return excelFilePath;

                throw new NotImplementedException("Web.Config key is missing.");

            }
        }
    }
}