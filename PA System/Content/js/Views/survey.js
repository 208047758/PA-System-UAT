var app = angular.module('MyApp', ['datatables']);

app.controller('homeCtrl', function ($scope, $http) {
       
    $scope.tablePreQuestions = true;

    function viewAssignedQuestions() {


        //$scope.assignedQuestionsTable = true;

        //var surveyQuestions = $http.get("/api/employeesurveyhelper/GetEmployeeSurveyQuestions");

        //surveyQuestions.then(function (data) {

        //    $scope.Questions = data.data;
        //});


    }

    function surveyQuestionAPIGet()
    {


        return $http.get("/api/employeesurveyhelper/GetEmployeeSurveyQuestions");
    }

    $scope.GetMoreQuestions = function () {

        var surveyQuestions = $scope.surveyQuestionAPIGet();

        surveyQuestions.then(function (data) {

            $scope.Questions = data.data;


        });

    }


});


//app.controller('homeCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', LoadQuestions]);


//function LoadQuestions($scope, $http, DTOptionsBuilder, DTColumnBuilder) {

//    $scope.dtColumns = [

//        DTColumnBuilder.newColumn("Question", "Question"),
//        DTColumnBuilder.newColumn("KPIDesc", "KPI"),
//        DTColumnBuilder.newColumn("RatersString", "Raters")
//    ]

//    $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {

//        url: "/api/employeesurveyhelper/GetEmployeeSurveyQuestions",

//        type: "GET"
//    })
//  .withPaginationType('full_numbers')

//  .withDisplayLength(10);

   
    
//}




