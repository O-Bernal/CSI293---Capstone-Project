$(document).ready(function () {
    // Prevent form submission if username or email fields are empty
    $('#settings-form').on('submit', function (e) {
        var username = $('#username').val();
        var email = $('#email').val();
        var currentPassword = $('#oldPassword').val();
        var newPassword = $('#newPassword').val();
        var confirmPassword = $('#confirmPassword').val();

        if (!username || !email) {
            alert('Username and email fields cannot be empty.');
            e.preventDefault();
        }

        if ((newPassword || confirmPassword) && !currentPassword) {
            alert('You must enter your current password to change your password.');
            e.preventDefault();
        }
    });

    // Add an 'edit' button to toggle the editability of the username and email fields
    $('#edit-username').click(function () {
        var usernameInput = $('#username');
        usernameInput.prop('readonly', !usernameInput.prop('readonly'));
    });
    $('#edit-email').click(function () {
        var emailInput = $('#email');
        emailInput.prop('readonly', !emailInput.prop('readonly'));
    });
});