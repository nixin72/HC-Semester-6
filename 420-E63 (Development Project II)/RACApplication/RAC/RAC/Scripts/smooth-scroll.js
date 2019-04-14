$(function () {
    $(window).scroll(function() {

        // Check if the window size has increased since page load
        checkForScrolling();

        // Hide the _Jump Top_ button if the page is at the top
        if ($(window).scrollTop() === 0) {
            $('#btnScrollToTop').hide();
        } else {
            $('#btnScrollToTop').show();
        }

        // Hide the _Jump Bottom_ button if the page is at the bottom
        if ($(window).scrollTop() + $(window).height() === $(document).height()) {
            $('#btnScrollToBottom').hide();
        } else {
            $('#btnScrollToBottom').show();
        }
    });

    function checkForScrolling() {
        /* Checks if the page is _scrollable_. If it is, show the scroll buttons */
        if ($('body').height() >= $(window).height()) {
            $('#divScrollButtons').show();
        } else {
            $('#divScrollButtons').hide();
        }
    }

    $('#btnScrollToTop').click(function () {
        console.log("click");
        $('html, body').animate({ scrollTop: 0 }, 400);
    });

    $('#btnScrollToBottom').click(function () {
        $('html, body').animate({ scrollTop: $(document).height() }, 400);
    });
    
    checkForScrolling();
});

$(window).resize(function () {
    if ($(document).height() <= $(window).height()) {
        $('#divScrollButtons').hide();
    }
});