
var completeSurveyApp = angular.module('completeSurveyApp', ['datatables']);

completeSurveyApp.controller('completeSurveyController', function ($scope, crudservice) {

    $scope.divEmployeeTable = false;;

    $scope.divShowQuestions = false;

    $scope.divBackButton = false;

    $scope.QuestionResponse = false;

    //$scope.DisableButtonSave = true;

    $scope.divRatingScale = false;

    $scope.cellSurvey_For = true;

    $scope.hideStatus = true;

    $scope.divSurversPerBusinessUnit = false;

    $scope.divSurveyStatusSurmmaryPerBusinessUnit = false;

    $scope.divSurversPerRater = false;

    $scope.divErrorMessage = false;

    $scope.alertResponseSuccess = false;
    //$scope.divShowAllSurveyStatus = true;

    //$scope.showAllSurveyStatusTable = true;

    GetEmployeesWithSurveysForRevificationAll();

    //GetEmployeesWithSurveysForReview();

    loadAllFutureGrowthEmployees();

    if (window.location.pathname == "/Admin/ViewSurveyCompletion" || window.location.pathname == "/PAS/Admin/ViewSurveyCompletion") {

        $('#DivLoadingProgress').removeClass('alertBg');

        LoadRatersDropDown();

    } else {
        loadEmployees();

        loadEmployeeWithSurveys();

    }

    loadRatings();

    loadRatingsDropDown();

    viewAssignedQuestions();

    $scope.divShowDepartmentFilter = false;

    $scope.divShowRaterFilter = false;

    $scope.divShowSurveyStatus = true;

    $scope.All = "true";

    $scope.divViewSurveyCompletion = false;

    $scope.divSurveyStatusSurmmary = false;

    $scope.divEmployeeTable = false;

    $scope.divSurveyStatusSurmmaryAccrossOrganisation = true;

    $scope.divSurveyStatusSurmmary = false;

    $scope.divShowRaterFilter = false;

    $scope.employeeTable = false;

    $scope.divEmployeeTable = false;

    $scope.showPopup = false;
    $scope.errorMessage = false;

    $scope.divReportShowQuestions = false;

    $scope.divReportShowQuestions = false;

    //Total Survey Details Report
    $scope.divShowTotalSurveysAccrossDetailsReport = false;

    $scope.ShowTotalSurveysAccrossDetailsReportTable = false;

    $scope.isPopupVisible = function () {

        $('#popupMessage').addClass('alertBg');

        $scope.showPopup = true;
    };

    $scope.ShowFinalisePopup = function (surveyFor) {

        $scope.ModelSurveyFor = surveyFor;

        $('#popupMessage').addClass('alertBg');

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

    $scope.divEmployeeTable = false;


    $scope.ShowEmployeeTable = function () {

        $scope.divShowQuestions = false;

        $scope.divEmployeeTable = true;

        $scope.employeeTable = true;

        $scope.QuestionResponse = false;

        $scope.divBackButton = false;

        $scope.divRatingScale = false;

        loadEmployeeWithSurveys();

        loadEmployees();
    }

    function CheckDisableButtons() {

        if ($scope.Q_No == 1) {

            $scope.DisablePreviousButton = true;

        } else {

            $scope.DisablePreviousButton = false;
        }

    }


    $scope.DisableSaveButton = function () {

        if ($scope.Response == "") {

            $scope.DisableButtonSave = true;

        } else {

            $scope.DisableButtonSave = false;
        }

        if ($scope.Q_No == $scope.Questions.length - 1) {

            $scope.DisableButtonSave = false;

        }
    };

    $scope.DisablePreviousButton = function () {

        if ($scope.Q_No == 1) {

            $scope.DisablePreviousButton = true;

        } else {

            $scope.DisablePreviousButton = false;
        }
    };

    $scope.GetEmployeeInformation = function () {

        var employeeList = crudservice.GetEmployeeInformation();

        $scope.EmployeeList = employeeList;

    };

    //$scope.validateForm = function (response, e) {



    //    if ($scope.Response == undefined && response == undefined) {

    //        alert("Please enter response and try again");

    //        $scope.divErrorMessage = true;

    //        $scope.clicked = false;

    //        $scope.lblResponseErorMessageAlert = "Please enter response and try again";

    //        $scope.DisableButtonSave = true;

    //        return false;
    //    }
    //    else {

    //        $scope.DisableButtonSave = false;

    //        $scope.lblResponseErorMessageAlert = "";

    //        $scope.divErrorMessage = false;

    //        //$scope.ApproveQuestion();
    //        $scope.SaveReponse(e);

    //    }




    //}

    $scope.ShowRaterDiv = function () {

        $scope.Rater = undefined;

        $scope.divViewSurveyCompletion = true;

        $scope.viewSurveyCompletionTable = false;

        GetEmployeesWithSurveysForReview();

        $scope.divShowRaterFilter = false;

        $scope.divShowDepartmentFilter = false;

        $scope.divShowAllEmployeesWithSurvayes = true;

        //$scope.LoadQuestions();

        $scope.divViewSurveyCompletion = false;

        $scope.surveyCompletionTable = false;

        $scope.divSurveyStatusSurmmary = false;

        $scope.divSurveyStatusSurmmaryAccrossOrganisation = false;

        $scope.divSurveyStatusSurmmaryPerBusinessUnit = false;

        //$scope.showAllSurveyStatusTable = false;

        //$scope.divShowAllSurveyStatus = false;

    };


    $scope.ShowDepartmentDiv = function () {

        $scope.BusinessUnit = undefined;

        $scope.divShowRaterFilter = false;

        $scope.divShowDepartmentFilter = true;

        $scope.divSurveyStatusSurmmary = true;

        //$scope.LoadQuestions();

        $scope.divEmployeeTable = false;

        $scope.divViewSurveyCompletion = false;

        $scope.surveyCompletionTable = false;

        LoadBusinessUnitList();

        $scope.divSurveyStatusSurmmaryAccrossOrganisation = false;

        $scope.divShowAllEmployeesWithSurvayes = false;

        $scope.divSurversPerRater = false;

        $scope.divReportShowQuestions = false;

        $scope.divReportShowQuestions = false;

    };

    $scope.ShowSummaryAccrossOrg = function () {

        $scope.BusinessUnit = undefined;

        $scope.Rater = undefined;

        $scope.divSurveyStatusSurmmaryPerBusinessUnit = false;

        $scope.divSurversPerBusinessUnit = false;

        $scope.divSurversPerBusinessUnit = false;

        $scope.divSurversPerRater = false;

        $scope.divSurveyStatusSurmmaryAccrossOrganisation = true;

        $scope.divShowRaterFilter = false;

        $scope.divShowRaterFilter = false;

        $scope.divEmployeeTable = false;

        $scope.employeeTable = false;

        $scope.divSurveyStatusSurmmary = false;

        $scope.divShowAllEmployeesWithSurvayes = false;

        $scope.divReportShowQuestions = false;

        $scope.divReportShowQuestions = false;



    }

    $scope.ShowAll = function () {

        $scope.divShowRaterFilter = false;

        $scope.divShowDepartmentFilter = false;

        $scope.divEmployeeTable = true;

        $scope.employeeTable = true;

        $scope.divViewSurveyCompletion = false;

        $scope.surveyCompletionTable = false;

    };

    function loadEmployees() {

        //$scope.btnApproveQuestionDisable = true;

        //$scope.surveyQuestionTable = false;

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurvey();

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }

    $scope.CompletionStatusByRaterCompleted = function () {

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        $scope.divShowRaterFilter = false;

        $scope.viewSurveyCompletionTable = false;

        var surveyQuestions = crudservice.CompletionStatusByRaterCompleted($scope.Rater.Rater);

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.divShowRaterFilter = true;

            $scope.viewSurveyCompletionTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });

    }

    //Total Surveys Completed By Rater Detail Report
    $scope.CompletionStatusCompleted = function () {

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        $scope.divShowRaterFilter = false;

        $scope.viewSurveyCompletionTable = false;

        $scope.divReportShowQuestions = false;

        $scope.surveyQuestionReportTable = false;

        var surveyQuestions = crudservice.CompletionStatusCompleted($scope.Rater.Rater);

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.divShowRaterFilter = true;

            $scope.viewSurveyCompletionTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });

    }

    //Total Outstanding Details Report
    $scope.CompletionStatusOutstanding = function () {

        $('#DivLoadingProgress').addClass('alertBg');

        $scope.divShowRaterFilter = false;

        $scope.viewSurveyCompletionTable = false;

        $scope.divReportShowQuestions = false;

        $scope.surveyQuestionReportTable = false;

        $('#loading').show();

        var surveyQuestions = crudservice.CompletionStatusOutstanding($scope.Rater.Rater);

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.divShowRaterFilter = true;

            $scope.viewSurveyCompletionTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });

    }

    $scope.CompletionStatusByRaterCompleted = function () {

        $('#DivLoadingProgress').addClass('alertBg');

        $scope.divShowRaterFilter = false;

        $scope.viewSurveyCompletionTable = false;

        $scope.surveyQuestionReportTable = false;

        $scope.divReportShowQuestions = false;

        $('#loading').show();

        var surveyQuestions = crudservice.CompletionStatusByRaterCompleted($scope.Rater.Rater);

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.divShowRaterFilter = true;

            $scope.viewSurveyCompletionTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });

    }

    $scope.SurveyQuestionDrillDownReportPerRater = function () {

        $('#DivLoadingProgress').addClass('alertBg');

        $scope.surveyQuestionReportTable = false;

        $scope.divReportShowQuestions = false;

        $scope.divReportShowQuestions = false;

        $scope.surveyQuestionReportTable = false;

        $('#loading').show();

        var surveyQuestions = crudservice.CompletionStatusByRaterCompleted($scope.Rater.Rater);

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.divShowRaterFilter = true;

            $scope.viewSurveyCompletionTable = true;



            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });

    }

    function loadAllFutureGrowthEmployees() {

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        $scope.showAllSurveyStatusTable = false;

        $scope.divShowAllSurveyStatus = false;

        var surveyQuestions = crudservice.GetAllFutureGrowthEmployeesSurveyStatusReport();

        surveyQuestions.then(function (data) {


            if (data.data.length > 0) {

                $scope.totalSurveysAll = data.data[0].Surveys;

                $scope.SurveysCompletedAll = data.data[0].Completed;

                $scope.TotalOutStandignSurveysAll = data.data[0].Outstanding;

                $scope.ProgressTotalSurveyPercentage = data.data[0].Progress;

                $scope.PercentageOutStanding = 100 - data.data[0].Progress;

            } else {
                $scope.totalSurveysAll = 0;

                $scope.SurveysCompletedAll = 0;

                $scope.TotalOutStandignSurveysAll = 0;

                $scope.ProgressTotalSurveyPercentage = 0;

                $scope.PercentageOutStanding = 0;
            }

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

        });
    }

    $scope.loadAllFutureGrowthEmployeesPerBusinessUnit = function (businessUnit) {

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        $scope.showAllSurveyStatusTable = false;

        $scope.divShowAllSurveyStatus = false;

        $scope.divSurversPerRater = false;

        var surveyQuestions = crudservice.GetAllFutureGrowthEmployeesSurveyStatusReportPerBusinessUnit(businessUnit);

        surveyQuestions.then(function (data) {

            $scope.divSurversPerBusinessUnit = true;

            $scope.divSurveyStatusSurmmaryPerBusinessUnit = true;

            if (data.data.length > 0) {

                // $scope.EmployeeSurveyStatusReportList = data.data;

                $scope.TotalSurveysPerBusinessUnit = data.data[0].Surveys;

                $scope.CompletedSurveysPerBusinessUnit = data.data[0].Completed;

                $scope.OutStandingSurveysPerBusinessUnit = data.data[0].Outstanding;

                $scope.ProgressTotalSurveyPercentagePerBusinessUnit = data.data[0].Progress;

                $scope.PercentageOutStandingPerBusinessUnit = 100 - data.data[0].Progress;
            } else {

                $scope.TotalSurveysPerBusinessUnit = 0;

                $scope.CompletedSurveysPerBusinessUnit = 0;

                $scope.OutStandingSurveysPerBusinessUnit = 0;

                $scope.ProgressTotalSurveyPercentagePerBusinessUnit = 0;

                $scope.PercentageOutStandingPerBusinessUnit = 0;
            }


            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            $('#loading').hide();

            $('div').removeClass('alertBg');
        });
    }


    function loadEmployeeWithSurveys() {

        //$scope.btnApproveQuestionDisable = true;

        //$scope.surveyQuestionTable = false;

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurvey();

        surveyQuestions.then(function (data) {

            $scope.EmployeesList = data.data;

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }

    function GetEmployeesWithSurveysForRevificationAll() {

        //$scope.btnApproveQuestionDisable = true;

        //$scope.surveyQuestionTable = false;

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurvey();

        surveyQuestions.then(function (data) {

            $scope.EmployeesList = data.data;

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }

    function LoadRatersDropDown() {

        //$scope.btnApproveQuestionDisable = true;

        //$scope.surveyQuestionTable = false;

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        var surveyQuestions = crudservice.GetSurveyCompletionStatusList();

        surveyQuestions.then(function (data) {

            $scope.RaterStatusList = data.data;

            //$scope.employeeTable = true;

            //$scope.divEmployeeTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }

    function GetEmployeesWithSurveysForReview() {

        //$scope.btnApproveQuestionDisable = true;

        //$scope.surveyQuestionTable = false;

        $('#DivLoadingProgress').addClass('alertBg');

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurveyAll();

        surveyQuestions.then(function (data) {

            $scope.Employees = data.data;

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }


    function LoadBusinessUnitList() {

        var businessUnitList = crudservice.GetBusinessUnitList();

        businessUnitList.then(function (data) {

            $scope.DepartmentList = data.data;
        })

    }


    function loadRatings() {

        var ratings = crudservice.GetRatings();

        ratings.then(function (data) {

            $scope.Ratings = data.data;

            $scope.selectedOption = $scope.Ratings[-1];
        },
        function () {
        });
    }

    function loadRatingsDropDown() {

        var ratings = crudservice.GetRatingsDropDown();

        ratings.then(function (data) {

            $scope.RatingList = data.data;

            $scope.selectedOption = $scope.RatingList[-1];
        },
        function () {
        });
    }

    function GetSurveyCompletionProgress(surveyFor) {


        var surveyCompletionProgress = crudservice.GetSurveyCompletionProgress(surveyFor);

        surveyCompletionProgress.then(function (data) {

        });
    }


    $scope.HideResonseDiv = function () {
        $scope.QuestionResponse = false;

        $scope.divRatingScale = false;
    }

    //save form data
    $scope.SaveReponse = function (event) {

        event.preventDefault();

        $scope.divEmployeeTable = false;

        $scope.employeeTable = false;

        CheckDisableButtons();

        //if ($scope.selectedOption.Rating == "") {
        //   // alert("Please select rating");
        //    $scope.divResponseErrorMessageAlert = true;

        //    $scope.ResponseErrorMessageAlert = "Please enter comment";
        //}

        //else

        if ($scope.Response == undefined || $scope.Response == "") {

            $scope.divResponseErrorMessageAlert = true;

            //$scope.ResponseErrorMessageAlert = "Please enter comment";

            //$scope.lblResponseErrorMessageAlert = "Please enter comment";

            $scope.divErrorMessage = true;

            $scope.clicked = false;

            $scope.lblResponseErrorMessageAlert = "Please enter response and try again";

            $scope.ResponseErrorMessageAlert = "Please enter response and try again";

            $scope.DisableButtonSave = true;

            return false;
        }
        else {
            $('#loading').show();

            $scope.DisableButtonSave = false;

            $scope.lblResponseErrorMessageAlert = "";

            $scope.divErrorMessage = false;

            //$scope.ApproveQuestion();
            // $scope.SaveReponse(e);

            var survey = {

                Question: $scope.Question,

                Survey_For: $scope.ModelSurveyFor,

                //Rating: $scope.selectedOption.RatingNumber,
                Rating: $scope.selectedOption.Rating,

                Comment: $scope.Response
            };

            var saverecords = crudservice.SaveRateEmployee(survey);

            saverecords.then(function (d) {


                // $scope.Question = d.data;

                //loadEmployees();

                //if (d.data >= 1) {

                $scope.approveQuestionSuccessLabel = "Saved Successfully";

                //$window.alert("Please enter your name!");

                var surveyQuestions = crudservice.LoadQuestionsPerEmployee($scope.ModelSurveyFor);

                surveyQuestions.then(function (data) {

                    $scope.Questions = data.data;

                    var counter = 1;

                    var index = 0;

                    var questionNo = $scope.Q_No;

                    var ratingTempValue = 0.0;

                    angular.forEach($scope.Questions, function (s) {

                        if ($scope.Q_No == $scope.Questions.length) {

                            $scope.Q_No = s.Q_No;

                            ratingTempValue = s.Rating;

                            $scope.Question = data.data[index].Question;

                            $scope.Response = data.data[index].Rater_Comment;

                            $('#loading').hide();

                            $('#DivLoadingProgress').removeClass('alertBg');

                            return false;

                        } else {

                            if (s.Q_No == questionNo + 1) {

                                ratingTempValue = s.Rating;

                                $scope.Q_No = s.Q_No;

                                $scope.Question = data.data[index].Question;

                                $scope.Response = data.data[index].Rater_Comment;

                                //$scope.selectedOption = $scope.RatingList[data.data.Rating];
                                $('#loading').hide();

                                $('#DivLoadingProgress').removeClass('alertBg');
                            }
                            index++;

                            counter++;
                        }


                    });

                    var nextRatingCounter = 0;
                    angular.forEach($scope.RatingList, function (s) {

                        if (s.Rating == ratingTempValue) {

                            $scope.selectedOption = $scope.RatingList[nextRatingCounter];
                        }
                        nextRatingCounter++;
                        // ratingCounter++;
                    });

                    CheckDisableButtons();
                    // GetNextQuestionToRate("Next");

                },
                function (d) {
                    //Show error message

                    event.preventDefault();

                    $scope.divErrorMessage = false;

                    $scope.alertApproveQuestionSuccess = true;

                    $('#loading').hide();

                    $('#DivLoadingProgress').removeClass('alertBg');

                });
                //}


            });

        }
    };

    $scope.FinaliseSurvey = function () {
        
        event.preventDefault();

        $('#loading').show();

        var survey = {

            Survey_For: $scope.ModelSurveyFor,

            Comment: $scope.OverallComment
        };

        var saverecords = crudservice.SaveFinaliseSurvey(survey);

        saverecords.then(function () {

            loadEmployees();

            loadEmployeeWithSurveys();

            $('#loading').hide();

            $('div').removeClass('alertBg');

            $scope.OverallComment = "";

            $scope.hidePrompt();
        });
    };

    $scope.ReOpenSurvey = function (survey_For) {

        event.preventDefault();

        $scope.ModelSurveyFor = survey_For;

        $('#loading').show();

        var survey = {

            Survey_For: $scope.ModelSurveyFor,

            Comment: "No Comment"
        };

        var saverecords = crudservice.SaveReOpenSurvey(survey);

        saverecords.then(function () {

            loadEmployees();

            loadEmployeeWithSurveys();

            $('#loading').hide();

            $('div').removeClass('alertBg');

            $scope.hidePrompt();
        });
    };



    $scope.ViewRaterInformationDetailsReport = function (survey_For) {

        $('#loading').show();

        var summaryInformation = crudservice.GetEmployeesWithSurveyAllPerEmployeeDetailedReport(survey_For.Survey_For);

        summaryInformation.then(function (data) {

            $scope.Employees = data.data;

            //$scope.employeeTable = true;

            //$scope.divEmployeeTable = true;

            //$scope.divSurversPerRater = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }

    $scope.ViewRaterInformationSurmmaryReport = function (survey_For) {

        $('#loading').show();

        $scope.divShowRaterFilter = false;

        $scope.viewSurveyCompletionTable = false;

        var summaryInformation = crudservice.GetEmployeesWithSurveyAllPerEmployeeSummaryReport(survey_For.Rater);

        summaryInformation.then(function (data) {

            $scope.Employees = data.data;

            if (data.data.length > 0) {


                $scope.TotalSurveysPerRater = data.data[0].Surveys;

                $scope.CompletedSurveysPerRater = data.data[0].Completed;

                $scope.OutStandingSurveysPerRater = data.data[0].Outstanding;

                $scope.ProgressTotalSurveyPercentagePerRater = data.data[0].Progress;

                $scope.PercentageOutStandingPerRater = 100 - data.data[0].Progress;
            } else {

                $scope.TotalSurveysPerRater = 0;

                $scope.CompletedSurveysPerRater = 0;

                $scope.OutStandingSurveysPerRater = 0;

                $scope.ProgressTotalSurveyPercentagePerRater = 0;

                $scope.PercentageOutStandingPerRater = 0;
            }


            $scope.divSurversPerRater = true;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

            //return $q.reject(response);
        });
    }


    $scope.LoadQuestions = function () {

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $scope.divEmployeeTable = false;

        $scope.divShowQuestions = true;

        $scope.divBackButton = true;

        //$scope.btnApproveQuestionDisable = true;

        var surveyQuestions = crudservice.LoadQuestions();

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;
        },
        function () {
            //Show error message
        });
    }


    $scope.LoadQuestionsPerEmployee = function (emp, survey_For) {


        // GetSurveyCompletionProgress(survey_For);

        $scope.SurverForName = survey_For;

        $('#loading').show();

        $scope.employeeTable = false;

        $scope.divEmployeeTable = false;

        $scope.divShowQuestions = false;

        $scope.surveyQuestionTable = false;


        $scope.divBackButton = true;

        var id = emp.EmployeeFirstName + ' ' + emp.EmployeeSurname;

        $scope.ModelSurveyFor = emp.Survey_For;

        //$scope.btnApproveQuestionDisable = true;

        var surveyQuestions = crudservice.LoadQuestionsPerEmployee(survey_For);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;

            $scope.QuestionListReport = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            $scope.divShowQuestions = true;

            $scope.surveyQuestionTable = true;

            $scope.divReportShowQuestions = true;

            $scope.surveyQuestionReportTable = true;

        },
        function () {
            //Show error message
        });
    }

    $scope.LoadQuestionsPerEmployeeReport = function (survey_For) {



        $('#loading').show();

        $scope.ModelSurveyFor = emp.Survey_For;

        var surveyQuestions = crudservice.LoadQuestionsPerEmployee(survey_For);

        surveyQuestions.then(function (data) {

            $scope.QuestionListReport = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            $scope.divReportShowQuestions = true;

            $scope.surveyQuestionReportTable = true;

        },
        function () {
            //Show error message
        });
    }

    function viewAssignedQuestions() {


        $scope.LoadQuestions = function () {

            $scope.assignedQuestionsTable = true;

            var assignedQuestions = crudservice.LoadAssignedQuestions();

            assignedQuestions.then(function (data) {

                $scope.Questions = data.data;
            },
            function () {
                //Show error
            });
        }
    }

    function GetNextQuestionToRate(direction) {


        var nextQuestion = crudservice.GetNextQuestion($scope.ModelSurveyFor, $scope.QuestionNo, direction);

        nextQuestion.then(function (data) {

            $scope.Question_ID = data.data.Q_No;

            $scope.Question = data.data.Question;

            $scope.Response = data.data.Rater_Comment;

            $scope.selectedOption = $scope.RatingList[data.data.Rating];

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');
        },
        function () {
            //Show error message
        });

    }

    $scope.GetPreviousQuestion = function (direction) {
        //GetNextQuestionToRate("Previous");  

        $('#loading').show();

        $scope.DisableButtonSave = false;

        $scope.lblResponseErrorMessageAlert = "";

        $scope.divErrorMessage = false;

        //$scope.ApproveQuestion();
        // $scope.SaveReponse(e);

        var survey = {

            Question: $scope.Question,

            Survey_For: $scope.ModelSurveyFor,

            //Rating: $scope.selectedOption.RatingNumber,
            Rating: $scope.selectedOption.Rating,

            Comment: $scope.Response
        };

        if ($scope.Response == undefined || $scope.Response == "") {

            $scope.divResponseErrorMessageAlert = true;

            $scope.ResponseErrorMessageAlert = "Please enter comment and try again";

            $scope.lblResponseErrorMessageAlert = "Please enter comment and try again";

            $scope.divErrorMessage = true;

            $scope.clicked = false;

            //$scope.lblResponseErorMessageAlert = "Please enter c and try again";

            //$scope.lblResponseErrorMessageAlert = "Please enter response and try again";

            $scope.DisableButtonSave = true;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            return false;
        } else {

            var saverecords = crudservice.SaveRateEmployee(survey);

            saverecords.then(function (d) {

                $scope.approveQuestionSuccessLabel = "Saved Successfully";

                var surveyQuestions = crudservice.LoadQuestionsPerEmployee($scope.ModelSurveyFor);

                surveyQuestions.then(function (data) {

                    $scope.Questions = data.data;


                    var questionNo = $scope.Q_No;

                    var index = questionNo - 1;

                    var ratingCounter = 0;

                    var tempRatingValue = 0.0;

                    angular.forEach($scope.Questions, function (s) {

                        if (s.Q_No == questionNo - 1) {

                            tempRatingValue = $scope.Questions[index - 1].Rating;
                            //$scope.selectedOption = $scope.Questions[index].Rating;

                            $scope.Q_No = s.Q_No;

                            $scope.Question = $scope.Questions[index - 1].Question;

                            $scope.Response = $scope.Questions[index - 1].Rater_Comment;

                            //$scope.selectedOption = $scope.RatingList[data.data.Rating];
                            //$('#loading').hide();

                            //$('#DivLoadingProgress').removeClass('alertBg');
                        }

                        //index--;
                    });

                    angular.forEach($scope.RatingList, function (s) {

                        if (s.Rating == tempRatingValue) {

                            $scope.selectedOption = $scope.RatingList[ratingCounter];
                        }
                        ratingCounter++;
                    });
                    $('#loading').hide();

                    $('#DivLoadingProgress').removeClass('alertBg');
                },
                function (d) {
                    //Show error message

                    event.preventDefault();

                    $scope.divErrorMessage = false;

                    $scope.alertApproveQuestionSuccess = true;

                    $('#loading').hide();

                    $('#DivLoadingProgress').removeClass('alertBg');

                });
                //}


            });


        }


    }


    //$scope.GetRatingDescription = function (rating) {

    //    //var singlerecord = crudservice.GetRatingDescription($scope.selectedOption.RatingNumber);

    //    //singlerecord.then(function (d) {

    //    //    var record = d.data;

    //    //    $scope.RatingDescription = record.RatingView;

    //    //},
    //    //function () {

    //    //});

    //}


    $scope.GetSingleQuestion = function (question, questionID, comment, rating, q_no) {


        $scope.ratingScaleValue = rating;

        $scope.Question_ID = questionID;

        $scope.Q_No = q_no;

        $scope.Question = question;

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $scope.panelSingleQuestion = true;

        $scope.divSurveyComment = false;

        $scope.QuestionResponse = true;

        $scope.divRatingScale = true;

        $scope.Response = comment;

        $scope.DisableSaveButton();

        $scope.QuestionNo = questionID;

        CheckDisableButtons();

        // $scope.Response = $scope.selectedOption.Rating;

        //$scope.selectedOption = rating;

        //$scope.selectedOption = $scope.RatingList[parseInt(rating)].Rating;
        var counter = 0;

        angular.forEach($scope.RatingList, function (s) {

            if (s.Rating == rating) {

                $scope.selectedOption = $scope.RatingList[counter];
            }
            counter++;
        });


        //Show Complete Questionnaire Modal


        //var singlerecord = crudservice.GetSingleQuestion(question);

        //singlerecord.then(function (d) {

        //    var record = d.data;

        //    $scope.Question = record.Question;

        //    $scope.KPIDesc = record.KPIDesc;

        //    $scope.RatersString = record.RatersString;
        //},
        //function () {

        //});
    }

    //update Employee data
    $scope.update = function () {

        var Employee = {

            EmployeeFirstname: $scope.EmployeeFirstname,

            Department: $scope.Department
        };

        var updaterecords = crudservice.update($scope.UpdateEmpNo, Employee);
        updaterecords.then(function (d) {
            loadEmployees();
            swal("Record updated successfully");
        },
        function () {
            //Show error message
        });
    }


});

