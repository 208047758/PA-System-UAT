

//Service to get data from service..

myapp.service('crudservice', function ($http) {
    
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

    //this.GetEmployeesWithSurvey = function () {

    //    var endPoint = "/api/employeesurveyhelper/GetEmployeesWithSurvey";

    //    //any environment except local
    //    if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
    //        endPoint = "/PAS" + endPoint;
    //    } else {
    //        endPoint = endPoint;
    //    }

    //    return $http.get(endPoint);
    //}

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

    this.GetEmployeesWithSurveyByDepartment = function (businessUnit) {

        var endPoint = "/api/employeehelper/GetEmployeesWithSurveyAllPerBusinessUnit?businessUnit=" + businessUnit;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }

    this.GetEmployeesWithSurveyToVerifiy = function () {

        var endPoint = "/api/employeehelper/GetEmployeesWithSurveyToVerifiy";

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

    this.LoadQuestionsPerEmployeeForReview = function (id) {


        var endPoint = "/api/employeesurveyhelper/GetEmployeeSurveyQuestionsForReview?id=" + id;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.LoadQuestionsPerEmployeeForVerification = function (id) {


        var endPoint = "/api/employeesurveyhelper/GetSurveyForVerifybyRater?id=" + id;

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

    this.ApproveQuestion = function (Employee) {

        var endPoint = '/api/EmployeeAPI/';

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        var request = $http({
            method: 'post',
            url: endPoint,
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
            method: 'post',
            url: endPoint,
            data: Survey
        });
        return request;
    }

    this.ReviewAllQuestions = function (Survey) {

        var endPoint = '/api/employeesurveyhelper/ReviewAllQuestions';

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

    this.ManagerVerifyAllQuestions = function (Survey) {

        var endPoint = '/api/employeesurveyhelper/ManagerVerifyAllQuestions';

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


    this.ManagerVerifySingleQuestion = function (Survey) {

        var endPoint = '/api/employeesurveyhelper/ManagerVerifySingleQuestion';

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

    //update Selected Question records
    this.update = function (UpdateEmpNo, Employee) {
        ;
        var updaterequest = $http({
            method: 'put',
            url: "/api/EmployeeAPI/" +UpdateEmpNo,
            data:Employee
        });
        return updaterequest;
    }

    

    //delete record
    this.delete = function (UpdateEmpNo) {
        
        var deleterecord= $http({
            method: 'delete',
            url: "/api/EmployeeAPI/" + UpdateEmpNo
        });
        return deleterecord;
    }
});