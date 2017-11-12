

//Service to get data from service..

capturequestionapp.service('questioncaptureservice', function ($http) {



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

    this.GetBusinessUnits = function () {

        var endPoint = "/api/employeesurveyhelper/GetDepartmentInformation";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.loadTempQuestions = function () {

        var endPoint = "/api/employeesurveyhelper/GetTempQuestions";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get();
    }

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

    this.IsDuplicateQuestion = function (question) {

        var endPoint = "/api/employeesurveyhelper/IsDuplicateQuestion?question=" + question;

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

    this.GetKPIInformation = function () {

        var endPoint = "/api/employeesurveyhelper/GetKPIInformation";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.SaveNewQuestion = function (questionObject) {
      //  return $http.get("/api/employeesurveyhelper/SaveQuestion?questionObject=" + questionObject);

        var endPoint = '/api/employeesurveyhelper/SaveQuestion';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

       return $http({
            method: 'POST',
            url: endPoint,
            data: questionObject,
        })
    }

    this.GetSingleQuestion = function (id) {
        
        var endPoint = "/api/employeesurveyhelper/GetSingleQuestion?question=" + id;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

});