

//Service to get data from service..


myapp2.service('viewquestionsService', function ($http) {

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

    //delete record
    this.delete = function (UpdateEmpNo) {
        
        var deleterecord = $http({
            method: 'delete',
            url: "/api/EmployeeAPI/" + UpdateEmpNo
        });
        return deleterecord;
    }
});