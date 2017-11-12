var surveyApp;
(function () {
    surveyApp = angular.module('PA_SurveyApp', []);

    $('#orderModal').modal({
        keyboard: true,
        backdrop: "static",
        show: false,

    }).on('show', function () {
        var getIdFromRow = $(event.target).closest('tr').data('id');
        //make your ajax call populate items or what even you need
        $(this).find('#orderDetails').html($('<b> Order Id selected: ' + getIdFromRow + '</b>'))
    });

})();