
using DataDLL.Sources.PASystem.Read;
using System.Collections.Generic;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Infrastructure
{
    public class EmployeeHelper
    {
        
        //Return all fg employees
        public List<SurveyTeamList> GetEmployeeListAll()
        {
            return new Surveys().getSurveyTeamEmployeeList();
        }

        //Return all fg employees per team Team
        
        public List<SurveyTeamList> GetSurveyTeamList(SurveyTeam team)
        {
            return new Surveys().getSurveyTeamList(team);
        }

    }
}