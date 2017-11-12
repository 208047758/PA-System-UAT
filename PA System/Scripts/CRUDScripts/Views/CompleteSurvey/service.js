

//Service to get data from service..

completeSurveyApp.service('crudservice', function ($http) {

    this.GetEmployees = function () {

        var endPoint = "/api/employeesurveyhelper/GetEmployees";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetRatings = function () {

        var endPoint = "/api/employeesurveyhelper/GetRatings";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetRatingsDropDown = function () {

        var endPoint = "/api/employeesurveyhelper/GetRatingsDropDown";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetEmployeesWithSurveyAll = function () {
        
        var endPoint = "/api/employeehelper/GetEmployeesWithSurveyAll";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetEmployeesWithSurveyAllPerEmployeeDetailedReport = function (surveyFor) {
        
        var endPoint = "/api/employeehelper/GetEmployeesWithSurveyAllPerRater?id=" + surveyFor;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetEmployeesWithSurveyAllPerEmployeeSummaryReport = function (surveyFor) {
        
        var endPoint = "/api/employeehelper/GetAllEmployeeStatusSurveyReportPerRater?rater=" + surveyFor;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetRatingDescription = function (id) {

        var endPoint = "/api/employeesurveyhelper/GetRatingDescription?rating=" + id;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
               
        return $http.get(endPoint);
    }

    this.LoadQuestions = function () {

        var endPoint = "/api/employeesurveyhelper/GetEmployeeSurveyQuestions";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.LoadAssignedQuestions = function () {

        var endPoint = "/api/employeesurveyhelper/GetEmployeeSurveyQuestions";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.LoadQuestionsPerEmployee = function (id) {

        var endPoint = "/api/employeesurveyhelper/GetEmployeeSurveyQuestions?id=" + id;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetSurveyCompletionProgress = function (id) {

        var endPoint = "/api/employeesurveyhelper/GetSurveyCompletionProgress?surveyFor=" + id;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetNextQuestion = function (id, questionNo, direction) {

        var endPoint = "/api/employeesurveyhelper/GetNextQuestion?id=" + id + "&questionNumber=" + questionNo + "&direction=" + direction;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetEmployeeInformation = function () {

        var endPoint = "/api/raterhelper/GetEmployeeInformation?adGroupName=" + "DG-Futuregrowth";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetEmployeesWithSurvey = function () {
        
        var endPoint = "/api/employeehelper/GetEmployeesWithSurvey";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.CompletionStatusByRaterCompleted = function (rater) {
        
        var endPoint = "/api/employeehelper/CompletionStatusByRaterCompleted?rater=" + rater + "&status=" + "Reviewed";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.CompletionStatusCompleted = function (rater) {
        
        var endPoint = "/api/employeehelper/CompletionStatusByRaterCompleted?rater=" + rater + "&status=" + "Completed";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.CompletionStatusOutstanding = function (rater) {
        
        var endPoint = "/api/employeehelper/CompletionStatusByRaterCompleted?rater=" + rater + "&status=" + "Incomplete";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }
    

    this.GetSurveyCompletionStatusList = function () {
        
        var endPoint = "/api/employeehelper/GetSurveyCompletionStatusList";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }    

    this.GetAllFutureGrowthEmployeesSurveyStatusReport = function () {
        
        var endPoint = "/api/employeehelper/GetAllEmployeeStatusSurveyReport";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetAllFutureGrowthEmployeesSurveyStatusReportPerBusinessUnit = function (businessUnit) {
        
        var endPoint = "/api/employeehelper/GetAllEmployeeStatusSurveyReportPerBusinessUnit?businessUnit=" + businessUnit;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetBusinessUnitList = function () {

        var endPoint = "/api/employeesurveyhelper/GetBusinessUnitList";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.ApproveQuestion = function (Employee) {
        var request = $http({
            method: 'post',
            url: '/api/EmployeeAPI/',
            data: Employee
        });
        return request;
    }

    //save
    this.ApproveQuestion = function (Survey) {

        var endPoint = '/api/employeesurveyhelper/ApproveQuestion';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        var request = $http({
            method: 'POST',
            url: endPoint,
            data: Survey
        });
        return request;
    }

    this.CompleteSurveySaveResponse = function (Survey) {

        var endPoint = '/api/employeesurveyhelper/ApproveQuestion';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        var request = $http({
            method: 'POST',
            url: endPoint,
            data: Survey
        });
        return request;
    }

    this.SaveFinaliseSurvey = function (Survey) {
        
        var endPoint = '/api/employeesurveyhelper/FinaliseSurvey';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        var request = $http({
            method: 'POST',
            url: endPoint,
            data: Survey
        });
        return request;
    }

    this.SaveReOpenSurvey = function (Survey) {
        
        var endPoint = '/api/employeesurveyhelper/ReOpenSurvey';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        var request = $http({
            method: 'POST',
            url: endPoint,
            data: Survey
        });
        return request;
    }

    this.SaveRateEmployee = function (survey) {
        
        var endPoint = '/api/employeesurveyhelper/SaveRateEmployee';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        var request = $http({
            method: 'POST',
            url: endPoint,
            data: survey
        });
        return request;
    }


    


    //this.GetSingleQuestion = function (id) {

    //    var endPoint = "/api/employeesurveyhelper/GetSingleQuestion?question=" + id;

    //    //any environment except local
    //    if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
    //        endPoint = "/PAS" + endPoint;
    //    } else {
    //        endPoint = endPoint;
    //    }

    //    return $http.get(endPoint);
    //}

    //update Selected Question records
    this.update = function (UpdateEmpNo, Employee) {

        var updaterequest = $http({
            method: 'put',
            url: "/api/EmployeeAPI/" + UpdateEmpNo,
            data: Employee
        });
        return updaterequest;
    }

});