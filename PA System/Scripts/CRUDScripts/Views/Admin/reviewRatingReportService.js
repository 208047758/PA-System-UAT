
reviewRatingAllocationApp.service('reviewRatingReportService', function ($http) {

    this.GetPrintRatingReport = function (surveyFor, includeRaters) {

        var endPoint = "/api/adminhelper/PrintRatingReport?Survey_For=" + surveyFor + "&includeRaters=" + includeRaters;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetRaterList = function () {

        var endPoint = "/api/employeehelper/GetSurveyCompletionStatusList";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetSurveyCompletionStatusReportPerRater = function (filter, showAllEmployees) {

        var endPoint = "/api/employeesurveyhelper/GetSurveyCompletionStatusReportPerRater?filter=" + filter + "&showEmployeesAssigned=" + showAllEmployees;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetSurveyCompletionStatusReportBySelectedRater = function (rater) {

        var endPoint = "/api/employeesurveyhelper/GetSurveyCompletionStatusReportBySelectedRater?rater=" + rater;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetSurveyCompletionStatusReportBySelectedbusinessUnit = function (businessUnit) {

        var endPoint = "/api/employeesurveyhelper/GetSurveyCompletionStatusReportBySelectedbusinessUnit?businessUnit=" + businessUnit;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetRaterList = function () {

        var endPoint = "/api/employeehelper/GetSurveyCompletionStatusList";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetSurveysAssignedPerRater = function (rater) {

        var endPoint = "/api/employeehelper/GetSurveysAssignedPerRater?rater=" + rater;

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

    this.GetEmployeesListToFilterReport = function () {

        var endPoint = "/api/employeehelper/GetSurveyCompletionStatusList";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }


});