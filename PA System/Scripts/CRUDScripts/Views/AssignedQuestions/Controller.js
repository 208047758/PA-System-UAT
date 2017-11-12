

//Angular controller


myapp2.controller('assignedQuestionsController', function ($scope, viewquestionsService) {

    viewAssignedQuestions();

    loadEmployees();

    $scope.ViewQuestionKPI = true;

    $scope.employeeTable = true;

    $scope.divEmployeeTable = true;

    $scope.assignedQuestionsTable = false;

    $scope.tableViewAssignedQuestion = false;

    function loadEmployees() {

        $scope.employeeTable = true;

        var surveyQuestions = viewquestionsService.GetEmployeesWithSurvey();

        surveyQuestions.then(function (data) {
            //success

            $scope.Employees = data.data;
        },
        function () {

            //$scope.divErrorMessage = true;

            //$scope.lblApproveQuestionErrorMessageAlert = "Please enter comment and try again";
        });
    }

    function viewAssignedQuestions() {

        $scope.assignedQuestionsTable = true;

        var surveyQuestions = viewquestionsService.LoadAssignedQuestions();

        surveyQuestions.then(function (data) {
            //success

            $scope.Questions = data.data;
        },
        function () {

            //show error message
        });

    
    }

    $scope.LoadQuestionsPerEmployee = function (emp) {


        var id = emp.EmployeeFirstName + ' ' + emp.EmployeeSurname;

        $scope.tableViewAssignedQuestions = true;

        $scope.divEmployeeTable = false;

        $scope.divApproveEmployeeQuestion = true;

        $scope.assignedQuestionsTable = true;

        $scope.divBackButton = true;

        //$scope.btnApproveQuestionDisable = true;

        var surveyQuestions = viewquestionsService.LoadQuestionsPerEmployee(id);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;
        },
        function () {
            //Show error message
        });
    }


    $scope.GetSingleQuestion = function (question) {

        $scope.panelViewAssignedQuestionsSingleQuestion = true;

        var singlerecord = viewquestionsService.GetSingleQuestion(question);

        singlerecord.then(function (d) {

            var record = d.data;

            $scope.Question = record.Question;

            $scope.KPIDesc = record.KPIDesc;

            $scope.RatersString = record.RatersString;
        },
        function () {
            swal("Oops...", "Error occured while getting record", "error");
        });
    }






});