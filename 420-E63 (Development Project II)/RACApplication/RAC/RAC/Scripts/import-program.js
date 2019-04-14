$(document).ready(function () {
    const searchProgramUri = $('#uri').attr('uri-searchprogram');
    const searchGenEdUri = $('#uri').attr('uri-searchgened');
    const getProgramCompetenciesUri = $('#uri').attr('uri-getprogramcompetencies');
    const getGenEdCompetenciesUri = $('#uri').attr('uri-getgenedcompetencies');
    const getCourseProfiles = $('#uri').attr('uri-getcourseprofile');
    const checkExistingProgramUri = $('#uri').attr('uri-checkexistingprogram');

    resetForm();
    $(window).keydown(function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            return false;
        }
        return true;
    });

    $('#ResetForm').click(() => {
        resetForm();
        $('#ProgramCode').val('');
        $('#GenEdCode').val('');
    });

    $('#btnSearch').click(function () {
        if (formIsValid()) {
            resetForm();
            searchProgram();
            if (!$('#chbNoGenEds').is(':checked')) {
                searchGenEd();
            }
            disableTextFields();
        }
    });

    function disableTextFields() {
        $('#ProgramCode').prop('disabled', true);
        $('#GenEdCode').prop('disabled', true);

    }

    function enableTextFields() {
        $('#ProgramCode').prop('disabled', false);
        $('#GenEdCode').prop('disabled', false);
    }

    function resetForm() {
        enableTextFields();

        $('#ProgramSearchResults').html('');
        $('#GenEdSearchResults').html('');
        $('#ProgramCompetencyResults').html('');
        $('#GenEdCompetencyResults').html('');
        $('#ProgramCourseProfileResults').html('');

        $('#SubmitRAC').hide();

        $('#LoadingIconProg').hide();
        $('#LoadingIconProg').css('visibility', 'hidden');
        $('#LoadingIconGenEd').hide();
        $('#LoadingIconGenEd').css('visibility', 'hidden');
        $('#LoadingIconProgComp').hide();
        $('#LoadingIconProgComp').css('visibility', 'hidden');
        $('#LoadingIconGenEdComp').hide();
        $('#LoadingIconGenEdComp').css('visibility', 'hidden');
        $('#LoadingIconCourses').hide();
        $('#LoadingIconCourses').css('visibility', 'hidden');
    }

    function formIsValid() {
        if (!$('#chbNoGenEds').is(':checked')) {
            $('#SearchProgram').validate().element('#ProgramCode');
            $('#SearchProgram').validate().element('#GenEdCode');
            if ($('#ProgramCode').valid() && $('#GenEdCode').valid()) {
                return true;
            } else
                return false;
        } else {
            $('#SearchProgram').validate().element('#ProgramCode');
            if ($('#ProgramCode').valid()) {
                return true;
            } else
                return false;
        }
    }

    function searchProgram() {
        var enteredProgramCode = $('#ProgramCode').val();
        $('#LoadingIconProg').show();
        $('#LoadingIconProg').css('visibility', 'visible');
        $.ajax({
            type: 'POST',
            url: searchProgramUri,
            data: addAntiForgeryToken({ ProgramCode: enteredProgramCode }),
            dataType: 'html',
            success: function (response) {
                $('#LoadingIconProg').hide();
                $('#LoadingIconProg').css('visibility', 'hidden');
                $('#ProgramSearchResults').html(response);
                $('#ProgRes').collapse('show');
                initializeCourseProfiles();
            }
        });
    }

    function searchGenEd() {
        var enteredGenEdCode = $('#GenEdCode').val();
        $('#LoadingIconGenEd').show();
        $('#LoadingIconGenEd').css('visibility', 'visible');
        $.ajax({
            type: 'POST',
            url: searchGenEdUri,
            data: addAntiForgeryToken({ ProgramCode: enteredGenEdCode }),
            dataType: 'html',
            success: function (response) {
                $('#LoadingIconGenEd').hide();
                $('#LoadingIconGenEd').css('visibility', 'hidden');
                $('#GenEdSearchResults').html(response);
                $('#GenEdRes').collapse('show');
                initializeCourseProfiles();
            }
        });
    }

    $('#chbNoGenEds').click(function () {
        resetForm();
        if ($(this).is(':checked')) {
            $('#GenEdCode').val('');
            $('#GenEdsDiv').fadeOut(250);
            $('#LoadingIconGenEdComp').css('height', '0');
            $('#LoadingIconGenEdComp').css('width', '0');
        } else {
            $('#GenEdsDiv').fadeIn(250);
            $('#LoadingIconGenEdComp').css('height', '5%');
            $('#LoadingIconGenEdComp').css('width', '5%');
        }
    });

    function addAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('form input[name=__RequestVerificationToken]').val();
        return data;
    }

    function initializeCourseProfiles() {
        $('input:radio[name="ProgramId"]').off();
        $('input:radio[name="ProgramId"]').change(
            function () {
                if ($(this).is(':checked')) {
                    $('#ProgRes').collapse('hide');
                    if (!$('#chbNoGenEds').is(':checked')) {
                        if ($('input:radio[name="GenEdId"]').is(':checked')) {
                            getCompetencies();
                        }
                    } else
                        getCompetencies();
                }
            });
        if (!$('#chbNoGenEds').is(':checked')) {
            $('input:radio[name="GenEdId"]').off();
            $('input:radio[name="GenEdId"]').change(
                function () {
                    if ($(this).is(':checked')) {
                        $('#GenEdRes').collapse('hide');
                        if ($('input:radio[name="ProgramId"]').is(':checked')) {
                            getCompetencies();
                        }
                    }
                });
        }
    }

    function getCompetencies() {

        $('#LoadingIconProgComp').show();
        $('#LoadingIconProgComp').css('visibility', 'visible');
        $('#LoadingIconGenEdComp').show();
        $('#LoadingIconGenEdComp').css('visibility', 'visible');
        $.ajax({
            type: 'POST',
            url: getProgramCompetenciesUri,
            data: addAntiForgeryToken({ ProgramId: $("input[name='ProgramId']:checked").val() }),
            dataType: 'html',
            success: function (response) {
                $('#LoadingIconProgComp').hide();
                $('#LoadingIconProgComp').css('visibility', 'hidden');
                $('#ProgramCompetencyResults').html(response);
                $('#ProgCompRes').collapse('show');
            }
        });

        if (!$('#chbNoGenEds').is(':checked')) {
            $.ajax({
                type: 'POST',
                url: getGenEdCompetenciesUri,
                data: addAntiForgeryToken({ ProgramId: $("input[name='GenEdId']:checked").val() }),
                dataType: 'html',
                success: function (response) {
                    $('#LoadingIconGenEdComp').hide();
                    $('#LoadingIconGenEdComp').css('visibility', 'hidden');
                    $('#GenEdCompetencyResults').html(response);
                    $('#GenEdCompRes').collapse('show');
                }
            });
        }

        $('#LoadingIconCourseProfile').show();
        $('#LoadingIconCourseProfile').css('visibility', 'visible');
        $.ajax({
            type: 'POST',
            url: getCourseProfiles,
            data: addAntiForgeryToken({ ProgramId: $("input[name='ProgramId']:checked").val() }),
            dataType: 'html',
            success: function (response) {
                $('#LoadingIconCourseProfile').hide();
                $('#LoadingIconCourseProfile').css('visibility', 'hidden');
                $('#ProgramCourseProfileResults').html(response);
                $('#ProfRes').collapse('show');
                initializeCourses();
            }
        });
    }

    function initializeCourses() {
        $('input:radio[name="CourseProfileId"]').off();
        $('input:radio[name="CourseProfileId"]').change(
            function () {
                if ($(this).is(':checked')) {
                    $('#LoadingIconCourses').show();
                    $('#LoadingIconCourses').css('visibility', 'visible');
                    $('#ProfRes').collapse('hide');
                    $('#ProgCompRes').collapse('hide');
                    $('#GenEdCompRes').collapse('hide');

                    $('#LoadingIconCourses').hide();
                    $('#LoadingIconCourses').css('visibility', 'hidden');
                    $.ajax({
                        type: 'POST',
                        url: checkExistingProgramUri,
                        data: addAntiForgeryToken({ programCode: $('#ProgramCode').val() }),
                        dataType: 'html',
                        success: function (response) {
                            var json = $.parseJSON(response);
                            var exists = json.Exists;
                            if (exists) {
                                changePopupHeader('Warning: Program Already Exists');
                                changePopupContent('<h3>A program using the code ' + $('#ProgramCode').val().toUpperCase() + ` already exists.</h3><h5>Adding this program will replace the previous version.</h5><p>
                                        </p><p>Any RAC Requests using the previous version <b>will not</b> be affected</p><button id='ConfirmOverwrite' class='btn btn-default'>Dismiss</button> |
                                        <a href='javascript:;' id='ResetFormPopup'>Restart Search</a>`);
                                showPopup();
                                $('#SubmitRAC').show();
                                setupPopupListeners();
                            }
                            else
                                $('#SubmitRAC').show();
                        }
                    });

                }
            });
    }

    function setupPopupListeners() {
        $('#ConfirmOverwrite').off();
        $('#ConfirmOverwrite').click(() => {
            hidePopup();
        });

        $('#ResetFormPopup').off();
        $('#ResetFormPopup').click(() => {
            resetForm();
            $('#ProgramCode').val('');
            $('#GenEdCode').val('');
            hidePopup();
        });
    }



})