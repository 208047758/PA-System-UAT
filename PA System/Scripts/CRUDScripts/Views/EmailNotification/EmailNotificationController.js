

var NotifyRatersApp = angular.module('EmailNotificationApp', ['datatables']);

NotifyRatersApp.controller('EmailNotificationController', function ($scope, EmailNotificationService) {

    $scope.showPopup = false;

    $scope.notifyRatersString = false;

    $scope.errorMessage = false;

    $scope.divShowSurveyRaters = true;

    $scope.divShowSurveyRaters = true;

    $scope.divShowSurveyStatus = true;

    populateBusinessUnits();

    $scope.divShowSurveyRaters = false;

    $scope.divBusinessUnits = true;

    $scope.divShowSurveyRatersToNotify = true;

    LoadBusinessUnitList();

    $scope.isPopupVisble = function () {

        $scope.showPopup = true;

    };

    $scope.showRaters = function () {

        $scope.divShowSurveyRaters = true;

    };


    $scope.hidePrompt = function () {

        $scope.errorMessage = false;

        $scope.showPopup = false;

    };
    $scope.deleteRecord = function () {

        $scope.hidePrompt();
    };

    $scope.SendEmail = function ()
    {
        
        var sendMail = EmailNotificationService.SendEmailRaters();

        sendMail.then(function (data) {

            alert("Email send successfully");
            //$scope.DepartmentList = data.data;

        })
    }

    function LoadBusinessUnitList() {
               

        var businessUnitList = EmailNotificationService.GetBusinessUnitList();

        businessUnitList.then(function (data) {

            $scope.DepartmentList = data.data;

        });

    }

    function populateBusinessUnits() {

        var employeeList = EmailNotificationService.GetEmployeeInformation("DG-Futuregrowth");

        employeeList.then(function (data) {

            //$scope.EmployeeList = data.data;

            //$scope.Department = "---Please Select---";
        });

    

    }
});


