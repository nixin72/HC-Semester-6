$(document).ready(function () {
    $('#resend').on('click',
        e => {
            e.preventDefault();

            let head = "";
            let text = "";

            $.ajax({
                type: 'POST',
                url: $('#uri').attr('uri-resendconfirmationemail'),
                data: { email: $('#email').val() },
                dataType: "text",
                success: function (response) {
                    if (response == "1") {
                        head = "Email Sent";
                        text = "The confirmation email was sent to " + $('#email').val() + ".";
                    } else if (response == "-1") {
                        head = "Email Not Sent";
                        text = "There are no accounts with the email " + $('#email').val() + " registered with us.";
                    } else if (response == "-2") {
                        head = "Email Not Sent";
                        text = "The email " + $('#email').val() + " has already been validated.";
                    }
                    text += "<br/><button onclick='hidePopup();'>Ok</button>";
                    changePopupHeader(head);
                    changePopupContent(text);
                    showPopup();
                }

            });
        });
});