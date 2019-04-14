$('form').submit(function () {
    // set this item so that the sign in page knows the user reset the password
    // this way the user can be notified that it was successful when they open the page
    if (localStorage.getItem('changedPassword') == null) {
        localStorage.setItem('changedPassword', 'true');
    }
});