
var app = angular.module('surveyApp', []);

app.service('crudservice', function ($http) {

    this.getEmployeeQuestions = function () {
        return $http.get("/api/EmployeeSurveyHelper/");
    }

    //save
    this.save = function (survey) {
        var request = $http({
            method: 'post',
            url: '/api/EmployeeSurveyHelper/SaveSurvey/',
            data:survey
        });
        return request;
    }

    //get single record by Id
    this.get = function (employeeName) {

        return $http.get("/api/EmployeeSurveyHelper/" + employeeName);
    }

    //update Employee records
    this.update = function (employeeName, survey) {

        var updaterequest = $http({
            method: 'put',
            url: "/api/EmployeeSurveyHelper/" + employeeName,
            data:survey
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (employeeName) {

        var deleterecord= $http({
            method: 'delete',
            url: "/api/EmployeeSurveyHelper/" + employeeName
        });
        return deleterecord;
    }
});