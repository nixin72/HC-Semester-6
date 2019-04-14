$(document).ready(function () {
    const deleteNotificationURI = $('#uri').attr('uri-deleteNotification');
    $('.removeNotification').click(function () {
        Delete(this.id);
    });

    function Delete(Id) {
        $.ajax({
            url: deleteNotificationURI,
            data: {
                id: Id
            },
            error: function () {
                console.log('An error has occured');
            },
            success: function (data) {
                $('.table').html(data);
                $('.removeNotification').unbind();
                $('.removeNotification').click(function() {
                    Delete(this.id);
                });
            },
            type: 'POST'
        });
    }
});