/*
 * This is code I coppied over from dropzone.js. I added method calls at the appropriate spots. I added comments so you can see what I did. 
 */
var drop;


function initializeDropZone() {
    $('#dropzoneForm').dropzone({
        addRemoveLinks: true,
        removeAllFiles: function(cancelIfNecessary) {
            return initialize_removeAllFiles(this, cancelIfNecessary);
        },
        addedfile: function(file) {
            return initialize_addedFile(this, file);

        },
        removedfile: function(file) {
            return initialize_removedFile(this, file);
        },
        acceptedFiles: $('#appsettings').attr('acceptedfiletypes'),
        maxFilesize: 10,
        error: (file, errorMessage) => {
            const errorDiv = $('#uploadFileErrorMessage');
            errorDiv.text(`An error has occured while uploading your document: ${errorMessage}`);

            if (file.previewElement) {
                file.previewElement.classList.add('dz-error');
            }

            const errorBubbles = document.querySelectorAll('.dz-error-message');
            for (let index in errorBubbles) {
                if (errorBubbles.hasOwnProperty(index)) {
                    errorBubbles[index].classList.add('hidden');
                }
            }
        }
    });
}

function initialize_removeAllFiles(dropzone, cancelIfNecessary) {
    var file;
    cancelIfNecessary = cancelIfNecessary != null;
    const ref = dropzone.files.slice();

    for (let i = 0, len = ref.length; i < len; i++) {
        file = ref[i];
        if (file.status !== Dropzone.UPLOADING || cancelIfNecessary) {
            dropzone.removeFile(file);
        }
    }

    return null;
}

function initialize_addedFile(dropzone, file) {
    if (dropzone.element === dropzone.previewsContainer) {
        dropzone.element.classList.add('dz-started');
    }

    if (dropzone.previewsContainer) {
        return previewsContainer(dropzone, file);
    } else {
        return null;
    }
}

function previewsContainer(dropzone, file) {
    var node;
    file.previewElement = Dropzone.createElement(dropzone.options.previewTemplate.trim());
    file.previewTemplate = file.previewElement;

    dropzone.previewsContainer.appendChild(file.previewElement);

    const ref = file.previewElement.querySelectorAll('[data-dz-name]');
    for (let i = 0, len = ref.length; i < len; i++) {
        node = ref[i];
        node.textContent = file.name;
    }

    const ref1 = file.previewElement.querySelectorAll('[data-dz-size]');
    for (let j = 0, len1 = ref1.length; j < len1; j++) {
        node = ref1[j];
        node.innerHTML = dropzone.filesize(file.size);
    }

    if (dropzone.options.addRemoveLinks) {
        file._removeLink =
            Dropzone.createElement(
                `<a class='dz-remove' href='javascript:undefined;' data-dz-remove>${dropzone.options.dictRemoveFile
                }</a>`);
        file._removeLink.addEventListener('click',
            function(e) {
                const id = e.target.id;
                RemoveFile(id);
            });
        file.previewElement.appendChild(file._removeLink);
    }

    const removeFileEvent = (function(ev) {
        return function(e) {
            e.preventDefault();
            e.stopPropagation();
            if (file.status === Dropzone.UPLOADING) {
                return Dropzone.confirm(ev.options.dictCancelUploadConfirmation,
                    function() {
                        return ev.removeFile(file);
                    });
            } else {
                if (ev.options.dictRemoveFileConfirmation) {
                    return Dropzone.confirm(ev.options.dictRemoveFileConfirmation,
                        function() {
                            return ev.removeFile(file);
                        });
                } else {
                    return ev.removeFile(file);
                }
            }
        };
    })(dropzone);

    const ref2 = file.previewElement.querySelectorAll('[data-dz-remove]');
    const results = [];

    var removeLink;
    for (let k = 0, len2 = ref2.length; k < len2; k++) {
        removeLink = ref2[k];
        results.push(removeLink.addEventListener('click', removeFileEvent));
    }

    AssignIdToPreviews();
    $('.dz-message').hide();

    return results;
}

function initialize_removedFile(dropzone, file) {
    var ref;
    if (file.previewElement) {
        if ((ref = file.previewElement) != null) {
            ref.parentNode.removeChild(file.previewElement);
        }
    }

    return dropzone._updateMaxFilesReachedClass();
}

/*
 * This function will assign an id to the visible previews once you upload a document to the dropzone. This is used to keep track of which one is removed/kept.
 */
function AssignIdToPreviews() {
    const removeBtns = $('.dz-remove');
    if (removeBtns != null) {
        for (let i = 0; i < removeBtns.length; i++) {
            $(removeBtns[i]).attr('id', i);
        }
    }
}


