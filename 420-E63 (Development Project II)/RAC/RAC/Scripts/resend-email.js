// used on login.cshtml
$(function () {
    $('#resendEmail').click(function (e) {
        clickResend(e);
    });
    if (localStorage.getItem('firstSignIn')) {
        createPopup('Confirm Your Email', 'If it is your first time signing in, please go to your email and click the link we sent you to confirm your email.');
        localStorage.removeItem('firstSignIn');
    }
    else if (localStorage.getItem('changedPassword')) {
        createPopup('Password Reset', 'Your password was successfully changed.');
        localStorage.removeItem('changedPassword');
    }
})
function clickResend(e) {
    // on load read in variables from the query string and send a request to the controller
    var userId = e.target.attributes.data.value;
    console.log(userId)
    $.ajax({
        type: "GET",
        url: "../Account/ResendEmailAjax",
        data: { "userID": parseInt(userId) },
        dataType: "text",
        success: function (response) {
            console.log(response);
            if (response == "Invalid Data") {
                error();
            }
            else {
                // response data wwas good
                // display pop up saaying it was a success
                editPopup('Email Re-sent','Your confirmation email was successfully re-sent.');
            }
        },
        error: function (response) {
            error();
        }
    });

}
function error() {
    // pop up saying that it failed
    createPopup('Resend Failure', 'Email failed to resend. Make sure you have waited at least 1 minute since the last email.');
}
function createPopup(header, content) {
    // this updates the popup partial view. 
    // If the partial view is broken or not available it uses alert instead
    var popupPartial = document.getElementById("pageBlur");
    if (popupPartial != null) {
        changePopupContent(content);
        changePopupHeader(header);
        showPopup();
    }
    else{
        alert(content);
    }

}