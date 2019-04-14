$(function () {
    $("#btnClear").click(function (e) {
        console.log("test");
        $("#lblLastName").val('');
        $("#lblFirstName").val('');
        $("#lblUsername").val('');
    });


    $(".replaceUserBtn").click(function (e) {
        console.log(this.id);
        var row = this.parentElement.parentElement;
        var inputUsername =
                row.getElementsByClassName("adusername")[0].getElementsByTagName("p")[0];
        console.log(inputUsername.innerText);
        if(inputUsername.innerText.length!=0 && inputUsername.innerText.length<=60)
            ajaxCall("POST", "EditUser", { user: { "IdUser": this.id, "Username": inputUsername.innerText } });
    });

    $(".editUserBtn").click(function (e) {
        console.log(this.id);
        var row = this.parentElement.parentElement;
        var inputUsername =
                row.getElementsByClassName("username")[0].getElementsByTagName("input")[0];

        if (this.innerText == "Save") {
            this.innerText = "Edit";
            inputUsername.disabled = true;

            ajaxCall("POST", "EditUser", { user: { "IdUser": this.id, "Username": inputUsername.value } });
            //var inputUsername =
            //    row.getElementsByClassName("username")[0].getElementsByTagName("input")[0];
            //var inputCode = row.getElementsByClassName("roleCode")[0].getElementsByTagName("input")[0];
            //if (hasInputChanged(inputDescription) || hasInputChanged(inputCode)) {
            //    var code = inputCode.value;
            //    var desc = inputDescription.value;
            //    var id = row.id.substr(4);
            //    if (code != null && desc != null && id != null) {
            //        ajaxCall("POST",
            //            "EditRole",
            //            { role: { "Description": desc, "Code": code, "IDRole": id } });
            //    }
            //}
         //   makeRowEditable();
            // make ajax call to update the row
        } else {
            // change all buttons back to edit
            //      makeRowEditable(row);
            inputUsername.disabled = false;
            $(".editRoleBtn").each(function (i, obj) {
                obj.innerText = "Edit";
            });
            this.innerText = "Save";
        }
    });

    function ajaxCall(type, url, data, dataType) {
        $.post(url, AddAntiForgeryToken(data), function(){
            location.reload();
        }).catch(function(err){
            console.error(err);
            alert("error");
        });
    }

    function AddAntiForgeryToken(data) {
        data.__RequestVerificationToken = $("form input[name=__RequestVerificationToken]").val();
        return data;
    }

    function makeRowEditable(row) {
        var tds;
        var input;
        // set any active row to inactive
        $(".editingRow").each(function () {
            this.classList.remove("editingRow");
            tds = this.getElementsByTagName("td");
            for (var i = 0; i < tds.length; i++) {
                if (checkClassName(tds[i].classList)) {
                    input = tds[i].getElementsByTagName("input")[0];
                    input.disabled = true;
                    input.value = (input.attributes["originalVal"].value);

                }
            }
        });
        if (row) {
            // set new row to active row
            row.classList.add("editingRow");
            tds = row.getElementsByTagName("td");
            for (var i = 0; i < tds.length; i++) {
          
                    input = tds[i].getElementsByTagName("input")[0];
                    input.disabled = false;
                    input.setAttribute("originalVal", input.value);
           
            }
        }

    }
});


