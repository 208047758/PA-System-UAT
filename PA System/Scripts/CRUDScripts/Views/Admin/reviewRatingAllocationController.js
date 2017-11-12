
var reviewRatingAllocationApp = angular.module('reviewRatingAllocationApp', ['datatables']);

reviewRatingAllocationApp.controller('reviewRatingAllocationController', function ($scope, reviewRatingReportService) {

    $scope.divShowRatersDropDown = false;

    $scope.divShowBusinessUnitDropDown = false;

    $scope.divShowCompletionStatusTable = true;

    $scope.showStatusCompletionTable = true;    

    GetSurveyCompletionStatusReportPerRater();
    
    LoadRatersDropDown();

    LoadBusinessUnitList();

    $('#loading').hide();

    $('div').removeClass('alertBg');


    function LoadRatersDropDown() {

        $('#loading').show();

        var raterList = reviewRatingReportService.GetEmployeesListToFilterReport();

        raterList.then(function (data) {

            $scope.RaterStatusList = data.data;

            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

        });
    }

    function LoadBusinessUnitList() {

        var businessUnitList = reviewRatingReportService.GetBusinessUnitList();

        businessUnitList.then(function (data) {

            $scope.DepartmentList = data.data;
        })

    }

    function GetSurveyCompletionStatusReportPerRater() {

        var surveyCompletionStatusReport = reviewRatingReportService.GetSurveyCompletionStatusReportPerRater("All", false);

        surveyCompletionStatusReport.then(function (data) {

            $scope.SurveyCompletionStatusList = data.data;

            $('#divShowAllEmployees').show();
        });

    }

    $scope.GetSurveyCompletionStatusReportPerRaterAll = function ()
    {

        var showAll = false;

        if ($('#chkShowAll').is(':checked')) {

            showAll = true;
        } else {
            showAll = false;
        }

        var surveyCompletionStatusReport = reviewRatingReportService.GetSurveyCompletionStatusReportPerRater("All", showAll);

        surveyCompletionStatusReport.then(function (data) {

            $scope.SurveyCompletionStatusList = data.data;
        });
    }


    $scope.GetSurveyCompletionStatusReportBySelectedRater = function (rater) {
        

        var showAll = false;

        if ($('#chkShowAll').is(':checked')) {

            showAll = true;
        } else {
            showAll = false;
        }

        var surveyCompletionStatusReport = reviewRatingReportService.GetSurveyCompletionStatusReportBySelectedRater(rater.Rater);

        surveyCompletionStatusReport.then(function (data) {

            $scope.SurveyCompletionStatusList = data.data;

            $scope.divShowCompletionStatusTable = true;

            $scope.showStatusCompletionTable = true;

            $('#divShowAllEmployees').hide();
        });
    }

    $scope.GetSurveyCompletionStatusReportBySelectedbusinessUnit = function (businessUnit) {
                

        var surveyCompletionStatusReport = reviewRatingReportService.GetSurveyCompletionStatusReportBySelectedbusinessUnit(businessUnit);

        surveyCompletionStatusReport.then(function (data) {

            $scope.SurveyCompletionStatusList = data.data;

            $scope.divShowCompletionStatusTable = true;

            $scope.showStatusCompletionTable = true;

            $('#divShowAllEmployees').hide();
        });
    }

    $scope.ShowReportWithRatersPerBusinessUnit = function () {

        $scope.divShowRatersDetailsReport = false;

        $scope.divShowViewDetailsButton = false;

        $scope.divShowDetailsReport = false;

        $('#divGenerateReport').show();

    }

    $scope.ShowRaterDiv = function () {

        $scope.divShowRatersDropDown = true;

        $scope.divShowBusinessUnitDropDown = false;

        $scope.divShowCompletionStatusTable = false;

        $scope.showStatusCompletionTable = false;

        $('#divShowAllEmployees').hide();


    }

    $scope.ShowDepartmentDiv = function () {

        $scope.divShowRatersDropDown = false;

        $scope.divShowBusinessUnitDropDown = true;

        $scope.divShowCompletionStatusTable = false;

        $scope.showStatusCompletionTable = false;

        $('#divShowAllEmployees').hide();


    }

    $scope.ShowAll = function () {

        $scope.divShowRatersDropDown = false;

        $scope.divShowBusinessUnitDropDown = false;

        $scope.divShowCompletionStatusTable = true;

        $scope.showStatusCompletionTable = true;

        $('#divShowAllEmployees').show();
        
        GetSurveyCompletionStatusReportPerRater();
    }

    $scope.GetSurveysPerRater = function (rater)
    {
        debugger;
        var surveysDetailsView = reviewRatingReportService.GetSurveysAssignedPerRater(rater);

        surveysDetailsView.then(function (data) {

            $scope.SurveysDetailsList = data.data;

            $('#divShowAllEmployees').hide();

            $('#popupMessage').show();
        });
    }


  

});