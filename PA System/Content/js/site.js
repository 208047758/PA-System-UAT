"use strict";

var EmployeeSurvey = (function () {

    var dthelper;

    var dataTablehelper;

    var approveQuestiondthelper;

    var preViewQuestionsdthelper;

    var questionTableHelper;

    var htmlHelper = {

        "createCheckboxDropDownList": function (DropDownListId) {
            
            $(DropDownListId).multipleSelect({
                textTemplate: function ($el) {
                    return $el.html();
                },
                filter: true,
                placeholder: "Please Select"
            });
        },

        "getSelectedValues": function (DropDownListId) {
            return $(DropDownListId).multipleSelect("getSelects", "value");
        },

        "setDropdownValues": function (DropdownListId, arrayValues) {

            $(DropdownListId).multipleSelect("setSelects", arrayValues);

        },

        "getSurveyRaterObject": function () {

            var raters = "";

            var items = document.getElementsByClassName("ms-choice");
            for (var i = 0; i < items.length; i++) {

                raters += items[i].innerText;

                break;
            }

            var surveyObject = {

                "Survey_For": $("#selectedRatee").val(),

                "Question": $("#updateRaterSelectedQuestion").text(),

                "Rater_List": raters
                
            };

            return JSON.stringify(surveyObject);

        },
    }

    var surveyHelper = {

        "getSurveyRaters": function () {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/raterhelper/GetEmployeeInformationForNotificationByTeam?adGroupName=" + businessUnit;

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {

                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    //baseobj.push({ "Name": "-- Select to change/amend Rater(s) ---", "Value": 0 });

                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Survey_For, "Value": dropDownData[i].Survey_For });
                    }

                    //$("#divEmployeesWithSurveyToNotify").show();

                    //var mySelect = $('#drpSurveyFor');
         
                    //mySelect.empty();

                    //mySelect.append($('<option></option>').val("0").html("---Please Select---"));

                    //$.each(baseobj, function (val, text) {
                    //    mySelect.append(
                    //        $('<option></option>').val(text.Value).html(text.Name)
                    //    );
                    //});



                    //$("#drpSurveyFor option:selected").text("---Please Select---");

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) { error(e) });

            });
        },

        "getLoadSurveyFor": function () {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/raterhelper/GetEmployeeInformationForNotificationByTeam?adGroupName=" + $("#drpSelectedBusinessUnit option:selected").text();

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }
               
                $.get(endPoint, function (data) {
                    
                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    //baseobj.push({ "Name": "-- Select to change/amend Rater(s) ---", "Value": 0 });

                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Survey_For, "Value": dropDownData[i].Survey_For });
                    }

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) { error(e) });

                $("#divShowNextButton").hide();               

                //$("#divSelectedRatersToNotify").show();

                $("#divSendMail").show();
            });
        },

        "getSurveyRatersList": function () {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/employeehelper/GetSurveyCompletionStatusList";

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {
                    
                    var baseobj = [];

                    var dropDownData = data;
                    
                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Rater, "Value": dropDownData[i].Rater });
                    }

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) { error(e) });
            });
        },

        "getSurveyRatersToBeNotified": function () {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/raterhelper/GetEmployeeInformationBySurvey?selectedSurveys=" + $("#drpSurveyFor option:selected").text();

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {
                    
                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    //baseobj.push({ "Name": "-- Select to change/amend Rater(s) ---", "Value": 0 });

                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Survey_For, "Value": dropDownData[i].Survey_For });
                    }

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) { error(e) });

                $("#divShowNextButton").hide();

                $("#divSelectedRatersToNotify").show();

                $("#divSendMail").show();
            });
        },

        "getLoadAllRaters": function () {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/employeehelper/GetSurveyCompletionStatusList";

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }
                
                $.get(endPoint, function (data) {
                    
                    var baseobj = [];

                    var dropDownData = data;
                    
                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Rater, "Value": dropDownData[i].Rater });
                    }
                    
                    var mySelect = $('#drpRater');

                    mySelect.empty();

                    $.each(baseobj, function (val, text) {
                        mySelect.append(
                            $('<option></option>').val(text.Value).html(text.Name)
                        );
                    });

                    htmlHelper.createCheckboxDropDownList(mySelect);

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) {
                    console.log(e.error);
                    error(e);
                    alert(e.error);
                });

                $("#divShowNextButton").hide();

                $("#divSelectedRatersToNotify").show();

                $("#divSendMail").show();
            });
        },

        "getSurveyForListByRater": function (rater) {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/raterhelper/GetSurveyForListByRater?rater=" + rater;

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }
                
                $.get(endPoint, function (data) {

                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    
                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Survey_For, "Value": dropDownData[i].Survey_For });
                    }

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) {
                    


                });
            });
        },
       

        "LoadEmployeeSurveyList": function () {

            return new Promise(function (surveyListObject, error) {

                var endPoint = "/api/raterhelper/GetEmployeeInformationForNotificationByTeam?adGroupName=" + $("#drpSelectedBusinessUnit option:selected").text();

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {
                   
                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    baseobj.push({ "Name": "---Please Select---", "Value": "---Please Select---" });

                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Survey_For, "Value": dropDownData[i].Survey_For });
                    }

                    var mySelect = $('#drpSurveyFor');                    

                    mySelect.empty();

                    $.each(baseobj, function (val, text) {
                        mySelect.append(
                            $('<option></option>').val(text.Value).html(text.Name)
                        );
                    });

                    $("#divEmployeesWithSurveyToNotify").show();

                   // $('input:checkbox').attr('checked', false);

                    $("#drpSurveyFor option:selected").text("---Please Select---");

                    var ratersSlected = $("#selectedRatersString").val();

                    // var selectRatersArray = ratersSlected.split(',');

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) {
                    
                    error(e);


                });

               // $("#divShowNextButton").hide();

                //$("#divSelectedRatersToNotify").show();

                //$("#divSendMail").show();
            });
        },

        "getSurveyRatersToUpdateSurveyRaters": function () {

            return new Promise(function (surveyListObject, error) {

                //var endPoint = "/api/raterhelper/GetEmployeeInformation?adGroupName=" + "DG-Futuregrowth";
                
                var endPoint = "/api/raterhelper/GetAllEmlpoyees";
                

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {

                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    //baseobj.push({ "Name": "-- Select to change/amend Rater(s) ---", "Value": 0 });

                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Employee, "Value": dropDownData[i].Employee });
                    }

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) { error(e) });

            });
        },

        "NotifyRaters": function () {

            return new Promise(function (surveyListObject, error) {
            

                var endPoint = "/api/raterhelper/NotifyRaters?selectedSurveys=" + htmlHelper.getSelectedValues("#drpSurveyFor");

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {
                    
                    var baseobj = [];

                    var dropDownData = JSON.parse(data);

                    //baseobj.push({ "Name": "-- Select to change/amend Rater(s) ---", "Value": 0 });

                    for (var i = 0; i < dropDownData.length; i++) {

                        baseobj.push({ "Name": dropDownData[i].Survey_For, "Value": dropDownData[i].Survey_For });
                    }

                    if (baseobj.length > 1) surveyListObject(baseobj);

                }).fail(function (e) { error(e) });

                $("#divSelectedRatersToNotify").show();
            });
        },
        "ExportRatingReportToPDF": function () {
            
            return new Promise(function (surveyListObject, error) {

                var includeRater = false;

                if ($("input[type=checkbox]:checked").length > 0) {
                    includeRater = true;
                } else {
                    includeRater = false;
                }

                var endPoint = "ExportToPDF?surveyFor=" + $("#optReportRater option:selected").text();

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.get(endPoint, function (data) {
                                    

                }).fail(function (e) { error(e) });

           
            });
        },

        "SaveUpdateRaters": function () {

            return new Promise(function (returnData, error) {
                
                var endPoint = "/api/raterhelper/UpdateSurveyRaters";

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $.post(endPoint, JSON.parse(htmlHelper.getSurveyRaterObject()), function (data) {

                    
                    $("#btnRealodQuestion").trigger('click');


                    $("#divShowUpdateRatersSuccess").show();

                    $("#saveUpdateRaterStatusAlert").show();

                    $("#divShowUpdateRatersWarning").hide();
                    if (data.length > 0) {

                        //returnData(JSON.parse(data));
                    }

                }).fail(function (e) { error(e) });

            });

        },


        "SenMail": function () {
            
            return new Promise(function (surveyListObject, error) {
                
                var surveyFor = "";

                var endPoint = "";

                if ($("#drpSelectOption option:selected").text() == "By Rater") {

                    if ($("#drpRaterList option:selected").text() == "---Please Select---") {

                        $("#divShowAlertWarning").show();

                        $("#lblAlertWarning").text("Please select a rater");

                        $('#loading').hide();

                        $('div').removeClass('alertBg');

                        return false;

                    }

                    surveyFor = htmlHelper.getSelectedValues("#drpRater");
                    
                    endPoint = "/api/emailnotificationhelper/NotifyRatersByRater?rater=" + $("#drpRaterList option:selected").text() + "&selectedSurveyFor=" + htmlHelper.getSelectedValues("#drpRater");

                }else {
                    surveyFor = $("#drpSurveyFor option:selected").text();

                    //if ($("#drpRater option:selected").text() == "" || $("#drpRater option:selected").text() == "Please Select") {

                    //    $("#divShowAlertWarning").show();

                    //    $("#lblAlertWarning").text("Please select rater(s)  from a selection list");

                    //    $('#loading').hide();

                    //    $('div').removeClass('alertBg');

                    //    return false;

                    //}

                    endPoint = "/api/emailnotificationhelper/NotifyRaters?selectedRaters=" + htmlHelper.getSelectedValues("#drpRater") + "&surveyFor=" + surveyFor;
                }               

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                $('#loading').show();

                $.get(endPoint, function (data) {

                    $('#loading').hide();

                    $('div').removeClass('alertBg');

                    $("#divEmailSentSuccessAlert").show();

                    $("#divSelectedRatersToNotify").hide();

                    $("#divRaterEmployeeList").hide();

                    $("#divSelectedRatersToNotify").hide();

                    $("#drpSelectedBusinessUnit").show();

                    $("#lblBusinessUnit").show();

                    $("#divSendMail").hide();

                    if ($("#drpSelectOption option:selected").text() == "By Rater") {

                        $("#successMessage").text($("#drpRaterList option:selected").text());

                        $("#drpSelectedBusinessUnit").show();

                        $("#lblBusinessUnit").show();

                        $("#divSelectedRatersToNotify").hide();

                        $("#divRaterEmployeeList").hide();

                        $("#divSelectedRatersToNotify").hide();

                        $("#divSendMail").hide();

                        $("#lblRatersToNotify").text("Survey For");

                    } else {
                        $("#successMessage").text(htmlHelper.getSelectedValues("#drpRater"));
                    }

                    $("#drpSelectedBusinessUnit").show();

                    $("#lblBusinessUnit").show();

                    $("#drpSelectOption").val("");

                    $("#drpSelectedBusinessUnit").val("");

                    $("#drpSurveyFor").val("---Please Select---");

                    $("#divEmployeesWithSurveyToNotify").hide();

                    $("#divSelectedRatersToNotify").hide();

                    if ($("#drpSelectOption option:selected").text() == "By Rater") {

                        $("#drpSelectedBusinessUnit").show();

                        $("#lblBusinessUnit").show();

                        $("#divSelectedRatersToNotify").hide();

                        $("#divRaterEmployeeList").hdie();

                        $("#divSendMail").hide();

                        $("#lblRatersToNotify").text("Select Rater(s) be notified");
                    }


                }).fail(function (e) {
                    error(e);
                    $('#loading').hide();
                    $('div').removeClass('alertBg');
                });

            });
        },

        "getQuestions": function () {

            var data = new FormData();

            var files = $("#fileUploadClientFile").get(0).files;

            // Add the uploaded image content to the form data collection
            if (files.length > 0) {
                data.append("UploadedFile", files[0]);
            }

            return new Promise(function (returnData, error) {

                var env = "";

                $("#modalQuestionPreview").modal('show')

                var endPoint = "/api/employeesurveyhelper/uploadFile";

                //any environment except local
                if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                    endPoint = "/PAS" + endPoint;
                } else {
                    endPoint = endPoint;
                }

                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: endPoint,
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (data) {

                        
                        $("#modalQuestionPreview").modal('hide')

                        $("#divPreviewtable").show();

                    },
                    error: function (data, status, message) {
                        
                        
                        alert('A jQuery error has occurred. Status: ' + status + ' - Message: ' + message);
                    },
                    error: function (data) {

                        $("#modalQuestionPreview").modal('hide')                    

                        if (data.status == 500) {

                            $("#btnUpload").attr("disabled", true);

                            $("#divErrorMessageAlert").show();

                            $("#lblErrorMessageAlert").text("Invalid Template Uploaded, please try again");



                        }



                    }
                });

                ajaxRequest.done(function (surveyList) {
                    
                    returnData(JSON.parse(surveyList));
                });

            });

        },

        //"SaveNewQuestion": function () {

        //    return new Promise(function (returnData, error) {

        //        $.post("/api/employeesurveyhelper/SaveQuestion", JSON.parse(htmlHelper.getSurveyObject()) ,function (data) {

        //            if (data.length > 0) {

        //                returnData(JSON.parse(data));
        //            }

        //        }).fail(function (e) { error(e) });

        //    });

        //},

        "PreviewData": function () {

            var endPoint = "/EmployeeSurvey/PreviewFile?filePath=" + $("#fileUploadClientFile").val() + "&ratee=" + $("#RateeName option:selected").val() + "&uploadedFile=" + data;

            //any environment except local
            if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
                endPoint = "/PAS" + endPoint;
            } else {
                endPoint = endPoint;
            }

            return new Promise(function (returnData, error) {

                $.post(endPoint, function (data) {

                    if (data.length > 0) {

                        returnData(JSON.parse(data));
                    }

                }).fail(function (e) { error(e) });

            });

        },

        "html": htmlHelper

    };

    function PostData() {

        var data = new FormData();

        var files = $("#fileUploadClientFile").get(0).files;

        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
        }

        var endPoint = "/api/employeesurveyhelper/uploadFile";

        //any environment except local
        if (window.location.href.indexOf("futuregrowth.co.za") > 0) {
            endPoint = "/PAS" + endPoint;
        } else {
            endPoint = endPoint;
        }

        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: endPoint,
            contentType: false,
            processData: false,
            data: data,
            success: function (data) {

            }
        });

        ajaxRequest.done(function (xhr, textStatus) {
        
        });
    }

    function getPreviewSurveyQuestions() {

        surveyHelper.getQuestions().then(function (e) {
            
            
            //$("#divErrorMessageAlert").show();

            $("#alertTotalNumberOfQuestionsError").hide();

            $("#alertTotalNumberOfQuestionsSuccess").hide();

            if (e.length < 30) {

                $("#alertTotalNumberOfQuestionsError").show();

                $("#lblAlertTotalNumberOfQuestionsError").text("Total Number of questions: " + e.length);

                
            } else if (e.length >= 30) {

                $("#alertTotalNumberOfQuestionsSuccess").show();

                $("#lblAlertTotalNumberOfQuestionsSuccess").text("Total Number of questions: " + e.length);
                
            }       

            $("#saveSurveryStatusAlert").hide();

            preViewQuestionsdthelper = $('#previewQuestionsTable').DataTable({

                "bProcessing": true,
                "responsive": false,
                "bSort": false,
                "bDestroy": true,
                "aaData": e,

                "aoColumns": [
                    { "mData": "Question" },
                    { "mData": "RateeName" }


                ]
            }), function (e) { console.error(e) };

        });
    };

    //function getEnteredQuestions() {

    //    surveyHelper.SaveNewQuestion().then(function (e) {



    //        dthelper = $('#tableEnteredQuestions').DataTable({

    //            "bProcessing": true,
    //            "responsive": true,
    //            "bSort": false,
    //            "bDestroy": true,
    //            "aaData": e,

    //            "aoColumns": [
    //                { "mData": "Question" },
    //                { "mData": "RatersString" },
    //                { "mData": "KPIDesc" }


    //            ]
    //        }), function (e) {
    //        };

    //    });
    //};

    function validateEmployeeUploadData(fileName) {
        if (window.location.pathname == "/Employee/UploadEmployeeData") {

            

            $("#btnUploadEmployeeData").attr("disabled", false);

            $("#fileUploadCaption").text("");

            $("#fileUploadEmployeeDataCaption").text(fileName);

            var fileExtension = fileName.split('.')[1];

            if (fileExtension != "csv") {

                $("#lblEmployeeDataErrorMessageAlert").text("Invalid file, please upload a valid csv file and try again (csv)");

                $("#divEmployeeDataErrorMessageAlert").show();

                $("#btnUploadEmployeeData").attr("disabled", true)

            }

        }

    }

    $(document).ready(function () {

        $('[data-toggle="popover"]').popover({
            container: 'body'
        });

        $("#divGenerateReport").hide();

        $("#divReportPerRater").hide();

        $("#divShowUpdateRatersWarning").hide();

        $("#divShowUpdateRatersSuccess").hide();

        $("#saveUpdateRaterStatusAlert").hide();

        $("#divShowAlertWarning").hide();

        $("#alertTotalNumberOfQuestionsError").hide();

        $("#alertTotalNumberOfQuestionsSuccess").hide();

        $("#divRatingReportSuccessWariningError").hide();

        $("#divSendMail").hide();

        $('#loading').hide();

        $('div').removeClass('alertBg');

         $("#divShowNextButton").hide();

        $("#divSelectedRatersToNotify").hide();

        $("#divEmployeesWithSurveyToNotify").hide();

        $("#divRaterEmployeeList").hide();

        $("#divPreviewtable").hide();

        $("#btnUpload").attr("disabled", true);

        $("#btnUploadEmployeeData").attr("disabled", true);

        $('#optAll').prop('checked', true);

        $('#optRaterName').prop('checked', true);

        $("#drpSelectOption").on('change', function () {

       

            if ($("#drpSelectOption option:selected").text() != "---Please Select---") {
            
            if ($("#drpSelectOption option:selected").text() == "By Rater") {

                $("#drpSelectedBusinessUnit").hide();

                $("#lblBusinessUnit").hide();

                $("#divSelectedRatersToNotify").hide();

                $("#divRaterEmployeeList").show();

                $("#lblRatersToNotify").text("Survey For");

                surveyHelper.getSurveyRatersList().then(function (e) {

                    var mySelect = $('#drpRaterList');

                    mySelect.empty();

                    mySelect.append($('<option></option>').val("0").html("---Please Select---"));

                    $.each(e, function (val, text) {
                        mySelect.append(
                            $('<option></option>').val(text.Value).html(text.Name)
                        );
                    });


                });

                //surveyHelper.getLoadAllRaters().then(function (e) {
             
                //});

            }
        }
            
        });

        $("#btnGenerateReport").on('click', function () {       

            if ($("#optRaterName").is(':checked')) {
                 $("#divReportPerRater").show();
            } else {
                $("#divReportPerRater").hide();
            }

           

            $("#divRatingReportSuccessWariningError").hide();

            if ($("#optReportRater option:selected").text() == "---please select---" && $("#drpBusinessUnit option:selected").text() == "---Please Select---") {

                $("#divRatingReportSuccessWariningError").show();

                $("#lblRatingReportErrorAlert").text("Please select an employee or business unit and try again");

                return false;

                e.preventDefault();
                

            } else {

                var includeRater = false;

                if ($("input[type=checkbox]:checked").length > 0) {
                    includeRater = true;
                } else {
                    includeRater = false;
                }

                $("#IncludeRaters").text(includeRater);

                $("#IncludeRaters").val(includeRater);
                   
                $("#Survey_For").text($("#optReportRater option:selected").text());

                $("#Survey_For").val($("#optReportRater option:selected").text());

                var surveryFor = "";

                surveryFor = $("#optReportRater option:selected").text();

                if ($("#optDepartment").is(':checked')) {

                    $("#Survey_For").text($("#drpBusinessUnit option:selected").text());

                    $("#Survey_For").val($("#drpBusinessUnit option:selected").text());

                    surveryFor = $("#drpBusinessUnit option:selected").text();
                }

                var data = { Survey_For: surveryFor, IncludeRaters: includeRater };

                //e.preventDefault();

            }

        });

        $("a").on('click', function () {

            var includeRater = false;

            if ($("input[type=checkbox]:checked").length > 0) {
                includeRater = true;
            } else {
                includeRater = false;
            }

            $("#IncludeRaters").text(includeRater);

            $("#IncludeRaters").val(includeRater);

            $("#Survey_For").text($("#optReportRater option:selected").text());

            $("#Survey_For").val($("#optReportRater option:selected").text());

            var surveryFor = $("#optReportRater option:selected").text();            

            var data = { Survey_For: surveryFor, IncludeRaters: includeRater };      

            //surveyHelper.ExportRatingReportToPDF().then(function (e) {

            //});


        });

        $("#questionComment").mouseleave(function (e) {
          
            var comment = $("#questionComment").val(); 

            if (comment.trim() == "") {

                $("#btnSaveResponse").attr("disabled", true);

            } else {

                $("#btnSaveResponse").removeAttr("disabled");
            }

        });

        $("#btnExportReport").on('click', function () {

            var includeRater = false;

            if ($("input[type=checkbox]:checked").length > 0) {
                includeRater = true;
            } else {
                includeRater = false;
            }

            $("#IncludeRaters").text(includeRater);

            $("#IncludeRaters").val(includeRater);

            $("#Survey_For").text($("#optReportRater option:selected").text());

            $("#Survey_For").val($("#optReportRater option:selected").text());

            var surveryFor = $("#optReportRater option:selected").text();

            var data = { Survey_For: surveryFor, IncludeRaters: includeRater };

            surveyHelper.ExportRatingReportToPDF().then(function (e) {
            });


        });

        

        $("#optReportRater").on('change', function () {

            if (!$("#optReportRater option:selected").text() == "---please select---") {

                $("#ShowDetailsReport").show();

                $("#divReportPerRater").show();

                $("#divGenerateReport").show();
                
            }

            $("#Survey_For").text($("#optReportRater option:selected").text());

            $("#Survey_For").val($("#optReportRater option:selected").text());

        });

        $("#drpBusinessUnit").on('change', function () {           
         
                //$("#ShowDetailsReport").hide();

                //$("#divReportPerRater").hide();

                $("#divGenerateReport").show();

            

        });

        $("#drpSelectedBusinessUnit").on('change', function () {
          
           
            if ($("#drpSelectOption option:selected").text() == "---Please Select---") {

                $("#divShowAlertWarning").show();

                $("#lblAlertWarning").text("Please select an option above (send email by survey or By Rater)");

                $(this).val("");

            } else {

                $("#divShowAlertWarning").hide();

                if ($("#drpSelectOption option:selected").text() == "By Survey") {

                    surveyHelper.LoadEmployeeSurveyList().then(function (e) {

                    });
                } else if ($("#drpSelectOption option:selected").text() == "By Rater") {
                        
                    surveyHelper.getLoadAllRaters().then(function (e) {
                    
                    });
                }
                
            }

        });

        $("#drpRaterList").on('change', function () {
            

            surveyHelper.getSurveyForListByRater($("#drpRaterList option:selected").text()).then(function (e) {

                $("#divSelectedRatersToNotify").show();

                var mySelect = $('#drpRater');

                mySelect.empty();

                $.each(e, function (val, text) {
                    mySelect.append(
                        $('<option></option>').val(text.Value).html(text.Name)
                    );
                });

                htmlHelper.createCheckboxDropDownList(mySelect);

                $("#divSendMail").show();
            });

        });        

        $("#btnUpdateSurveyRater").on('click', function () {
           
            var raters = "";

            var items = document.getElementsByClassName("ms-choice");
            for (var i = 0; i < items.length; i++) {

                raters += items[i].innerText;

                break;
            }

            if (raters == "Please Select" || raters.split(',').length < 3) {
                
                $("#divShowUpdateRatersWarning").show();

                $("#divShowUpdateRatersSuccess").hide();

                $("#saveUpdateRaterStatusAlert").hide();
                

            } else {
                surveyHelper.SaveUpdateRaters().then(function (e) {

                    $("#divShowUpdateRatersSuccess").show();

                    $("#saveUpdateRaterStatusAlert").show();

                    $("#divShowUpdateRatersWarning").hide();

                });
            }
            

        });


        $("#drpSurveyFor").on('change', function () {

            $("#divShowNextButton").show();
        });

        $("#btnNext").on('click', function () {

           
            //Update Raters
            surveyHelper.getSurveyRatersToBeNotified().then(function (e) {

                //$("#divNotifyRaters").hide();
               
                var mySelect = $('#drpRater');

                mySelect.empty();

                $.each(e, function (val, text) {
                    mySelect.append(
                        $('<option></option>').val(text.Value).html(text.Name)
                    );
                });

                htmlHelper.createCheckboxDropDownList(mySelect);
              

                //var selection = htmlHelper.getSelectedValues("#drpSurveyFor").split(',');



                //if (selection.length > 1) {

                //} else {
                //    htmlHelper.createCheckboxDropDownList(mySelect);
                //}

                $('input:checkbox').attr('checked', false);

                $("#drpRater option:selected").text("Please Select");

                var ratersSlected = $("#selectedRatersNoBeNotified").val();

                $("#divNotifyRaters").show();

                var selectRatersToBeNotifiedArray = ratersSlected.split(',');

            });

        });


        $("#btnSendEmail").on('click', function () {
           
            
            
            //Update Raters
            surveyHelper.SenMail().then(function (e) {

                $("#divEmailSentSuccessAlert").show();

                if ($("#drpSelectOption option:selected").text() == "By Rater") {

                    $("#successMessage").text($("#drpRaterList option:selected").text());

                    $("#divSelectedRatersToNotify").hide();

                    $("#divSendMail").hide();

                    $("#lblRatersToNotify").text("Survey For");

                } else {
                    $("#successMessage").text(htmlHelper.getSelectedValues("#drpRater"));

                    $("#lblRatersToNotify").text("Select Rater(s) be notified");
                }
                

                //Reload Page
                $("#drpSelectedBusinessUnit").show();

                $("#lblBusinessUnit").show();

                $("#drpSelectOption").val("");

                $("#drpSelectedBusinessUnit").val("");

                $("#drpSurveyFor").val("---Please Select---");

                $("#divEmployeesWithSurveyToNotify").hide();

                $("#divSelectedRatersToNotify").hide();

                if ($("#drpSelectOption option:selected").text() == "By Rater") {

                    $("#drpSelectedBusinessUnit").show();

                    $("#lblBusinessUnit").show();

                    $("#divSelectedRatersToNotify").hide();

                    $("#divRaterEmployeeList").hdie();

                    $("#divSendMail").hide();

                    $("#divSelectedRatersToNotify").hide();

                    $("#divSendMail").hide();

                   
                }
                
            });

           // $('#DivLoadingProgress').addClass('alertBg');

            dataTablehelper = $("#updateRaters").DataTable();

        });


        $("#btnSaveQuestion").on('click', function () {


            $("#selectedRatersString").val(htmlHelper.getSelectedValues("#drpRater"));

            $("#selectedRatersString").text(htmlHelper.getSelectedValues("#drpRater"));

            $("#RateeName").text(htmlHelper.getSelectedValues("#drpRater"));

            $("#raterStringModel").val(htmlHelper.getSelectedValues("#drpRater"));

            $("#raterStringModel").text(htmlHelper.getSelectedValues("#drpRater"));

            $("#raterList").text($('#selectedRatersString').text());

            htmlHelper.setDropdownValues("#drpRater", []);
            // htmlHelper.getSelectedValues("#drpRater");
            // getEnteredQuestions();

        });

        //$("#divFileUpload").hide();

        $("#RateeName").on('change', function (e) {

            $("#btnUpload").attr('disabled', true);

            if ($(this).val() == "0") {

                $("#divFileUpload").hide();
            } else {
                $("#divFileUpload").show();
            }
        });



        $("#drpDepartment").on('change', function (e) {

           

            $("#Department").val($(this).val());

            var data = { adGroupName: $(this).val() };

            $.post('PopulateUsers', data, function (e) {

                var mySelect = $('#drpEmployee');

                mySelect.empty();

                mySelect.append($('<option></option>').val("0").html("---Please Select---"));

                var data = JSON.parse(e);

                $.each(data, function (val, text) {
                    mySelect.append(
                        $('<option></option>').val(text.DisplayName).html(text.DisplayName)
                    );
                });

            });

        });

        $("#drpEmployee").val(0);

        var current_Month = (new Date).getMonth() + 1;

        $("#optMonth").val(current_Month)

        $("#saveSurveryStatusAlert").hide();

        $("#divQuartelySureyOption").hide();

        $("#divYearSureyOption").hide();

        if (current_Month >= 1 && current_Month <= 3) {

            $("#optSurveyOption").val(1);
        }

        if (current_Month >= 4 && current_Month <= 6) {

            $("#optSurveyOption").val(2);
        }

        if (current_Month >= 7 && current_Month <= 9) {

            $("#optSurveyOption").val(3);
        }



        if (current_Month >= 10 && current_Month <= 12) {

            $("#optSurveyOption").val(4);
        }

        $("#optYear").val(2);

        $("#optYear").attr('disabled', true);

        $("#optMonthSurvey").on("click", function (e) {

            $("#divQuartelySureyOption").hide();

            $("#divYearSureyOption").hide();

            $("#divMontlySurveyOption").show();

        });

        $("#optQuarterlySurvey").on("click", function (e) {

            $("#divQuartelySureyOption").show();

            $("#divYearSureyOption").hide();

            $("#divMontlySurveyOption").hide();

        });


        $("#optYearlySurvey").on("click", function (e) {

            $("#divYearSureyOption").show();

            $("#divQuartelySureyOption").hide();

            $("#divMontlySurveyOption").hide();
        });


        $(document).on('change', ':file', function () {
            
            var input = $(this), label = input.val().replace(/\\/g, '/').replace(/.*\//, '');

            input.trigger('fileselect', [1, label]);
        });

        $(':file').on('fileselect', function (event, numFiles, label) {

            $("#saveSurveryStatusAlert").hide();

            validateEmployeeUploadData(label);

            $("#btnUpload").attr("disabled", false);

            $("#fileUploadCaption").text("");

            $("#fileUploadCaption").text(label);

            var fileExtension = label.split('.')[1];

            if (fileExtension != "xlsx" && fileExtension != "xls" && fileExtension != "xlsm") {

                $("#lblErrorMessageAlert").text("Invalid file, please upload a valid excel spreadsheet file and try again (xlsx, xls, xlsm)");

                $("#divErrorMessageAlert").show();

                $("#btnUpload").attr("disabled", true)
            } else {
                getPreviewSurveyQuestions();
            }



        });


        $("#btnUpload").on('click', function (e) {

            var surveyPeriodType = "";

            var surveyPeriodValue = "";

            var date = new Date();

            var month = parseInt($("#optMonth option:selected").val());

            var quarter;

            if (month < 4) { quarter = 1; }

            else if (month < 7) { quarter = 2; }

            else if (month < 10) { quarter = 3; }

            else if (month < 13) { quarter = 4; }

            if ($("#optMonthSurvey").is(":checked")) {

                surveyPeriodType = "Monthly";

                surveyPeriodValue = date.getFullYear();

                if (month < 10) {

                    surveyPeriodValue = date.getFullYear() + "" + "0" + "" + month + "";

                } else {
                    surveyPeriodValue = date.getFullYear() + "" + month + "";
                }

            }

            if ($("#optQuarterlySurvey").is(":checked")) {

                var year = parseInt($("#optYear option:selected").text());

                surveyPeriodType = "Quartely";

                surveyPeriodValue = date.getFullYear() + "" + "0" + quarter + "";

            }

            if ($("#optYearlySurvey").is(":checked")) {

                surveyPeriodType = "Annually";

                surveyPeriodValue = $("#optYear option:selected").text();

            }

            $("#QuestionnairePeriodType").val(surveyPeriodType);

            $("#QuestionnairePeriodValue").val(surveyPeriodValue);


        });


        $("table#updateRaters").on("click", "a.btnSelectedRaters", function (e) {


            e.preventDefault();

            $('#loading').show();

            $('#DivLoadingProgress').addClass('alertBg');

            dataTablehelper = $("#updateRaters").DataTable();

            var selectedIndex = $(this).closest("tr").index();

            var rowData = dataTablehelper.row(selectedIndex).data();


            // var dd = JSON.parse(rowData);
            
            surveyHelper.getSurveyRatersToUpdateSurveyRaters().then(function (e) {

                var mySelect = $('#manageRater');

                mySelect.empty();

                $.each(e, function (val, text) {
                    mySelect.append(
                        $('<option></option>').val(text.Value).html(text.Name)
                    );
                });
                
                htmlHelper.createCheckboxDropDownList(mySelect);

                $('input:checkbox').attr('checked', false);

                $("#manageRater option:selected").text("");

                var ratersSlected = $("#selectedRatersString").val();

                var selectRatersArray = ratersSlected.split(',');

                htmlHelper.setDropdownValues("#manageRater", selectRatersArray);

                $('#loading').hide();

                $('#DivLoadingProgress').removeClass('alertBg');

            });


            // var rowData = dataTablehelper.row($(this).data("rowId")).data();

        });

        $(function () {

            if ($("#UploadStatus").val() == "success") {

                $("#saveSurveryStatusAlert").show();

            } else if ($("#UploadStatus").val() == "failed") {
               
                $("#divErrorMessageAlert").show();
            }

            $("#drpDepartment").val("DG-Futuregrowth");

            $('#optAll').prop('checked', true);

            //$("#divEmailSentSuccessAlert").show();

        });

        $("#btnShowSelectedRaters").on('click', function (e) {
            
            $('#loading').show();

            surveyHelper.getSurveyRaters().then(function (e) {

                var mySelect = $('#manageRater');

                mySelect.empty();

                $.each(e, function (val, text) {
                    mySelect.append(
                        $('<option></option>').val(text.DisplayName).html(text.DisplayName)
                    );
                });

                htmlHelper.createCheckboxDropDownList(mySelect);

                $('#loading').show();

            });
        });

        $("#optYearlySurvey").trigger('click');
    });

    return { surveyHelper: surveyHelper };
})();