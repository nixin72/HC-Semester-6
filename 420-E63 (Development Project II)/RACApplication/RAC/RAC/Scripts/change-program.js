$(function() {

    checkContainsGenEds();

    $('#ProgramId').change(function () {
        checkContainsGenEds();
        $('#chbGenEdOnly').prop('checked', false);
    });

    function AddAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('form input[name=__RequestVerificationToken]').val();
        return data;
    }

    function checkContainsGenEds() {
        var programId = $('#ProgramId').val();

        //If a valid programId is selected
        if ($.isNumeric(programId)) {
            ;
            $.ajax({
                type: 'POST',
                url: $('#uri').attr('uri-containsgeneds'),
                data: AddAntiForgeryToken({ id: programId }),
                dataType: 'json',
                success: function (response) {
                    enableDisableGenEd(response);
                }
            });
        } else {
            enableDisableGenEd(false);
        }
    }

    function enableDisableGenEd(containsGenEds) {
        if (containsGenEds) {
            $('#chbGenEdOnly').attr('disabled', false);
            $('#chbGenEdOnly').closest('div').removeClass('disabled');
        }
        else {
            $('#chbGenEdOnly').attr('disabled', true);
            $('#chbGenEdOnly').closest('div').addClass('disabled');
        }
    }
});