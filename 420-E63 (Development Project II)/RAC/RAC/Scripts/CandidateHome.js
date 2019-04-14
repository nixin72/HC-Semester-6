$(".tabHeader").click(event => {
    document.body.scrollTo(0, 0);
});

$(() => {
    if (location.href.indexOf("#") == -1) {
        $("#tabs #pnlProgram").css("display", "block");
    }
    else {
        if (location.href.indexOf("#pnlProgram") == -1) {
            $("#tabs #pnlProgram").css("display", "none");
        }
    }
});


$("table[genEdOnly=true]").each((key, val) => {
    $("#tblGenEd").append("<table class='tblCompetencies' genEdOnly='true'>" + $(val).html() + "</table>");
    $(val).css("display", "none");
});

$("table[genEdOnly=false]").each((key, val) => {
    $("#tblProgram").append("<table class='tblCompetencies' genEdOnly='false'>" + $(val).html() + "</table>");
    $(val).css("display", "none");
});


function GetSaveChangesURI() {
    return '@Url.Content("UploadDocument/SaveChangesToDB")';
}

function GetDeleteChangesURI() {
    return '@Url.Content("UploadDocument/DeleteFileFromDB")';
}

function GetShowViewDocumentsURI() {
    return '@Url.Content("UploadDocument/ShowViewDocuments")';
}

function GetSubmitRACRequestURI() {
    return '@Url.Content("Home/SubmitRACRequest")';
}
function GetFallbackURI() {
    return '@Url.Action("Fallback", "Home")';
}
function GetSaveRACRequestURI() {
    return '@Url.Content("Home/SaveRACRequest")';
}
function GetSelfEvaluationProgressURI() {
    return '@Url.Content("Home/GetSelfEvaluationProgress")';
}