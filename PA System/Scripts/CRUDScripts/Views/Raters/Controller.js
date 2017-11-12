

//Angular controller


raterApp.controller('UpdateRateController', function ($scope, raterService) {

    $scope.updateSurveyRatertable = true;

    $scope.ShowRaterList = false;

    $scope.divSave = false;

    populateAllUsers();

    $scope.Selectedraters = [];

    $scope.selectedRatersList = false;

    $scope.ShowRaterList = false;

    //$scope.showOldQuestion = false;

    $scope.divSurveyQuestions = false;

    loadEmployees();

    loadEmployeesToUpdateRaters();

    LoadBusinessUnitList();

    $scope.divEmployeeTable = false;

    $scope.showPopup = false;

    $scope.cellSurvey_For = true;

    $scope.showPopup = false;

    $scope.errorMessage = false;

    $scope.hideSurveyFor = true;

    $scope.btnRefreshQuestions = true;

    $scope.divShowBackButton = false;

    $scope.isPopupVisible = function () {
        $scope.showPopup = true;
    };
    $scope.hidePrompt = function () {
        $scope.EndDate = "";
        $scope.errorMessage = false;
        $scope.showPopup = false;
    };
    $scope.deleteRecord = function () {

        alert("Removing a question from a survey");

        $scope.hidePrompt();
    };

    function populateAllUsers() {

        var employeeList = raterService.GetEmployeeInformation("DG-Futuregrowth");

        employeeList.then(function (data) {

            $scope.EmployeeList = data.data;
        });

    }

    function loadEmployeesToUpdateRaters() {
        
        $('#loading').show();

        var surveyQuestions = raterService.GetEmployeesWithSurveyAll();

        surveyQuestions.then(function (data) {

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;


            $scope.UpdateRaterEmployeesList = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');


        },
        function () {
        });
    }

    function loadEmployees() {
        
        $('#loading').show();

        var surveyQuestions = raterService.GetEmployeesWithSurvey();

        surveyQuestions.then(function (data) {

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $scope.Employees = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            
        },
        function () {
        });
    }

    $scope.GetEmployeesWithSurveyByTeam = function (team)
    {
        $('#loading').show();

        var surveyQuestions = raterService.GetEmployeesWithSurveyByTeam(team);

        surveyQuestions.then(function (data) {

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $scope.Employees = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');


        },
        function () {
        });
    }

    function LoadBusinessUnitList() {

        var businessUnitList = raterService.GetBusinessUnitList();

        businessUnitList.then(function (data) {

            $scope.DepartmentList = data.data;

        })

    }

    $scope.ShowSelectRaters = function (index) {

    }

    $scope.showSubmitButton = function (surveyRaters, question) {
        
        $scope.Question = question;

        $scope.ShowRaterList = true;

        $scope.ShowRaterList = true;

        $scope.showOldQuestion = false;

        $scope.divEmployeeTable = false;

        //$scope.oldQuestion = question;

        $scope.Selectedraters = [];

        $scope.SurveyRaterList = surveyRaters;

        $scope.Selectedraters = surveyRaters.split(',')

        $scope.raterStringModel = $scope.SurveyRaterList;

        $scope.selectedRatersString = $scope.Selectedraters;

        //angular.forEach($scope.SurveyRaterList, function (item) {
            
        //   // $scope.selectedRatersString += item.Name + ",";

        //    $scope.Selectedraters.push(item.Name);

        //});

        $scope.raterStringModel = $scope.Selectedraters;
    }

    $scope.GetEmployeeInformation = function (department) {
        
        var employeeList = raterService.GetEmployeeInformation(department);

        $scope.EmployeeList = employeeList;

    }

    $scope.HideBackButton = function ()
    {
        $scope.divShowBackButton = false;

        $scope.divEmployeeTable = true;

        $scope.employeeTable = true;

        $scope.divSurveyQuestions = false;

        $scope.selectedRatersList = false;

        $scope.ShowRaterList = false;
    }

    $scope.LoadQuestionsPerEmployee = function (survey_For) {
        
        $('#loading').show();

        $scope.lblSurveyFor = survey_For;

        $scope.rateeModel = survey_For;

        $scope.selectedRatee = survey_For;

        $scope.rateeString = survey_For;

        $scope.updateRaterSelectedQuestion = $scope.Question;

        $scope.divEmployeeTable = false;

        $scope.Raters = false;

        $scope.divSurveyQuestions = false;

        //$scope.employeeTable = false;

        //$scope.divSurveyQuestions = true;

        $scope.ModelSurveyFor = survey_For;

        var surveyQuestions = raterService.LoadQuestionsPerEmployee(survey_For);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;

            $scope.divSurveyQuestions = true;

            $scope.updateSurveyRatertable = true;

            $scope.divShowBackButton = true;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            //$scope.divShowQuestions = true;

            //$scope.surveyQuestionTable = true;

        },
        function () {
            //Show error message
        });
    }

    $scope.LoadQuestionsPerEmployeeRefresh = function () {
                
        $('#loading').show();
        var surveyQuestions = raterService.LoadQuestionsPerEmployee($scope.ModelSurveyFor);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');
        },
        function () {

        });
    }

    $scope.GetEmployeeQuestionsByRater = function (rater) {
        
        $scope.divEmployeeTable = false;

        $scope.Raters = false;

        $scope.employeeTable = false;

        $scope.divSurveyQuestions = true;

        var surveyQuestions = crudservice.LoadQuestionsPerEmployee(rater.First_Name + " " + rater.Surname);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;

            GetNextQuestionToRate("Next");

        })
    }

        //var employeeList = raterService.GetEmployeeQuestionsByRater(rater.First_Name + " " + rater.Surname);

        //employeeList.then(function (data) {

        //    $scope.Questions = data.data;

        //});
    

});