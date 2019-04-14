$(document).ready(function() {
    $('.btn-primary').click(function(e) {
        console.log($(e.target).find('span'));
        if ($(e.target).find('span').hasClass('glyphicon-chevron-right')) {
            $(e.target).find('span').addClass('glyphicon-chevron-down').removeClass('glyphicon-chevron-right');
        } 
        else
        {
            $(e.target).find('span').addClass('glyphicon-chevron-right').removeClass('glyphicon-chevron-down');
        }
    });
});