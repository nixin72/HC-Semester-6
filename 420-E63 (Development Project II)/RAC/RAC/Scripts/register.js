//<summary>
//Every time the user tabs out of the email textbox an ajax call will be sent to the server grabbing whatever is in the field
//and compares it to make sure that the email is unique to the system
//</summary>
$('document').ready(function() {
    $('#chbPrivacyPolicy').removeAttr('Checked');

    $('#Password').keyup(function() {
        checkPasswordErrorMessageDisplayed();
    });
    $('#Password').focusout(function() {
        checkPasswordErrorMessageDisplayed();
    });

    function checkPasswordErrorMessageDisplayed() {
        setTimeout(function() {
                const errorDivChildren = $('#passErrMsg').children();
                if (errorDivChildren.length !== 0) {
                    const span = errorDivChildren[0];
                    const text = span.childNodes[0].data;
                    if (text === 'Password is Required') {
                        $('#passDiv').removeClass('pass-div-responsive');
                    } else {
                        $('#passDiv').addClass('pass-div-responsive');
                    }
                } else {
                    $('#passDiv').removeClass('pass-div-responsive');
                }
            },
            30);
    }

    addRemovePreferredContact();
    checkContainsGenEds();

    $('#Email').focusout(function() {
        validateEmail();
    });

    $('#WorkPhone').focusout(function() {
        addRemovePreferredContact();
    });

    $('#ProgramId').change(function() {
        checkContainsGenEds();
    });

    $('#chbPrivacyPolicy').on('change',
        function(e) {
            acceptDenyPrivacyPolicy(e.target.checked);
        });

    //This function will accept a data json, and append the RequestVerificationToken to it
    function addAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('form input[name=__RequestVerificationToken]').val();
        return data;
    }

    //Helper functions
    function validateEmail() {
        const enteredEmail = $('#Email').val();

        //Checking to make sure that the field is not empty and contains symbol before doing an ajax call
        if (enteredEmail !== '' && enteredEmail.indexOf('@') >= 0) {
            $.ajax({
                type: 'POST',
                url: GetCheckUniqueEmail(),
                data: addAntiForgeryToken({ email: enteredEmail }),
                dataType: 'json',
                success: function(response) {
                    showHideMessage(response);
                }
            });
        } else {
            enableDisableGenEd(false);
        }

    }

    //TODO Get message to show
    function showHideMessage(unique) {
        if (unique) {
            $('span[data-valmsg-for="Email"]').text('This email is already in use.');
        }
    }

    function checkContainsGenEds() {
        const programId = $('#ProgramId').val();
        const hasGenEdsUri = $('#uri').attr('uri-hasgeneds');

        //If a valid programId is selected
        if ($.isNumeric(programId)) {
            $.ajax({
                type: 'POST',
                url: hasGenEdsUri,
                data: addAntiForgeryToken({ id: programId }),
                dataType: 'json',
                success: function(response) {
                    enableDisableGenEd(response);
                }
            });
        } else {
            enableDisableGenEd(false);
        }
    }

    function enableDisableGenEd(containsGenEds) {
        if (containsGenEds) {
            $('#chbGenEdOnly').attr('disabled', false);
            $('#chbGenEdOnly').closest('div').removeClass('disabled');
        } else {
            $('#chbGenEdOnly').attr('disabled', true);
            $('#chbGenEdOnly').closest('div').addClass('disabled');
        }

    }

    function acceptDenyPrivacyPolicy(checked) {
        $('#btnSubmit').attr('disabled', (!checked));
    }

    //This function will remove or add the preferred way of contacts based off of the information being loaded
    function addRemovePreferredContact() {
        const workPhoneVal = $('#WorkPhone').val().trim();
        const workPhoneRegex = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
        if (workPhoneRegex.test(workPhoneVal)) {
            //Removing to prevent duplicates
            $("#PreferredMethodOfContact option[value='2']").remove();
            $('#PreferredMethodOfContact').append($('<option>', { value: 2, text: 'Work Phone' }));
        } else {
            $("#PreferredMethodOfContact option[value='2']").remove();
        }

    }

    $('form').submit(function() {
        // set this item so that the sign in page knows its the users first time
        // this way the user can be notified that they must confirm their email before first sign in
        if (localStorage.getItem('firstSignIn') == null) {
            localStorage.setItem('firstSignIn', 'true');
        }
    });
});