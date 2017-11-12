
NotifyRatersApp.service('EmailNotificationService', function ($http) {

    this.GetEmployeeInformation = function (adGroupName) {

        var endPoint = "/api/raterhelper/GetEmployeeInformation?adGroupName=" + adGroupName;

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        return $http.get(endPoint);
    }

    this.GetBusinessUnitList = function () {
        
        var endPoint = "/api/employeesurveyhelper/GetBusinessUnitList";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }
        return $http.get(endPoint);
    }
    

 
});