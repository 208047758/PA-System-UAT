
var capturequestionapp = angular.module('capturequestionapp', ['datatables']);

capturequestionapp.config(['$compileProvider', function ($compileProvider) {
    //$compileProvider.debugInfoEnabled(false);
}]);

capturequestionapp.controller('capturesurveycontroller', function ($scope, $http, questioncaptureservice) {

    loadEmployees();

    LoadQuestions();

    GetBusinessUnits();

    loadEmployees("DG-Futuregrowth");

    GetKPIInformation();

    loadTempQuestions();

    $scope.NewQuestion = false;

    $scope.saveCaptureSurveyStatusAlert = false;

    $scope.divQuartely = false;

    $scope.divMonthly = true;

    $scope.divDivYearly = false;

    $scope.divShowQuestions = true;

    $scope.selectedRatersList = false;

    $scope.ErrorMessage = "Error occured, please try again";

    $scope.saveCaptureSurveyStatusAlert = false;

    $scope.divCaptureSurveyErrorMessageAlert = false;

    $scope.SubmitSurvey = false;

    $scope.drpSelectedRaters = "Please Select";

    $scope.drpSelectedRaters = "";

    $scope.Selectedraters = [];

    $scope.isPreviousQuestion = true;

    $scope.DuplicateQuestion = false;

    var date = new Date()

    var month = date.getMonth() + 1;

    var surveyPeriodType = "";

    var surveyPeriodValue = "";

    var year = date.getFullYear();

    var quarter;

    $scope.ShowDivQuestion = function () {

        $scope.NewQuestion = true;

        $scope.isPreviousQuestion = false;
    }

    $scope.MonthList = [

        //{ value: 0, Name: "---Please Select---" },
        { value: 1, Name: "January" },
        { value: 2, Name: "February" },
        { value: 3, Name: "March" },
        { value: 4, Name: "April" },
        { value: 5, Name: "May" },
        { value: 6, Name: "June" },
        { value: 7, Name: "July" },
        { value: 8, Name: "August" },
        { value: 9, Name: "September" },
        { value: 10, Name: "October" },
        { value: 11, Name: "November" },
        { value: 12, Name: "December" }
    ];

    $scope.MonthList[month].Selected = true;

    $scope.selectedMonth = $scope.MonthList[month].Name;

    surveyPeriodType = "Monthly";

    $scope.QuestionnairePeriodType = surveyPeriodType;

    // surveyPeriodValue = date.getFullYear();

    //if (month < 10) {

    //    surveyPeriodValue = date.getFullYear() + "" + "0" + "" + month + "";
    //    $scope.QuestionnairePeriodValue = surveyPeriodValue;

    //} else {
    //    surveyPeriodValue = date.getFullYear() + "" + month + "";
    //    $scope.QuestionnairePeriodValue = surveyPeriodValue;
    //}


    $scope.YearList = [

     { value: 2016, Name: "2016" },
     { value: 2016, Name: "2016" },
     { value: 2015, Name: "2015" },
     { value: 2014, Name: "2014" }
    ];

    $scope.QuartelyOptions = [

        { value: 1, Name: "1st Quarter" },
        { value: 2, Name: "2nd Quarter" },
        { value: 3, Name: "3rd Quarter" },
        { value: 4, Name: "4th Quarter" }
    ];

    $scope.SaveNewQuestion = function (surveyRaters) {


        var selectedRaters = document.getElementsByClassName("ms-choice");

        var wrappedResult = angular.element(selectedRaters);

        var defaultPlaceHolder = document.getElementsByClassName("placeholder");

        var defaultPlaceHolderwrappedResult = angular.element(defaultPlaceHolder);
        
        //var selectedRaters = document.getElementsByClassName("ng-pristine ng-untouched ng-valid ng-hide");

        //alert("Entered Question: " + $scope.Question);

        var selectedQuestion = $scope.selectedOption.Question == undefined && $scope.isPreviousQuestion == false ? $scope.Question : $scope.selectedOption.Question;

        var ratersList = wrappedResult[0].innerText.split(',');
            
        //var validateQuestion = $scope.IsDuplicateQuestion(selectedQuestion);

        if (surveyPeriodType == "Monthly") {

            surveyPeriodValue = $scope.selectedMonth.value;

        } else if (surveyPeriodType == "Quartely") {


            surveyPeriodValue = $scope.selectedQuarter.value;

        } else if (surveyPeriodType == "Annually") {

            surveyPeriodValue = $scope.selectedYear.value;

        }

        if ($scope.selectedEmployee == undefined) {

            $scope.divCaptureSurveyErrorMessageAlert = true;

            $scope.errorMessage = "Warning!!, Please select a ratee and try again";

        }
        else if ($scope.DuplicateQuestion == true) {

                $scope.divCaptureSurveyErrorMessageAlert = true;

                $scope.errorMessage = "Duplicate Entry - Question already exists, please try again";

                $scope.saveCaptureSurveyStatusAlert = false;

                $scope.DuplicateQuestion = false;

                validateQuestion = "";
        }

        else if ($scope.selectedOption == undefined && $scope.isPreviousQuestion == true) {

                $scope.divCaptureSurveyErrorMessageAlert = true;

                $scope.errorMessage = "Warning!!, Please select or click add to enter new question";            

        }

        else if ($scope.selectedKPIOption == undefined) {

            $scope.divCaptureSurveyErrorMessageAlert = true;

            $scope.errorMessage = "Warning!!, Please select assign KPI to the question";
        }
        else if (wrappedResult[0].innerText == undefined || wrappedResult[0].innerText == "Please Select") {

            $scope.divCaptureSurveyErrorMessageAlert = true;

            $scope.errorMessage = "Warning!!, Please select rater(s) dropdown to assign raters";

        } else if (ratersList.length < 3) {

            $scope.divCaptureSurveyErrorMessageAlert = true;

            $scope.errorMessage = "Warning!!, Please select at least 3 raters and try again";

        }
        else if (surveyPeriodType == "Monthly" && $scope.selectedmonth.value < month) {

            $scope.divCaptureSurveyErrorMessageAlert = true;

            $scope.errorMessage = "Warning!!, Cannot capture a question for a previous month";

            $scope.selectedmonth = "";

        }

        else {
            $scope.divCaptureSurveyErrorMessageAlert = false

            $scope.ErrorMessage = "";

            $scope.QuestionnaireObject = {
                Question: $scope.selectedOption.Question == undefined && $scope.isPreviousQuestion == false ? $scope.Question : $scope.selectedOption.Question,
                KPIDesc: $scope.selectedKPIOption.KPIDesc,
                QuestionnairePeriodType: surveyPeriodType,
                QuestionnairePeriodValue: surveyPeriodValue,
                Raters: $scope.Selectedraters,
                RateeName: $scope.selectedEmployee.DisplayName,
                RatersString: wrappedResult[0].innerText
            };

            document.getElementById("panelQuestionEntry").style.marginTop = "5vw";

            return $http({
                method: 'POST',
                url: '/api/employeesurveyhelper/savequestion',
                //url: '/employeesurvey/SaveQuestion',
                data: $scope.QuestionnaireObject
            }).success(function (data) {

                  loadTempQuestions();

                  resetForm();

                  $scope.saveCaptureSurveyStatusAlert = true;

            }).error(function(data, status){
                
                if (status == 409) {

                    $scope.divCaptureSurveyErrorMessageAlert = true;

                    $scope.errorMessage = "Duplicate Entry - Question already exists, please try again";

                    $scope.saveCaptureSurveyStatusAlert = false;

                    $scope.DuplicateQuestion = false;

                    validateQuestion = "";
                    
                    $scope.divCaptureSurveyErrorMessageAlert = true;

                    document.getElementById("panelQuestionEntry").style.marginTop = "1vw";

                    //angular.element("#panelQuestionEntry")[0].style = { "margin-top": "1vw" };

                }
            });
            $scope.isPreviousQuestion = true;
        }

    };

    $scope.IsDuplicateQuestion = function (question) {

        var doesQuestionExists = questioncaptureservice.IsDuplicateQuestion(question);

        doesQuestionExists.then(function (data) {

            if (data.data == true || data.data.length > 0) {

                $scope.DuplicateQuestion = true;
            }

            return $scope.DuplicateQuestion;

        },
        
        function () {
        });
    }

    $scope.GetValue = function (monthName) {

        $scope.selectedmonth = monthName;

        surveyPeriodValue = $scope.selectedmonth.value;
    }

    $scope.GetQuartelyValue = function (quarter) {

        $scope.selectedQuarter = quarter;

        surveyPeriodValue = $scope.selectedQuarter.value;
    }

    $scope.GetAnnualValue = function (annual) {
        $scope.selectedYear = annual;

        surveyPeriodValue = $scope.selectedYear.value;
    }

    function resetForm() {
        var selectedRaters = document.getElementsByClassName("ms-choice");

        var wrappedResult = angular.element(selectedRaters);

        wrappedResult[0].innerText

        $scope.selectedOption = "";

        $scope.selectedKPIOption = "";

        $scope.drpSelectedRaters = "";

        $scope.Question = "";

        $scope.isPreviousQuestion = true;

        $scope.NewQuestion = false;
    }


    function loadTempQuestions() {
        
        var enteredQuestions = questioncaptureservice.loadTempQuestions();

        enteredQuestions.then(function (data) {
            //success
            $scope.EnteredQuestions = data.data;

            $scope.numberOfQuestions = $scope.EnteredQuestions.length;

            if ($scope.EnteredQuestions.length >= 5) {

                $scope.SubmitSurvey = true;                
            }
        });
    }

    function loadEmployees() {

        var surveyQuestions = questioncaptureservice.GetEmployees();

        surveyQuestions.then(function (data) {
            //success

            $scope.Employees = data.data;
        },
        function () {

            $scope.divErrorMessage = true;

            $scope.lblApproveQuestionErrorMessageAlert = "Please enter comment and try again";
        });
    }

    function loadEmployees(id) {

        var empList = questioncaptureservice.GetEmployeeInformation(id);

        empList.then(function (d) {

            $scope.Employees = angular.fromJson(d.data);

            // $scope.selectedEmployee = $scope.Employees[0];
        },
        function () {

        });
    }

    function LoadQuestions() {

        var surveyQuestions = questioncaptureservice.LoadQuestions();

        surveyQuestions.then(function (data) {
            //success
            var questionObject = data.data;

            $scope.Questions = data.data;

            //$scope.selectedOption = $scope.Questions[0];
        },
        function () {
        });
    }

    function GetKPIInformation() {

        var surveyQuestions = questioncaptureservice.GetKPIInformation();

        surveyQuestions.then(function (data) {
            //success

            var questionObject = data.data;

            $scope.KPIs = data.data;

            // $scope.selectedKPIOption = $scope.KPIs[0];
        },
        function () {
        });
    }

    function GetBusinessUnits() {

        var departmentList = questioncaptureservice.GetBusinessUnits();

        departmentList.then(function (data) {

            $scope.DepartmentList = data.data;

            $scope.selectedQuestion = $scope.departmentList;
        },
        function () {
        });
    }


    $scope.ShowMonthly = function () {
        
        $scope.divQuartely = false;

        $scope.divMonthly = true;

        $scope.divDivYearly = false;

        surveyPeriodType = "Monthly";

        $scope.QuestionnairePeriodType = surveyPeriodType

        surveyPeriodValue = date.getFullYear();

        if (month < 10) {

            surveyPeriodValue = date.getFullYear() + "" + "0" + "" + month + "";
            $scope.QuestionnairePeriodValue = surveyPeriodValue;

        } else {
            surveyPeriodValue = date.getFullYear() + "" + month + "";
            $scope.QuestionnairePeriodValue = surveyPeriodValue;
        }


        $scope.MonthList[month].Selected = true;

        $scope.selectedMonth = $scope.MonthList[month].Name;
    }


    $scope.ShowQuartely = function () {

        $scope.divQuartely = true;

        $scope.divMonthly = false;

        $scope.divDivYearly = false;

        if (month < 4) { quarter = 1; }

        else if (month < 7) { quarter = 2; }

        else if (month < 10) { quarter = 3; }

        else if (month < 13) { quarter = 4; }

        $scope.Quarter = quarter;

        surveyPeriodType = "Quartely";

        surveyPeriodValue = date.getFullYear() + "" + "0" + quarter + "";

        $scope.QuestionnairePeriodType = surveyPeriodType

        $scope.QuestionnairePeriodValue = surveyPeriodValue;

    }

    $scope.Year = year;

    // $scope.Month = month;

    $scope.ShowAnnally = function () {

        $scope.divQuartely = false;

        $scope.divMonthly = false;

        $scope.divDivYearly = true;

        $scope.selectedYear = $scope.YearList[year];

        surveyPeriodType = "Annually";

        surveyPeriodValue = $scope.Year;

        $scope.QuestionnairePeriodType = surveyPeriodType

        $scope.QuestionnairePeriodValue = surveyPeriodValue;

    }

    $scope.LoadQuestions = function () {

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $scope.divEmployeeTable = false;

        $scope.divShowQuestions = true;

        $scope.divBackButton = true;

        //$scope.btnApproveQuestionDisable = true;

        var surveyQuestions = questioncaptureservice.LoadQuestions();

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;
        },
        function () {
            //Show error message
        });
    }



    $scope.GetEmployeeInformation = function (id) {

        var empList = questioncaptureservice.GetEmployeeInformation(id);

        empList.then(function (d) {

            $scope.Employees = angular.fromJson(d.data);

            // $scope.selectedEmployee = $scope.Employees[0];
        },
        function () {

        });
    }

});

