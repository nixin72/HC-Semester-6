$(document).ready(function () {
    $('.cheveron-controller').on('click', function (e) {
        $(e.target).find('span').toggleClass('glyphicon-chevron-right glyphicon-chevron-down');
    });
});