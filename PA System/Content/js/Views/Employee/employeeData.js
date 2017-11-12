"use strict";

$(document).ready(function () {

    $("#saveEmployeeDataStatusAlert").hide();

    $("#divEmployeeDataErrorMessageAlert").hide();

    $("#btnUploadEmployeeData").on("click", function (e) {

        //e.preventDefault();

        // $("#modalEployeeDataUpload").modal("show");

        //$("#modalErrorContent").append('<li>      Error message 1</li>');

        $("#divEmployeeDataErrorMessageAlert").hide();

        $("#saveEmployeeDataStatusAlert").hide();


    });


    $(document).on('change', ':file', function () {

        $("#btnUploadEmployeeData").attr("disabled", true);

        var input = $(this), label = input.val().replace(/\\/g, '/').replace(/.*\//, '');

        input.trigger('fileselect', [1, label]);        

    });

    //$(window).bind("load", function () {

    //    var headerColumnsErrorMessage = $("#CSVFileStatus").val();

    //    if (headerColumnsErrorMessage != "") {

    //        $("#modalEployeeDataUpload").modal("show");

    //        $("#modalErrorContent").append('<li>' + headerColumnsErrorMessage + '</li>');

    //        $('#modalEployeeDataUpload').modal('show')
    //    }

    //});

    $(function () {
        
        var headerColumnsErrorMessage = $("#CSVFileStatus").val();

        if (headerColumnsErrorMessage != "Success" && headerColumnsErrorMessage != "") {

            // $("#modalEployeeDataUpload").modal("show");

            $("#divEmployeeDataErrorMessageAlert").show();

            $("#lblEmployeeDataErrorMessageAlert").text(headerColumnsErrorMessage);
        }
        else if (headerColumnsErrorMessage == "Success") {

            $("#divEmployeeDataErrorMessageAlert").hide();

            $("#saveEmployeeDataStatusAlert").show();

            $("#lblEmployeeDataErrorMessageAlert").text(headerColumnsErrorMessage);
        }

    });

    $(':file').on('fileselect', function (event, numFiles, label) {

        $("#btnUploadEmployeeData").attr("disabled", false);

        $("#fileUploadEmployeeDataCaption").text("");

        $("#fileUploadEmployeeDataCaption").text(label);
        
        var fileExtension = label.split('.')[1];

        if (fileExtension != "csv") {

            $("#lblEmployeeDataErrorMessageAlert").text("Invalid file, please upload a valid csv file and try again (.csv)");

            $("#divEmployeeDataErrorMessageAlert").show();

            $("#btnUploadEmployeeData").attr("disabled", true)
        }

        if (fileFormatStatusMessage != "") {

        }

    });


});