$(document).ready(function() {
    $('#Email').focusout(function (e) {
        validateExistingEmail();
    });


    function validateExistingEmail() {
        var enteredEmail = $("#Email").val();
        //Checking to make sure that the field is not empty and contains symbol before doing an ajax call
        if (enteredEmail !== '' && enteredEmail.indexOf('@') >= 0) {
            $.ajax({
                type: 'POST',
                url: GetIsExistingEmailUrl(),
                data: AddAntiForgeryToken({ email: enteredEmail }),
                dataType: 'json',
                success: function (response) {
                    ShowHideMessage(response);
                }
            });
        };
    }

    //TODO Get message to show
    function ShowHideMessage(unique) {
        if (!unique) {
            $('span[data-valmsg-for="Email"]').text('This email doesn\'t exist');
        }
    }




    //This function will accept a data json, and append the RequestVerificationToken to it
    function AddAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('form input[name=__RequestVerificationToken]').val();
        return data;
    }
});


