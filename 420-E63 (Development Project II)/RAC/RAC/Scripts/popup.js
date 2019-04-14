/* This is the javascript code for the _Popup.cshtml partial view */
window.onload = function () {
    setupListener();
}


function setupListener() {
    document.getElementById('messageBox').addEventListener("click", function (e) {
        e.stopPropagation();
    });

    document.getElementById('pageBlur').addEventListener("click", function (e) {
        hidePopup();
    });

    document.getElementById('closeBtn').addEventListener("click", function (e) {
        hidePopup();
    });
}

function changePopupHeader(newHead) {
    document.getElementById("popupHeader").innerHTML = newHead;
}

function changePopupContent(newText) {
    document.getElementById("popupContent").innerHTML = newText;
}

function showPopup() {
    document.getElementById('pageBlur').style.display = "block";
    document.getElementById('messageBox').style.display = "block";
}

function hidePopup() {
    document.getElementById('pageBlur').style.display = "none";
    document.getElementById('messageBox').style.display = "none";
}