$(function() {
    if (localStorage.getItem('firstSignIn')) {
        createPopup('Confirm Your Email',
            'If it is your first time signing in, please go to your email and click the link we sent you to confirm your email.');
        localStorage.removeItem('firstSignIn');
    }
});

function createPopup(header, content) {
    // this updates the popup partial view.
    // If the partial view is broken or not available it uses alert instead
    var popupPartial = document.getElementById('pageBlur');
    if (popupPartial != null) {
        changePopupContent(content);
        changePopupHeader(header);
        showPopup();
    }
    else{
        alert(content);
    }

}