$(document).ready(function() {
    var isRacAdvisor = CheckIsRACAdvisor();

    if (!isRacAdvisor) {
        ShowViewDocuments(false);
    } else {
        ShowViewDocuments(true);
    }

    checkForCommentsRequired();

    const elementAnswers = document.querySelectorAll('#programSpecificCompetencyElement_Answer');

    for (let idx in elementAnswers) {
        if (elementAnswers.hasOwnProperty(idx)) {
            $(elementAnswers[idx]).click(() => {
                checkForCommentsRequired();
            });
        }
    }

    const selectAllButtons = document.querySelectorAll('#selectAll');

    for (let idx in selectAllButtons) {
        if (selectAllButtons.hasOwnProperty(idx)) {
            $(selectAllButtons[idx]).click(() => {
                selectAllAnswers();
            });
        }
    }

    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
        window.onbeforeunload = function() {
            saveRACRequest();
            return '';
        };
    } else {
        $(window).on('beforeunload',
            function() {
                saveRACRequest();
            });
    }

    function selectAllAnswers() {

    }

    function CheckIsRACAdvisor() {
        const GetCheckIsRACAdvisor = $('#uri').attr('uri-checkisracadvisor');
        var isRACAdvisor = false;
        $.ajax({
            type: 'GET',
            url: GetCheckIsRACAdvisor,
            success: (resp) => {
                isRACAdvisor = resp
            }
        });

        return isRACAdvisor;
    }

    function getCommentsAndAnswers() {
        const compentecyTables = document.querySelectorAll('.tblCompetencies');
        const commentsAnswers = [];

        for (let idx in compentecyTables) {
            if (compentecyTables.hasOwnProperty(idx)) {
                const competencyTable = compentecyTables[idx];
                const answers = competencyTable.querySelectorAll('#programSpecificCompetencyElement_Answer');
                const commentBox = competencyTable.querySelector('#competencyCommentInstance_Comment');

                commentsAnswers.push({ 'answers': answers, 'comments': commentBox });
            }
        }
        return commentsAnswers;
    }

    function checkForCommentsRequired() {
        /* Checks all answer radio buttons. If any selected are positive answers, then enable the example box. Else, disable
         the example box.*/
        const commentAnswers = getCommentsAndAnswers();

        for (let idx in commentAnswers) {
            if (commentAnswers.hasOwnProperty(idx)) {
                const compentency = commentAnswers[idx];
                compentency.comments.disabled = true;
                compentency.comments.placeholder = '';

                // TODO Could probably filter the list before running through it. Might improve performance slightly.
                for (let idx in compentency.answers) {
                    if (compentency.answers.hasOwnProperty(idx)) {
                        if (compentency.answers[idx].checked) {
                            if (compentency.answers[idx].value === '1' || compentency.answers[idx].value === '2') {
                                compentency.comments.disabled = false;
                                compentency.comments.placeholder = 'Please enter an example of your experience.';
                            }
                        }
                    }
                }
            }
        }
    }

    function areAllAnswersFilled(compentecyTables) {
        let isAllFilled = true;

        for (let idx in compentecyTables) {
            if (compentecyTables.hasOwnProperty(idx)) {
                const elementRows = compentecyTables[idx].querySelectorAll('tbody .elementRow');
                for (let idx in elementRows) {
                    if (elementRows.hasOwnProperty(idx)) {
                        const elementAnswers = elementRows[idx].querySelectorAll('input');
                        let hasAnswer = false;

                        for (let idx in elementAnswers) {
                                if (elementAnswers[idx].checked) {
                                    hasAnswer = true;
                            }
                        }

                        if (!hasAnswer) {
                            isAllFilled = false;
                            elementRows[idx].classList.add('errorBorder');
                        } else {
                            if (elementRows[idx].classList.contains('errorBorder')) {
                                elementRows[idx].classList.remove('errorBorder');
                            }
                        }
                    }
                }
            }
        }
        return isAllFilled;
    }

    function areManditoryCommentsFilled(compentecyTables) {
        let isAllFilled = true;

        for (let idx in compentecyTables) {
            if (compentecyTables.hasOwnProperty(idx)) {
                const elementRows = compentecyTables[idx].querySelectorAll('tbody .elementRow');
                const competencyExample = compentecyTables[idx].querySelector('.textArea');
                console.log(competencyExample);

                for (let idx in elementRows) {
                    if (elementRows.hasOwnProperty(idx)) {
                        const elementAnswers = elementRows[idx].querySelectorAll('input');
                        let canDo = false;

                        for (let idx in elementAnswers) {
                            if (elementAnswers.hasOwnProperty(idx)) {
                                if (elementAnswers[idx].checked && elementAnswers[idx].value < 3) {
                                    canDo = true;
                                }
                            }
                        }

                        if (canDo) {
                            if (competencyExample.value === '') {
                                competencyExample.classList.add('errorBorder');
                                isAllFilled = false;
                            } else {
                                if (competencyExample.classList.contains('errorBorder')) {
                                    competencyExample.classList.remove('errorBorder');
                                }
                            }
                        }
                    }
                }
            }
        }
        return isAllFilled;
    }

    function isOneDocumentUploaded() {
        const uploadedDocumentTable = document.querySelector('#uploadedDocumentTable');
        
        if (uploadedDocumentTable.querySelector('.document') === null) {
            uploadedDocumentTable.classList.add('errorBorder');
            return false;
        } else {
            if (uploadedDocumentTable.classList.contains('errorBorder')) {
                uploadedDocumentTable.classList.remove('errorBorder');
            }

            return true;
        }
    }

    function submitRACRequest() {
        const compentecyTables = document.querySelectorAll('.tblCompetencies');

        const isAnswersFilled = areAllAnswersFilled(compentecyTables);
        const isCommentsFilled = areManditoryCommentsFilled(compentecyTables);
        const isDocumentUploaded = isOneDocumentUploaded();
        const isAllValid = isAnswersFilled && isCommentsFilled && isDocumentUploaded;

        if (isAllValid) {
            $.ajax({
                type: 'POST',
                url: GetSubmitRACRequestURI(),
                data: addAntiForgeryToken($('#competenciesForm').serialize()),
                success: resp => {
                    const result = JSON.parse(resp);
                    if (result.isSuccess) {
                        window.location.replace(GetFallbackURI());
                    } else {
                        $('#errors')
                            .text(
                                'Something has gone wrong. Please verify you have filled out the entire form, then try again.');
                    }
                }
            });
        } else {
            const errorMessage = ['Your Self-Assessment has not been submitted: <ul>'];

            if (!isAnswersFilled) {
                errorMessage.push('<li>One or more answers has not been given.</li>');
            }

            if (!isCommentsFilled) {
                errorMessage.push('<li>One or more example has not been entered.</li>');
            }

            if (!isDocumentUploaded) {
                errorMessage.push('<li>One or more documents must be uploaded.</li>');
            }

            errorMessage.push('</ul>');
            $('#errors').html(errorMessage.join(''));
        }
    }

    function saveRACRequest() {
        if (!isRacAdvisor) {
            const getAutoSaveRacRequestUri = $('#uri').attr('uri-getautosaveracrequesturi');
            $.ajax({
                type: 'POST',
                url: getAutoSaveRacRequestUri,
                data: addAntiForgeryToken($('#competenciesForm').serialize()),
                success: () => {}
            });
        }
    }

    function addAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('#competenciesForm input[name=__RequestVerificationToken]').val();
        return data;
    }

    $('#submitRAC').click(function() {
        window.onbeforeunload = null;
        submitRACRequest();
    });

    $('#submit-all').click(function() {
        window.onbeforeunload = null;
    });

    $('#saveRAC').click(function() {
        window.onbeforeunload = null;
        //Custom ajax because we need to refresh the page on upon a success
        $.ajax({
            type: 'POST',
            url: GetSaveRACRequestURI(),
            data: addAntiForgeryToken($('#competenciesForm').serialize()),
            success: () => {
                location.reload();
            }
        });
    });

});