function RemoveFile(id) {
    /*
     * This will remove the file from the local list of files you want to upload. This will NOT affect the database it
     * is only for the dropzone. 
     */
    const removeFileFromLocalUri = $('#uri').attr('uri-removefilefromlocal');

    $.ajax({
        url: removeFileFromLocalUri,
        type: 'POST',
        data: JSON.stringify({ 'ind': id }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: function(xhr, textStatus, errorThrown) {
            alert(textStatus + '\n' + errorThrown);
        },
        success: function() {
            AssignIdToPreviews();
        }
    });
}

/*
 * This method is called in dropzone.js on line 1390 in Dropzone.prototype._finished
 */
var submitFiles = function() {
    /*
     * This handles the logic for uploading the documents from the dropzone to the database. It will call the
     * SaveChangesToDB method from the UploadedDocumentsController and once it is successfull, will call the
     * ShowViewDocuments which will re-display the table with the updated list of documents.
     */

    const isTotalFileSizeValidUri = $('#uri').attr('uri-istotalfilesizevalid');
    const saveChangesToDbUri = $('#uri').attr('uri-savechangestodb');
    var isRACAdvisor = CheckIsRACAdvisor();
    // First, we confirm that the user has not exceeded their maximum file size limit of 30 GB.
    $.ajax({
        type: 'POST',
        url: isTotalFileSizeValidUri,
        data: { isAddedByRACAdvisor: isRACAdvisor },
        success: function(resp) {
            const result = JSON.parse(resp);
            if (result.isSuccess) {
                $.get(saveChangesToDbUri)
                    .done(() => {
                        location.reload();
                    });
            } else {
                const errorDiv = $('#uploadFileErrorMessage');
                errorDiv.text('An error has occured while uploading your document(s):' +
                    ' Total file size off all combined documents cannot exceed 30 MB' +
                    ' Please remove unneccisary documents, and try again.');
            }
        }
    });
}

function ShowViewDocuments() {
    /*
     * This function will make a call to ShowViewDocuments from the UploadDocumentController which will return a partial
     * view result of the table to display all of the uploaded documents and add it to the Div with the id of "view" in the
     * _CandidateHome.cshtml page. 
     */

    const showViewDocumentsUri = $('#uri').attr('uri-showviewdocuments');
    var isRACAdvisor = CheckIsRACAdvisor();

    if (!isRACAdvisor) {

        if ($('#viewCandidateUploaded') != null) {
            $.ajax({
                type: 'POST',
                data: { isAddedByRACAdvisor: false },
                url: showViewDocumentsUri,
                success: function (data) {
                    $('#viewCandidateUploaded').html(data);
                }
            });
        }
        

        if ($('#viewAdvisorUploaded') != null) {
            $.ajax({
                type: 'POST',
                data: { isAddedByRACAdvisor: true },
                url: showViewDocumentsUri,
                success: function (data) {
                    $('#viewAdvisorUploaded').html(data);
                }
            });
        }

    } else {
        $.ajax({
            type: 'POST',
            data: { isAddedByRACAdvisor: false },
            url: showViewDocumentsUri,
            success: function (data) {
                $('#viewCandidateUploaded').html(data);
            }
        });

        $.ajax({
            type: 'POST',
            data: { isAddedByRACAdvisor: true },
            url: showViewDocumentsUri,
            success: function (data) {
                $('#viewAdvisorUploaded').html(data);
            }
        });
    }

}

function confirmDeleteFile(el) {
    changePopupHeader('Confirm');

    let text = 'Are you sure that you would like to remove this file? It will be removed from the RAC Request and will not be accessible.<br /><br />';
    text += "<button onclick='callDeleteControl(" + el.id + ");'>Yes, I'm sure</button> | <a href='#' onclick='hidePopup();' >Cancel</a>";
    changePopupContent(text);
    showPopup();
}

function CheckIsRACAdvisor() {
        const GetCheckIsRACAdvisor = $('#uri').attr('uri-checkisracadvisor');
        var isRACAdvisor = false;
        $.ajax({
            type: 'GET',
            url: GetCheckIsRACAdvisor,
            async : false,
            success: (resp) => { isRACAdvisor = resp }
        });

        return isRACAdvisor;
    }
function callDeleteControl(id) {
    /*
     * This function is called whenever a file is to be removed from the database.
     *
     * Parameters:
     * el - The element representing the document that will be deleted.
     */

    const deleteFileFromDbUri = $('#uri').attr('uri-deletefilefromdb');
    var isRACAdvisor = CheckIsRACAdvisor();
    $.ajax({
        type: 'POST',
        data: {
            isAddedByRACAdvisor: isRACAdvisor,
            id : id
        },
        url: deleteFileFromDbUri,
        success: function(){
            location.reload();
        }
    });
}

function removeUnread(id) {
    $('#' + id).removeClass('unreadDocument');
}

(() => {
        ShowViewDocuments();
        initializeDropZone();
})();
