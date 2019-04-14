$(function () {
    $(".editApplicationBtn").click(function(e) {
        var row = this.parentElement.parentElement;
        if (this.innerText == "Save") {

            var button = this.parentElement.getElementsByClassName("deleteApplicationBtn")[0];            
            button.innerText = "Delete";

            this.innerText = "Edit";
            // make ajax call to update the application
            var inputDescription =
                row.getElementsByClassName("applicationDescription")[0].getElementsByTagName("input")[0];
            var inputCode = row.getElementsByClassName("applicationCode")[0].getElementsByTagName("input")[0];
            if (hasInputChanged(inputDescription) || hasInputChanged(inputCode)) {
                var code = inputCode.value;
                var desc = inputDescription.value;
                var id = row.id.substr(3);
                if (code != null && desc != null && id != null) {
                    if ((code.length == 3 || code.length == 2) && desc.length < 50 && desc.length != 0) {
                        if (hasInputChanged(inputCode)) {
                            isRoleUsed(code, function () {
                                ajaxCall("EditApplication",
                                                { app: { "Description": desc, "Code": code, "IDApplication": id } })
                            });
                        } else {
                            ajaxCall("EditApplication",
                                { app: { "Description": desc, "Code": code, "IDApplication": id } });
                        }
                    } else {
                        //invalid code or description length
                        invalidFieldError(desc, code);
                    }
                } else {
                    // missing description code or id
                    nullFieldError(desc, code, id);
                }
            }
            makeRowEditable();
        } else {
            // change all buttons back to edit
            makeRowEditable(row);
            $(".editApplicationBtn").each(function(i, obj) {
                obj.innerText = "Edit";
            });
            // change all buttons back to delete
            $(".deleteRoleBtn").each(function (i, obj) {
                obj.innerText = "Delete";
            });
            this.innerText = "Save";
            // change delete button to 'cancel'
            var button = this.parentElement.getElementsByClassName("deleteApplicationBtn")[0];  
            button.innerText = "Cancel";
        }
    });
    $(".editRoleBtn").click(function(e) {
        var row = this.parentElement.parentElement;
        if (this.innerText == "Save") {

            this.parentElement.getElementsByClassName("deleteRoleBtn")[0].innerText = "Delete";

            this.innerText = "Edit";
            var inputDescription =
                row.getElementsByClassName("roleDescription")[0].getElementsByTagName("input")[0];
            var inputCode = row.getElementsByClassName("roleCode")[0].getElementsByTagName("input")[0];
            if (hasInputChanged(inputDescription) || hasInputChanged(inputCode)) {
                var code = inputCode.value;
                var desc = inputDescription.value;
                var id = row.id.substr(4);
                if (code != null && desc != null && id != null) {
                    if ((code.length == 3 || code.length == 2) && desc.length < 50 && desc.length != 0) {
                        // check that the role has not been changed to something that is taken
                        if (hasInputChanged(inputCode)) {
                            isRoleUsed(code, function () {
                                ajaxCall("EditRole",
                                                { role: { "Description": desc, "Code": code, "IDRole": id } })
                            });
                        } else {
                            ajaxCall("EditRole",
                                            { role: { "Description": desc, "Code": code, "IDRole": id } })
                        }
                        
                    } else {
                        //invalid code or description length
                        invalidFieldError(desc, code);
                    }
                } else {
                    // missing description code or id
                    nullFieldError(desc, code, id);
                }
            }
            makeRowEditable();
            // make ajax call to update the row
        } else {
            // change all buttons back to edit
            makeRowEditable(row);
            $(".editRoleBtn").each(function(i, obj) {
                obj.innerText = "Edit";
            });
            // change all buttons back to delete
            $(".deleteRoleBtn").each(function (i, obj) {
                obj.innerText = "Delete";
            });
            this.innerText = "Save";
            // change delete button to 'cancel'
            this.parentElement.getElementsByClassName("deleteRoleBtn")[0].innerText = "Cancel";
        }
    });

    $(".addApplicationBtn").click(function () {
        var desc = $("#newApplicationDescription")[0].value;
        var code = $("#newApplicationCode")[0].value;
        var id = 666;
        if (code != null && desc != null) {
            if ((code.length == 3 || code.length == 2) && desc.length < 50 && desc.length != 0) {
                isAppUsed(code,
                    function() { ajaxCall("AddApplication", { app: { "Description": desc, "Code": code } }) });
            } else {
                //invalid code or description length
                invalidFieldError(desc, code);
            }
        } else {
            // missing description code or id
            nullFieldError(desc, code, id);
        }
    });



    $(".addRoleBtn").click(function() {
        var desc = $("#newRoleDescription")[0].value;
        var code = $("#newRoleCode")[0].value;
        var id = 666;
        if (code != null && desc != null) {
            if ((code.length == 3 || code.length == 2) && desc.length < 50 && desc.length != 0) {
                isRoleUsed(code,
                    function() { ajaxCall("AddRole", { role: { "Description": desc, "Code": code } }) });
            } else {
                //invalid code or description length
                invalidFieldError(desc, code);
            }
        } else {
            // missing description code or id
            nullFieldError(desc, code, id);
        }
    });

    $(".deleteApplicationBtn").click(function (e) {
        if (this.innerText == "Cancel") {
            // set editable fields back to normal
            makeRowEditable();
            e.preventDefault();
            // change all buttons back to edit
            $(".editApplicationBtn").each(function (i, obj) {
                obj.innerText = "Edit";
            });
            // change all buttons back to delete
            $(".deleteRoleBtn").each(function (i, obj) {
                obj.innerText = "Delete";
            });
        }
        else {
            if (window.confirm("Do you really want to delete this application?")) {
                // otherwise link will continue
            } else {
                e.preventDefault();
            }
        }
    });

    $(".deleteRoleBtn").click(function (e) {
        if (this.innerText == "Cancel") {
            // set editable fields back to normal
            makeRowEditable();
            e.preventDefault();
            // change all buttons back to edit
            $(".editRoleBtn").each(function (i, obj) {
                obj.innerText = "Edit";
            });
            // change all buttons back to delete
            $(".deleteRoleBtn").each(function (i, obj) {
                obj.innerText = "Delete";
            });
        }
        else {
            if (window.confirm("Do you really wish to delete this role?")) {
                // otherwise link will continue
            } else {
                e.preventDefault();
            }
        }
    });
    $('.expandAppRoles').click(function () {
        var idOfClicked = this.id.substr(14);

        $('.appRoles' + idOfClicked).slideToggle();
        
        var imageRotate = $('#img' + idOfClicked);
        var value = imageRotate[0].getAttribute("opened");

        if (value == "true") {      
            imageRotate.css({ 'transform': 'rotate(' + 0 + 'deg)' });
            removeOpened(idOfClicked);
            imageRotate.first().attr("opened", "false");
        }
        else {
           /* App Role dropdown is being opened.
            * When this happens we will save which is opened in local storage.
            * this way when the page is reloaded (after an operation), 
            * the same dropdown can be re-opened
            */
            addOpened(idOfClicked);
            imageRotate.css({ 'transform': 'rotate(' + 180 + 'deg)' });
            imageRotate.first().attr("opened", "true");
        }
    })

    $(".deleteApplicationRole").click(function (e) {
        var idOfClicked = this.id.substr(7);
        ajaxCall("DeleteApplicationRole", { IDApplicationRole: idOfClicked })
    });
    $(".addRoleToApp").click(function (e) {
        var appId = e.target.getAttribute("appId");
        var roleId = $("#applicationDropdown" + appId).find(":selected").val();
        if (appId && !isNaN(roleId)) {
            ajaxCall("AddApplicationRole", { "appId": appId, "roleId": roleId })
        }
        else {
            alert("Please select a role.");
        }
    });
    $(".setInactive").click(function (e) {
        var appRoleId = e.target.getAttribute("value");
        ajaxCall("UpdateApplicationRole", { "IDApplicationRole": appRoleId, "isActive": false })
    });
    $(".setActive").click(function (e) {
        var appRoleId = e.target.getAttribute("value");
        ajaxCall("UpdateApplicationRole", { "IDApplicationRole": appRoleId, "isActive": true })
    });

    // when document is loaded, open the dropdowns that were open last time on the page
    var openedItems = JSON.parse(localStorage.getItem("OpenedLists"));
    if (openedItems) {
        for(item in openedItems){
            openDropdown(openedItems[item])
        }
    }
    
    
});
function addOpened(newItem) {
    var openedArray = JSON.parse(localStorage.getItem("OpenedLists"));
    if (!openedArray) {
        openedArray = [];
    }
    openedArray.push(newItem);
    localStorage.setItem("OpenedLists",JSON.stringify(openedArray));
}
function removeOpened(itemToRemove) {
    var openedArray = JSON.parse(localStorage.getItem("OpenedLists"));
    if (openedArray) {
        var index = openedArray.indexOf(itemToRemove);
        if (index !== -1) openedArray.splice(index, 1);
    }
    localStorage.setItem("OpenedLists", JSON.stringify(openedArray));
}
function openDropdown(id) {
    var imageRotate = $("#img" + id);
    imageRotate.css({ 'transform': 'rotate(' + 180 + 'deg)' });
    imageRotate.first().attr("opened", "true");
    $('.appRoles' + id).slideToggle();
}
function isRoleUsed(_role, callback) {
    $.ajax({
        type: "Get",
        url: "isRoleCodeUsed",
        data: AddAntiForgeryToken({ roleId: _role }),
        success: function(data) {
            if (data === "False"){
                callback();
            } else {
                alert("Role code already in use");
            }
        },
        error: function(data) {console.log(data)}
    });
}
function isAppUsed(_app, callback) {
    $.ajax({
        type: "Get",
        url: "isApplicationCodeUsed",
        data: AddAntiForgeryToken({ appId: _app }),
        success: function (data) {
            if (data === "False") {
                callback();
            } else {
                alert("App code already in use");
            }
        },
        error: function (data) { console.log(data) }
    });
}
function ajaxCall(url, data) {
    $.post(url, AddAntiForgeryToken(data), function(){
        location.reload();
    }).catch(function(err){
        console.error(err);
        alert("Error sending information to server");
    });
}

