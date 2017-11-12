angular.module("RatingComparisonApp", [])

       .service("ratingComparisonService", function ($http) {

           return {

               GetRatingComparisonReport: function () {

                   return $http.get("/api/employeesurveyhelper/ViewRatingComparison");
               }
           }

       })
.controller("RatingComparisonController", function ($scope, ratingComparisonService) {

    ratingComparisonService.GetRatingComparisonReport().success(function (data) {

        console.log(data);
        
        console.log("Ratee " + data[0].SurveryForList[0].Text);

        $scope.RatingComparisonReport = data;

        console.log(data.data);

    })
    .error(function (error) {

        console.log("Printing an error");

        console.log(error);
    });
})