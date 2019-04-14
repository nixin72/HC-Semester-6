$(document).ready(function () {
    $('#Description').rules('add', {
        required: true,
        messages: {
            required: 'Please enter a description',
        }
    });

})