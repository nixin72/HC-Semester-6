function submitFiles() {
    /*
     * This handles the logic for uploading the documents from the dropzone to the database. It will call the
     * SaveChangesToDB method from the UploadedDocumentsController and once it is successfull, will call the
     * ShowViewDocuments which will re-display the table with the updated list of documents.
     */

    const isTotalFileSizeValidUri = $('#uri').attr('uri-istotalfilesizevalid');
    const saveChangesToDbUri = $('#uri').attr('uri-savechangestodb');
    const isRACAdvisor = checkIsRACAdvisor();

    // First, we confirm that the user has not exceeded their maximum file size limit of 30 GB.
    $.ajax({
        type: 'POST',
        url: isTotalFileSizeValidUri,
        data: { isAddedByRACAdvisor: isRACAdvisor },
        success: function(resp) {
            const result = JSON.parse(resp);
            
            // If a NullReferenceException has occured server side, that means a document is either corrupt or empty
            if (result.exception === 'NullReferenceException') {
                const errorDiv = $('#uploadFileErrorMessage');
                errorDiv.text('An error has occured while uploading your document(s):' +
                    ' Please verify the documents are not corrupted or empty, and try again.');
                return;
            }

            // If they have not exceeded their maximum file size
            if (result.isSuccess) {
                $.get(saveChangesToDbUri)
                    .done(function() {
                        location.reload();
                    });
            } else {
                // If they have exceeded their maximum file size
                const errorDiv = $('#uploadFileErrorMessage');
                errorDiv.text('An error has occured while uploading your document(s):' +
                    ' Total file size off all combined documents cannot exceed 30 MB' +
                    ' Please remove unneccisary documents, and try again.');
            }
        }
    });
};

function cutOffText() {
    /* Used to prevent text from causing formatting issues on mobile */

    if (window.innerWidth < 720) {
        $('.document').each(function(k, el) {
            const docName = el.querySelector('td:nth-child(1)');
            if (docName.innerText.length > 12) {
                docName.innerText = docName.innerText.substring(0, 11) + '...';
            }

            const ext = el.querySelector('td:nth-child(2)');
            ext.innerText = el.querySelector('td:nth-child(5)').innerText;

            const dwnld = el.querySelector('td:nth-child(4)');
            dwnld.innerHTML = dwnld.innerHTML.replace(' File', '');
            dwnld.innerHTML = dwnld.innerHTML.replace('|', '<br />');
        });
    }
}

function showViewDocuments() {
    /*
     * This function will make a call to ShowViewDocuments from the UploadDocumentController which will return a partial
     * view result of the table to display all of the uploaded documents and add it to the Div with the id of "view" in the
     * _CandidateHome.cshtml page. 
     */

    const showViewDocumentsUri = $('#uri').attr('uri-showviewdocuments');
    const isRACAdvisor = checkIsRACAdvisor();

    if (!isRACAdvisor) {
        if ($('#viewCandidateUploaded') != null) {
            $.ajax({
                type: 'POST',
                data: { isAddedByRACAdvisor: false },
                url: showViewDocumentsUri,
                success: function(data) {
                    $('#viewCandidateUploaded').html(data);
                    cutOffText();
                }
            });
        }

        if ($('#viewAdvisorUploaded') != null) {
            $.ajax({
                type: 'POST',
                data: { isAddedByRACAdvisor: true },
                url: showViewDocumentsUri,
                success: function(data) {
                    $('#viewAdvisorUploaded').html(data);
                    cutOffText();
                }
            });
        }
    } else {
        $.ajax({
            type: 'POST',
            data: { isAddedByRACAdvisor: false },
            url: showViewDocumentsUri,
            success: function(data) {
                $('#viewCandidateUploaded').html(data);
                cutOffText();
            }
        });

        $.ajax({
            type: 'POST',
            data: { isAddedByRACAdvisor: true },
            url: showViewDocumentsUri,
            success: function(data) {
                $('#viewAdvisorUploaded').html(data);
                cutOffText();
            }
        });
    }
}

function confirmDeleteFile(el) {
    /* Create the deletion confirmation modal. */

    changePopupHeader('Confirm');
    let text =
        'Are you sure that you would like to remove this file? It will be removed from the RAC Request and will not be accessible.<br /><br />';
    text += `<button onclick='callDeleteControl(${el.id
        });'>Yes, I'm sure</button> | <a href='#' onclick='hidePopup();' >Cancel</a>`;
    changePopupContent(text);
    showPopup();
}

function checkIsRACAdvisor() {
    /* Check if current user is a RAC Advisor. */
    const getCheckIsRacAdvisor = $('#uri').attr('uri-checkisracadvisor');
    var isRACAdvisor = false;
    $.ajax({
        type: 'GET',
        url: getCheckIsRacAdvisor,
        async: false,
        success: function(resp) {
            isRACAdvisor = resp;
        }
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
    const isRACAdvisor = checkIsRACAdvisor();
    $.ajax({
        type: 'POST',
        data: {
            isAddedByRACAdvisor: isRACAdvisor,
            id: id
        },
        url: deleteFileFromDbUri,
        success: function() {
            location.reload();
        }
    });
}

function removeUnread(id) {
    $(`#${id}`).removeClass('unreadDocument');
}

(function() {
    // The following sets the options for our dropzone
    if (typeof Dropzone != 'undefined') {
        Dropzone.options.dropzoneForm = {
            // `init` funs when the dropzone is loaded. As per the documentation, `init` should hold all of the event
            // listeners used to extend/override dropzone functionality
            init: function () {
                // Listener for `error` displays the error message and hides the arrow bubble that looks out of place
                this.on('error',
                    function (file, errorMessage) {
                        const errorDiv = $('#uploadFileErrorMessage');
                        errorDiv.append(`An error has occured while uploading ${file.name}: ${errorMessage}<br />`);

                        if (file.previewElement) {
                            file.previewElement.classList.add('dz-error');
                        }

                        const errorBubbles = document.querySelectorAll('.dz-error-message');
                        for (let index in errorBubbles) {
                            if (errorBubbles.hasOwnProperty(index)) {
                                errorBubbles[index].classList.add('hidden');
                            }
                        }
                    });

                // Listener for `success` triggers the server to store all uploaded files to the database if there are no
                // more files queued up or in the process of being uploaded
                this.on('success',
                    function () {
                        if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                            submitFiles();
                        }
                    });
            },
            // `#appsettings` is located inside `_UploadDocuments.cshtml`
            acceptedFiles: $('#appsettings').attr('acceptedfiletypes'),
            // `maxFileSize` is in MB
            maxFilesize: 10
        };
    }
   

    // Call the server to display the documents to the page
    showViewDocuments();
})();