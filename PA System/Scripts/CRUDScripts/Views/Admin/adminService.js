
printReportApp.service('adminService', function ($http) {

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
        
        var endPoint = "/api/employeehelper/GetEmployeesListToFilterReport";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }


});