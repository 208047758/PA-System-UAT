
var app = angular.module('surveyApp', []);
//Angular controller 

app.controller('employeeSurveyController', ['$scope', '$http', employeeSurveyController]);

$('#orderModal').modal({
    keyboard: true,
    backdrop: "static",
    show: false,

}).on('show', function () {
    var getIdFromRow = $(event.target).closest('tr').data('id');
    //make your ajax call populate items or what even you need
    $(this).find('#orderDetails').html($('<b> Order Id selected: ' + getIdFromRow + '</b>'))
});

function employeeSurveyController($scope, $http) {

    $scope.surveyQuestionTable = false;

    $scope.employeeTable = true;

    $http.get('/api/employeesurveyhelper/GetEmployeeSurveyQuestions').success(function (data) {

        $scope.Employees = data;

    }).error(function () {

        $scope.error = "An Error has occured while loading posts!";

    });

    $scope.GetQuestions = function () {

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $http.get('/api/employeesurveyhelper/GetEmployeeSurvey').success(function (data) {

            $scope.Questions = data; 

            var survey = {

                Question: $scope.Question,

                RaterList: $scope.RaterList,

                KPIDesc: $scope.KPIDesc,

                RatersString: $scope.RatersString

            };


        }).error(function () {

            $scope.error = "An Error has occured while loading posts!";

        });

    
    };

    $scope.GetSingleQuestion = function (question) {

        $http.get('/api/employeesurveyhelper/GetSingleQuestion?question=' + question).success(function (data) {

            $scope.Question = data.Question;

            $scope.KPIDesc = data.KPIDesc;

            $scope.RatersString = data.RatersString;



        }).error(function () {

            $scope.error = "An Error has occured while loading posts!";

        })

    };

    $scope.EmpDetailsModel =
   {
       Question: '',
       KPIDesc: '',
       RatersString: ''
   };

    $scope.EmployeeViewModel = {

        empDetailModel: $scope.EmpDetailsModel
    };

    $scope.EditEmployee = function (EmployeeID) {

        var questions = $scope.EditedEmployee(EmployeeID);

        
        questions.then(function (d) {

            var data = d.data;

            $scope.Question = data.Question;
            $scope.KPIDesc = data.KPIDesc;
            $scope.RatersString = data.RatersString;
            //$scope.$apply();

        })
    };

    $scope.EditEmployee = function (id) {

        var ServerData = $http({
            method: "Get",
            url: "/api/employeesurveyhelper/GetSingleQuestion?question=" + id,
            data: JSON.stringify({ EmpID: id }),
            dataType: 'json',
            //contentType: 'application/json',

        });
        //    .then(function (data) {
          
        //    $scope.Question = data.Question;
        //    $scope.KPIDesc = data.KPIDesc;
        //    $scope.RatersString = data.RatersString;
        //});
              

        return ServerData;
    }


    //$scope.get = function () {
        
    //    var singlerecord = crudservice.get(Employee.EmpNo);
    //    singlerecord.then(function (d) {

    //        var record = d.data;
    //        $scope.UpdateEmpNo = record.EmpNo;
    //        $scope.UpdateEmpName = record.EmpName;
    //        $scope.UpdateSalary = record.Salary;
    //        $scope.UpdateDeptName = record.DeptName;
    //        $scope.UpdateDesignation = record.Designation;
    //    },
    //    function () {
    //        swal("Oops...", "Error occured while getting record", "error");
    //    });
    //}


    //var EmployeeSurveyRecords = crudservice.getEmployeeQuestions();
    //EmployeeSurveyRecords.then(function (d) {     //success
    //    $scope.Employees = d.data;
    //},
    //function () {
    //    swal("Oops..", "Error occured while loading", "error"); //fail
    //});


    //save form data
    //$scope.save = function () {

    //    var Employee = {
    //        EmpNo: $scope.EmployeeFirstName,
    //        EmpName: $scope.EmployeeSurname,
    //        Salary: $scope.Department
    //    };
    //    //var saverecords = crudservice.save(Employee);
    //    //saverecords.then(function (d) {
    //    //    $scope.EmployeeFirstName = d.data.EmployeeFirstName;
    //    //    loadEmployees();
    //    //    swal("Reord inserted successfully");
    //    //},
    //    //function () {
    //    //    swal("Oops..", "Error occured while saving", 'error');
    //    //});
    //}

    //get single record by ID

    //$scope.get = function (Employee) {

    //    //var singlerecord = crudservice.get(Employee.EmployeeFirstName);
    //    //singlerecord.then(function (d) {

    //    //    var record = d.data;
    //    //    $scope.UpdateFirstName = record.EmployeeFirstName;
    //    //    $scope.UpdateEmployeeSurname = record.EmployeeSurname;
    //    //    $scope.UpdateDepartment = record.Department;
    //    //},
    //    //function () {
    //    //    swal("Oops...", "Error occured while getting record", "error");
    //    //});
    //}

    //update Employee data
    //$scope.update = function () {

    //    var Employee = {
    //        EmpNo: $scope.UpdateFirstName,
    //        EmpName: $scope.UpdateEmployeeSurname,
    //        Salary: $scope.UpdateDepartment
    //    };

    //    //var updaterecords = crudservice.update($scope.UpdateFirstName, Employee);
    //    //updaterecords.then(function (d) {
    //    //    loadEmployees();
    //    //    swal("Record updated successfully");
    //    //},
    //    //function () {
    //    //    swal("Opps...", "Error occured while updating", "error");
    //    //});
    //}

    ////delete Employee record
    //$scope.delete = function (UpdateEmpNo) {

    //    //var deleterecord = crudservice.delete($scope.UpdateFirstName);
    //    //deleterecord.then(function (d) {
    //    //    var Employee = {
    //    //        EmployeeFirstName: '',
    //    //        EmployeeSurname: '',
    //    //        Department: ''
    //    //    };
    //    //    loadEmployees();
    //    //    swal("Record deleted succussfully");
    //    //});
    //}
    //});
}