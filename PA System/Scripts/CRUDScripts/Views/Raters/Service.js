

//Service to get data from service..


raterApp.service('raterService', function ($http) {

    this.GetEmployeeInformation = function (adGroupName) {

        var endPoint = "/api/raterhelper/GetEmployeeInformation?adGroupName=" + adGroupName;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetEmployeeQuestionsByRater = function (rater) {
   

        var endPoint = "/api/raterhelper/GetEmployeeQuestionsByRater?rater=" + rater;

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

    this.GetEmployeesWithSurveyByTeam = function (team) {

        var endPoint = "/api/employeehelper/GetEmployeesWithSurveyPerTeam?team=" + team;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.LoadQuestionsPerEmployee = function (id) {

        var endPoint = "/api/employeesurveyhelper/GetEmployeeSurveyQuestionsForraterUpdate?id=" + id;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }
});