//This function will accept a data json, and append the RequestVerificationToken to it
function AddAntiForgeryToken(data) {
    data.__RequestVerificationToken = $("form input[name=__RequestVerificationToken]").val();
    return data;
}

function invalidFieldError(desc, code) {
    var errorStr = "Error: ";
    var multipleErrors = false;
    if (desc.length == 0) {
        errorStr += "The description is a required field. ";
        multipleErrors = true;
    }
    if (desc.length >= 50) {
        if (multipleErrors) {
            errorStr += "\r\n";
        }
        errorStr += "The description must be under 50 characters. ";
    }
    if (code.length != 3 && code.length != 2) {
        if (multipleErrors) {
            errorStr += "\r\n";
        }
        errorStr += "The code must be 2 or 3 characters. ";
    }
    alert(errorStr);
}

function nullFieldError(desc, code, id) {
    var errorStr = "Error: ";
    var multipleErrors = false;
    if (code == null) {
        errorStr += "The code was not found. ";
        multipleErrors = true;
    }
    if (desc == null) {
        if (multipleErrors) {
            errorStr += "\r\n";
        }
        errorStr += "The description was not found. ";
        multipleErrors = true;
    }
    if (id == null) {
        if (multipleErrors) {
            errorStr += "\r\n";
        }
        errorStr += "The ID was not found for this entity. ";
    }
    alert(errorStr);
}

function makeRowEditable(row) {
    var tds;
    var input;
    // set any active row to inactive
    $(".editingRow").each(function() {
        this.classList.remove("editingRow");
        tds = this.getElementsByTagName("td");
        for (var i = 0; i < tds.length; i++) {
            if (checkClassName(tds[i].classList)) {
                input = tds[i].getElementsByTagName("input")[0];
                input.disabled = true;
                input.value=(input.attributes["originalVal"].value);

            }
        }
    });
    if (row) {
        // set new row to active row
        row.classList.add("editingRow");
        tds = row.getElementsByTagName("td");
        for (var i = 0; i < tds.length; i++) {
            if (checkClassName(tds[i].classList)) {
                input = tds[i].getElementsByTagName("input")[0];
                input.disabled = false;
                input.setAttribute("originalVal", input.value);
            }
        }
    }

}

function hasInputChanged(textbox) {
    return (textbox.value != textbox.attributes["originalVal"].value);
}

function checkClassName(classNames) {
    var validClasses = ["applicationDescription", "applicationCode", "roleDescription", "roleCode"];
    for (var i = 0; i < classNames.length; i++) {
        if (validClasses.indexOf(classNames[i]) != -1) {
            return true;
        }
    }
    return false;
}