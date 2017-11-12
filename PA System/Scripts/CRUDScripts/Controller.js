

//Angular controller
//var myapp = angular.module('myangularapp');

myapp.controller('crudcontroller', function ($scope, crudservice) {

    $scope.divEmployeeTable = true;

    $scope.divApproveEmployeeQuestion = false;

    $scope.divBackButton = false;

    $scope.QuestionResponse = false;

    $scope.divShowFilters = false;

    $scope.StatusList = [];

    $scope.employeeTable = false;

    $scope.divEmployeeTable = false;

    $scope.cellSurvey_For = true;

    $scope.hideRatersList = true;
    //Review Survey
    $scope.divShowDepartmentFilter = true;

    $scope.cellManager = true;

    GetBusinessUnitList();

    $scope.surveyReviewedAlertSucess = false;

    //loadEmployees();

    GetEmployeesWithSurveysForReviewAll();

    if (window.location.pathname == "/PAS/EmployeeSurvey/ApproveQuestionnaire" || "/EmployeeSurvey/ApproveQuestionnaire") {
        GetEmployeesWithSurveyToVerifiy();
    }

    viewAssignedQuestions();

    $scope.showPopup = false;
    $scope.errorMessage = false;

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

    function GetBusinessUnitList() {

        var employeeList = crudservice.GetBusinessUnitList();

        employeeList.then(function (data) {

            $scope.DepartmentList = data.data;

            //$scope.Department = "---Please Select---";
        });



    }


    $scope.GetSurveyStatus = function (model) {


      

        if (model = "") {
            $scope.StatusList.push(status);
        } else {

            $scope.removeItem = function (status) {

                $scope.StatusList.splice(index, 1);

                
            }
        }

    }

    $scope.showCommentDiv = function (action) {

        $scope.EnteredSurveyComment = $scope.EnteredSurveyComment;

        if (action == "No") {

            $scope.divSurveyComment = true;

        } else {

            $scope.divSurveyComment = false;
        }

        if (action == "---Please Select---") {

            $scope.btnApproveQuestionDisable = true;

        } else {
            $scope.btnApproveQuestionDisable = false;
        }


    }

    $scope.HideQuestionResponseDiv = function () {
        $scope.panelSingleQuestion = false;
    }

    $scope.ShowEmployeeTable = function () {

        $scope.divApproveEmployeeQuestion = false;

        $scope.divEmployeeTable = true;

        $scope.employeeTable = true;

        $scope.panelSingleQuestion = false;

        $scope.divBackButton = false;

        $scope.divApproveEmployeeQuestion = false;

        $scope.divApproveEmployeeQuestion = false;

        $scope.BusinessUnit = undefined;

       $scope.divShowDepartmentFilter = true;
        
       

        if (window.location.pathname == "/Admin/ReviewSurvey" || window.location.pathname == "/PAS/Admin/ReviewSurvey") {

            GetEmployeesWithSurveysForReviewAll();


        } else {

            GetEmployeesWithSurveyToVerifiy();
        }

        
    }



    $scope.DisableSaveButton = function () {

        if ($scope.EnteredSurveyComment == "") {

            $scope.btnApproveQuestionDisable = true;

        } else {

            $scope.btnApproveQuestionDisable = false;
        }
    };

    $scope.validateForm = function (action) {

        if ($scope.EnteredSurveyComment == undefined && action == "No") {

            alert("Please enter comment and try again");

            $scope.divErrorMessage = true;

            $scope.clicked = false;

            $scope.lblApproveQuestionErrorMessageAlert = "Please enter comment and try again";


            $scope.btnApproveQuestionDisable = true;



            return false;
        }
        else {

            $scope.btnApproveQuestionDisable = false;

            $scope.lblApproveQuestionErrorMessageAlert = "";

            $scope.divErrorMessage = false;

            $scope.ApproveQuestion();
        }




    }

    function loadEmployees() {

        //$scope.btnApproveQuestionDisable = true;
        //$scope.surveyQuestionTable = false;

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurvey();

        surveyQuestions.then(function (data) {
            //success

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $scope.Employees = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

        },
        function () {

            $scope.divErrorMessage = true;

            $scope.lblApproveQuestionErrorMessageAlert = "Please enter comment and try again";
        });
    }

    function GetEmployeesWithSurveysForReviewAll() {

        //$scope.btnApproveQuestionDisable = true;
        //$scope.surveyQuestionTable = false;

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurveyAll();

        surveyQuestions.then(function (data) {
            //success

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            //$scope.EmployeesList = data.data;

            $scope.Employees = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

        },
        function () {

            $scope.divErrorMessage = true;

            $scope.lblApproveQuestionErrorMessageAlert = "Please enter comment and try again";
        });
    }

    function GetEmployeesWithSurveyToVerifiy() {

        //$scope.btnApproveQuestionDisable = true;
        //$scope.surveyQuestionTable = false;

        $('#loading').show();

        var surveyQuestions = crudservice.GetEmployeesWithSurveyToVerifiy();

        surveyQuestions.then(function (data) {
            //success

            $scope.employeeTable = true;

            $scope.divEmployeeTable = true;

            $scope.EmployeesListVerify = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

        },
        function () {

            $scope.divErrorMessage = true;

            $scope.lblApproveQuestionErrorMessageAlert = "Please enter comment and try again";
        });
    }

    $scope.Actions = ["Yes", "No"];


    //save form data
    $scope.ApproveQuestion = function (e) {

        var Survey = {
            Question: $scope.Question,
            KPIDesc: $scope.KPIDesc,
            RatersString: $scope.RatersString
        };

        var saverecords = crudservice.ApproveQuestion(Survey);

        saverecords.then(function (d) {

            $scope.Question = d.data;

            loadEmployees();

            if (d.data == 1) {

                $scope.divErrorMessage = false;

                $scope.alertApproveQuestionSuccess = true;

                $scope.approveQuestionSuccessLabel = "Saved Successfully";

                $window.alert("Please enter your name!");

                e.preventDefault();
            }


        })
    };

    $scope.LoadQuestions = function () {

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $scope.divEmployeeTable = false;

        //$scope.divApproveEmployeeQuestion = true;

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


        var id = emp.EmployeeFirstName + ' ' + emp.EmployeeSurname;

        

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $scope.divEmployeeTable = false;

        //$scope.divApproveEmployeeQuestion = true;

        $scope.divBackButton = true;

        $scope.ModelSurveyFor = survey_For;

        //$scope.btnApproveQuestionDisable = true;

        var surveyQuestions = crudservice.LoadQuestionsPerEmployee($scope.ModelSurveyFor);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;
        },
        function () {
            //Show error message
        });
    }

    $scope.LoadQuestionsPerEmployeeForReview = function (emp, survey_For) {

        $scope.divShowDepartmentFilter = false;

        var id = emp.EmployeeFirstName + ' ' + emp.EmployeeSurname;

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = false;

        $scope.divEmployeeTable = false;

        $scope.divApproveEmployeeQuestion = false;

        $scope.divBackButton = true;

        $scope.ModelSurveyFor = survey_For;

        //$scope.btnApproveQuestionDisable = true;

        $('#loading').show();

        $scope.divApproveEmployeeQuestion = false;

        $scope.surveyQuestionTable = false;

        var surveyQuestions = crudservice.LoadQuestionsPerEmployeeForReview($scope.ModelSurveyFor);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            $scope.surveyQuestionTable = true;

            $scope.divApproveEmployeeQuestion = true;
        },
        function () {
            //Show error message
        });
    }

    $scope.LoadQuestionsPerEmployeeForVerification = function (emp, survey_For) {

        $('#loading').show();

        //$('#DivLoadingProgress').addClass('alertBg');

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = false;

        $scope.divEmployeeTable = false;

        $scope.divApproveEmployeeQuestion = false;

        $scope.divBackButton = true;

        $scope.ModelSurveyFor = survey_For;

        //$scope.btnApproveQuestionDisable = true;

        var surveyQuestions = crudservice.LoadQuestionsPerEmployeeForVerification($scope.ModelSurveyFor);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;

            $scope.divApproveEmployeeQuestion = true;

            $scope.surveyQuestionTable = true;

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');
        },
        function () {
            //Show error message
        });
    }


    $scope.ReviewAllQuestions = function () {

        $('#loading').show();

        $scope.divApproveEmployeeQuestion = false;

        $scope.surveyQuestionTable = false;

        var SurveyReview = {

            Question: "",

            Survey_For: $scope.ModelSurveyFor,

            Rating: ""
        };

        var surveyReviewAllQuestions = crudservice.ReviewAllQuestions(SurveyReview);

        surveyReviewAllQuestions.then(function (data) {

            var surveyQuestions = crudservice.LoadQuestionsPerEmployeeForReview($scope.ModelSurveyFor);

            surveyQuestions.then(function (data) {

                $scope.Questions = data.data;

                $scope.surveyReviewedAlertSucess = true;

                $('#loading').hide();

                $('#DivLoadingProgress').removeClass('alertBg');

                $scope.divApproveEmployeeQuestion = true;

                $scope.surveyQuestionTable = true;
            },
            function () {
                //Show error message
            });

            if (data.length > 0) {

            }
        },
        function () {
            //Show error message
        });
    }


    $scope.ManagerVerifyAllQuestions = function () {

        $('#loading').show();

        $scope.divApproveEmployeeQuestion = false;

        $scope.divApproveEmployeeQuestion = false;

        var SurveyReview = {

            Question: "",

            Survey_For: $scope.ModelSurveyFor,

            Rating: ""
        };

        var surveyReviewAllQuestions = crudservice.ManagerVerifyAllQuestions(SurveyReview);

        surveyReviewAllQuestions.then(function (data) {

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            var surveyQuestions = crudservice.LoadQuestionsPerEmployeeForVerification($scope.ModelSurveyFor);

            surveyQuestions.then(function (data) {

                $scope.Questions = data.data;

                $scope.divApproveEmployeeQuestion = true;

                $scope.divApproveEmployeeQuestion = true;

                $('#loading').hide();

                $('#DivLoadingProgress').removeClass('alertBg');
            },
            function () {
                //Show error message
            });

            if (data.length > 0) {

            }
        },
        function () {
            //Show error message
        });
    }


    $scope.ManagerVerifySingleQuestion = function (question) {

        $('#loading').show();

        $scope.divApproveEmployeeQuestion = false;

        $scope.divApproveEmployeeQuestion = false;

        var SurveyReview = {

            Question: question,

            Survey_For: $scope.ModelSurveyFor
        };

        var revifySingleQuestion = crudservice.ManagerVerifySingleQuestion(SurveyReview);

        revifySingleQuestion.then(function (data) {

            ReloadQuestionsAfterVerification();

            $('#loading').hide();

            $('#DivLoadingProgress').removeClass('alertBg');

            $scope.divApproveEmployeeQuestion = true;

            $scope.divApproveEmployeeQuestion = true;

            if (data.length > 0) {

            }
        },
        function () {
            //Show error message
        });
    }

    $scope.GetEmployeeSurveysByBusinessUnit = function (businessUnit) {

        var employeeInfoList = crudservice.GetEmployeesWithSurveyByDepartment(businessUnit);

        employeeInfoList.then(function (data) {

            $scope.Employees = data.data;
        },
        function () {
            //Show error message
        });
    }

    $scope.ShowAllEmployees = function () {

        GetEmployeesWithSurveysForReviewAll();
    }

    $scope.ReFreshQuestionList = function () {

        alert($scope.ModelSurveyFor);
        var surveyQuestions = crudservice.LoadQuestionsPerEmployee($scope.ModelSurveyFor);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;
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

    function ReloadQuestionsAfterVerification() {

        var surveyQuestions = crudservice.LoadQuestionsPerEmployeeForVerification($scope.ModelSurveyFor);

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;          
        },
        function () {
            //Show error message
        });

    }  


    $scope.GetSingleQuestion = function (question, raters) {

        $scope.employeeTable = false;

        $scope.surveyQuestionTable = true;

        $scope.panelSingleQuestion = true;

        $scope.divSurveyComment = false;

        $scope.QuestionResponse = true;

        //Show Complete Questionnaire Modal

        $scope.Question = question;

        $scope.RatersString = raters;


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





    //delete Employee record
    $scope.delete = function (UpdateEmpNo) {

        var deleterecord = crudservice.delete($scope.UpdateEmpNo);
        deleterecord.then(function (d) {
            var Employee = {
                EmpNo: '',
                EmpName: '',
                Salary: '',
                DeptName: '',
                Designation: ''
            };
            loadEmployees();
            //Show error message
        });
    }
});

