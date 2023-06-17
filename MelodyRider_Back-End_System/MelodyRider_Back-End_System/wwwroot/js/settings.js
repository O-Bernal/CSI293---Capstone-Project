$(document).ready(function () {
    // Prevent form submission if username or email fields are empty
    $('#settings-form').on('submit', function (e) {
        var username = $('#username').val();
        var email = $('#email').val();
        var currentPassword = $('#oldPassword').val();
        var newPassword = $('#newPassword').val();
        var confirmPassword = $('#confirmPassword').val();

        if (!username && !email && !currentPassword && !newPassword && !confirmPassword) {
            alert('No changes made.');
            e.preventDefault();
        }
        if ((newPassword || confirmPassword) && !currentPassword) {
            alert('You must enter your current password to change your password.');
            e.preventDefault();
        }
        if (newPassword !== confirmPassword) {
            alert('New password and confirm password do not match.');
            e.preventDefault();
        }
    });

    // Toggle visibility of scores table when "View Scores" button is clicked
    $('#viewScoresButton').click(function (event) {
        event.preventDefault();
        $('#scoresTable').toggle();
    });

    // Show the delete modal when the delete button is clicked
    $('#deleteButton').click(function (event) {
        event.preventDefault();
        $('#deleteModal').modal('show');
    });

    // Verify the user's password when the delete button is clicked
    $('#confirmDelete').click(function () {
        var password = $('#deleteConfirmPassword').val();
        if (password) {
            $.ajax({
                type: 'POST',
                url: deleteUrl,
                data: JSON.stringify({ OldPassword: password }),
                contentType: 'application/json',
                success: function (response) {
                    if (response === true) {
                        // Password is correct and user is deleted
                        alert('User deleted successfully.');
                        window.location.href = '@Url.Action("Index", "Game")';
                    } else {
                        alert('Incorrect password.');
                    }
                }
            });
        } else {
            alert('You must enter your password to confirm deletion.');
        }
    });
});