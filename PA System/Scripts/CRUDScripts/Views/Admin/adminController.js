
var printReportApp = angular.module('printReportApp', ['datatables']);

printReportApp.controller('adminController', function ($scope, adminService) {

    $scope.divShowDepartmentFilter = false;

    $scope.divShowRaters = true;

    LoadRatersDropDown();

    LoadBusinessUnitList();

    $scope.divShowViewDetailsButton = false;

    $scope.divIncludeRaters = true;

    $scope.divShowRatersDetailsReport = false;


    //function LoadRatersDropDown() {
    //    
       

    //    $('#loading').show();

    //    var raterList = adminService.GetRaterList();

    //    raterList.then(function (data) {
    //        
    //        $scope.RaterStatusList = data.data;

    //        $('#loading').hide();

    //        $('div').removeClass('alertBg');

    //    }, function (response) {

    //    });
    //}

    function LoadRatersDropDown() {


        $('#loading').show();

        var raterList = adminService.GetEmployeesListToFilterReport();

        raterList.then(function (data) {
           
            $scope.RaterStatusList = data.data;
            
            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

        });
    }

    

    function LoadBusinessUnitList() {

        var businessUnitList = adminService.GetBusinessUnitList();

        businessUnitList.then(function (data) {

            $scope.DepartmentList = data.data;
        })

    }


    $scope.ShowReportWithRaters = function () {
       
        if ($('#optReportRater option:selected').text() != "---please select---" ) {

            var getReportContent = adminService.GetPrintRatingReport($('#optReportRater option:selected').text());

            getReportContent.then(function (data) {

                

                $scope.divShowRatersDetailsReport = true;

                $scope.divShowViewDetailsButton = true;

                $scope.divShowDetailsReport = true;

                $('#divReportPerRater').show();

                $('#divGenerateReport').show();

                $scope.RatingReport = data.data;              

                //if ($('#chkIncluderaters').is(':checked') == true) {
                //    $scope.divShowDetailsReport = true;
                //} else {
                //   // $scope.divShowDetailsReport = false;
                //}

                $('#loading').hide();

                $('div').removeClass('alertBg');

            }, function (response) {


            });
        }
        
        if ($('#chkIncluderaters').is(':checked') == true) {

            $scope.divShowRatersDetailsReport = true;

        } else {

            $scope.divShowRatersDetailsReport = false;
        }

      

    
    }

    $scope.ShowReportWithRatersPerBusinessUnit = function () {
        

        $scope.divShowRatersDetailsReport = false;

        $scope.divShowViewDetailsButton = false;

        $scope.divShowDetailsReport = false;   

        $('#divGenerateReport').show();

    }

    $scope.ShowRaterDiv = function () {

        $scope.divShowRaters = true;

        $scope.divShowDepartmentFilter = false;

        $scope.divIncludeRaters = true;

    }

    $scope.ShowDepartmentDiv = function () {

        $scope.divShowRaters = false;

        $scope.divShowDepartmentFilter = true;

        $scope.divIncludeRaters = true;

        $('#divReportPerRater').hide();
    }

    $scope.ShowAll = function () {

        $scope.divShowRaters = false;

        $scope.divShowDepartmentFilter = false;
    }
   
    $scope.GetGenerateRatingReport = function () {      

        var includeRaters = $('#chkIncluderaters').is(':checked'); 

        $('#loading').show();
        
        var getReportContent = adminService.GetPrintRatingReport($('#optReportRater option:selected').text());

        getReportContent.then(function (data) {           

            $scope.divShowViewDetailsButton = true;

            $scope.divShowDetailsReport = true;

            if ($('#optRaterName').is(':checked')) {
                $('#divReportPerRater').show();
            } else {
                $('#divReportPerRater').hide();
            }

            $('#divGenerateReport').show();

            $scope.RatingReport = data.data;

            $scope.ModelRaterName = $scope.RatingReport[0].Survey_For;

            if (includeRaters == true) {

                $scope.divShowRatersDetailsReport = true;

            } else {
                   
                $scope.divShowRatersDetailsReport = false;
            }


            $('#loading').hide();

            $('div').removeClass('alertBg');

        }, function (response) {

         
        });
        if ($('#chkIncluderaters').is(':checked') == true) {

            $scope.divShowRatersDetailsReport = true;

        } else {

            $scope.divShowRatersDetailsReport = false;
        }
    }
    
});