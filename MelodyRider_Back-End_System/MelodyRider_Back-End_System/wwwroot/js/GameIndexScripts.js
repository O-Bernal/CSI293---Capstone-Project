var container = document.querySelector("#unity-container");
var canvas = document.querySelector("#unity-canvas");
var loadingBar = document.querySelector("#unity-loading-bar");
var progressBarFull = document.querySelector("#unity-progress-bar-full");
var fullscreenButton = document.querySelector("#unity-fullscreen-button");
var warningBanner = document.querySelector("#unity-warning");

// Shows a temporary message banner/ribbon for a few seconds, or
// a permanent error message on top of the canvas if type=='error'.
// If type=='warning', a yellow highlight color is used.
// Modify or remove this function to customize the visually presented
// way that non-critical warnings and error messages are presented to the
// user.
function unityShowBanner(msg, type) {
    function updateBannerVisibility() {
        warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
    }
    var div = document.createElement('div');
    div.innerHTML = msg;
    warningBanner.appendChild(div);
    if (type == 'error') div.style = 'background: red; padding: 10px;';
    else {
        if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
        setTimeout(function () {
            warningBanner.removeChild(div);
            updateBannerVisibility();
        }, 5000);
    }
    updateBannerVisibility();
}

var buildUrl = "/unity/Build";
var loaderUrl = buildUrl + "/unity.loader.js";
var config = {
    dataUrl: buildUrl + "/unity.data.br",
    frameworkUrl: buildUrl + "/unity.framework.js.br",
    codeUrl: buildUrl + "/unity.wasm.br",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "DefaultCompany",
    productName: "Melody Rider - Alpha Build",
    productVersion: "1.0",
    showBanner: unityShowBanner,
};

// By default Unity keeps WebGL canvas render target size matched with
// the DOM size of the canvas element (scaled by window.devicePixelRatio)
// Set this to false if you want to decouple this synchronization from
// happening inside the engine, and you would instead like to size up
// the canvas DOM size and WebGL render target sizes yourself.
// config.matchWebGLToCanvasSize = false;

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
    // Mobile device style: fill the whole browser client area with the game canvas:

    var meta = document.createElement('meta');
    meta.name = 'viewport';
    meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
    document.getElementsByTagName('head')[0].appendChild(meta);
    container.className = "unity-mobile";
    canvas.className = "unity-mobile";

    // To lower canvas resolution on mobile devices to gain some
    // performance, uncomment the following line:
    // config.devicePixelRatio = 1;

    unityShowBanner('WebGL builds are not supported on mobile devices.');
} else {
    // Desktop style: Render the game canvas in a window that can be maximized to fullscreen:

    canvas.style.width = "960px";
    canvas.style.height = "600px";
}

loadingBar.style.display = "block";

var script = document.createElement("script");
script.src = loaderUrl;
script.onload = () => {
    document.getElementById("startButton").addEventListener("click", function () {
        document.getElementById('unity-container').classList.add('game-started');
        createUnityInstance(canvas, config, (progress) => {
            progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
            loadingBar.style.display = "none";
            fullscreenButton.onclick = () => {
                unityInstance.SetFullscreen(1);
            };
        }).catch((message) => {
            alert(message);
        });
    });
};
document.body.appendChild(script);

// Modal Script
document.addEventListener('DOMContentLoaded', (event) => {
    // Get the modal and the sign in button
    var modal = document.getElementById('login-modal');
    var signInButton = document.getElementById('sign-in-button');

    // Show the modal when the sign in button is clicked
    if (signInButton) {
        signInButton.onclick = function (event) {
            event.preventDefault();
            modal.classList.add("show");
        }
    }
});

// Script for login / sign up form
const loginBtn = document.getElementById('login');
const signupBtn = document.getElementById('signup');

loginBtn.addEventListener('click', (e) => {
    let parent = e.target.parentNode.parentNode;
    Array.from(e.target.parentNode.parentNode.classList).find((element) => {
        if (element !== "slide-up") {
            parent.classList.add('slide-up')
        } else {
            signupBtn.parentNode.classList.add('slide-up')
            parent.classList.remove('slide-up')
        }
    });
});

signupBtn.addEventListener('click', (e) => {
    let parent = e.target.parentNode;
    Array.from(e.target.parentNode.classList).find((element) => {
        if (element !== "slide-up") {
                parent.classList.add('slide-up')
        } else {
            loginBtn.parentNode.parentNode.classList.add('slide-up')
            parent.classList.remove('slide-up')
        }
    });
});

// Hide the modal when the user clicks outside the form
var modal = document.getElementById('login-modal');
window.onclick = function (event) {
    if (event.target == modal) {
        modal.classList.remove("show");
    }
}

// Submitting the form using AJAX
// This script handles the sign up portion of the form.
$('#signup-form').on('submit', function (e) {
    // This will prevent the form from being submitted in the usual way
    e.preventDefault();

    var username = $('input[name="NewUsername"]');
    var email = $('input[name="NewEmail"]');
    var password = $('input[name="NewPassword"]');
    var message = $('#signup-message');

    // Clear previous messages
    message.empty();

    // Validate username
    if (username.val() === '') {
        message.append('<p class="error">Username required</p>');
        username.focus();
        return;
    }

    // Validate email
    if (email.val() === '') {
        message.append('<p class="error">Email required</p>');
        email.focus();
        return;
    }

    // Validate password
    if (password.val() === '') {
        message.append('<p class="error">Password required</p>');
        password.focus();
        return;
    }

    // Check password strength
    var passwordStrengthRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/;
    if (!passwordStrengthRegex.test(password.val())) {
        message.append('<p class="error">Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 digit, and be at least 8 characters long</p>');
        password.focus();
        return;
    }

    // If all validations pass, submit the form data. 
    // The serialize method is used to create a string of the form data, which is sent to the server. 
    // If the server responds with a success status, a message is shown in the modal form. 
    // If an error occurs, an error message is shown.
    $.ajax({
        type: 'POST',
        url: '/User/Create',
        data: {
            Username: username.val(),
            Password: password.val(),
            Email: email.val()
        },
        success: function (data) {
            if (data.username) {
                alert("User " + data.username + " created successfully!");
                $('#login-modal').modal('hide'); // Close the modal
            }
        },
        error: function (jqXHR) {
            var response = jqXHR.responseJSON;
            if (response.error) {
                alert("An error occurred: " + response.error.join("\n"));
            }
        }
    });

    // The following code is an alternative to the above AJAX call.
    //fetch('/User/Create', {
    //    method: 'POST',
    //    headers: {
    //        'Content-Type': 'application/json'
    //    },
    //    body: JSON.stringify({
    //        Username: username.val(),
    //        Password: password.val(),
    //        Email: email.val()
    //    })
    //})
    //.then(response => response.json())
    //.then(data => {
    //    if (data.username) {
    //        alert("User " + data.username + " created successfully!");
    //        $('#login-modal').modal('hide'); // Close the modal
    //    }
    //})
    //.catch(error => {
    //    alert("An error occurred: " + error);
    //});
});
// This script handles the login portion of the form.
$('#login-form').on('submit', function (e) {
    e.preventDefault();

    var usernameOrEmail = $('input[name="UsernameOrEmail"]');
    var password = $('input[name="UserPassword"]');
    var message = $('#login-message');

    message.empty();

    // Validate username
    if (usernameOrEmail.val() === '') {
        message.append('<p class="error">Email or Username required</p>');
        usernameOrEmail.focus();
        return;
    }

    // Validate password
    if (password.val() === '') {
        message.append('<p class="error">Password incorrect</p>');
        password.focus();
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/User/Login',
        data: {
            UsernameOrEmail: usernameOrEmail.val(),
            Password: password.val()
        },
        success: function (data) {
            if (data.success) {
                alert("User logged in successfully!");
                document.getElementById('login-modal').classList.remove("show"); // Close the modal
                location.reload(); // Refresh the page
            } else {
                alert("An error occurred: " + data.error.join("\n"));
            }
        },
        error: function (jqXHR) {
            var response = jqXHR.responseJSON;
            if (response.error) {
                alert("An error occurred: " + response.error.join("\n"));
            }
        }
    });

    // The following code is an alternative to the above AJAX call.
    //fetch('/User/Login', {
    //    method: 'POST',
    //    headers: {
    //        'Content-Type': 'application/json'
    //    },
    //    body: JSON.stringify({
    //        UsernameOrEmail: usernameOrEmail.val(),
    //        Password: password.val()
    //    })
    //})
    //.then(response => {
    //    if (!response.ok) {
    //        throw new Error("HTTP error " + response.status);
    //    }
    //    return response.json();
    //})
    //.then(data => {
    //    if (data.success) {
    //        alert("User logged in successfully!");
    //        document.getElementById('login-modal').classList.remove("show"); // Close the modal
    //        location.reload(); // Refresh the page
    //    } else {
    //        alert("An error occurred: " + data.error.join("\n"));
    //    }
    //})
    //.catch(error => {
    //    alert("An error occurred: " + error);
    //});
    // Fetch requires you to manually set the headers and stringify the body, unlike jQuery AJAX which does it for you.
});

// Toggle visibility of dropdown when user badge is clicked
$('.user-badge').click(function (event) {
    event.stopPropagation(); // Prevent event from bubbling up to document
    $('.dropdown-content').toggle();
});
// Hide dropdown when clicking anywhere else on the document
$(document).click(function () {
    $('.dropdown-content').hide();
});

// This will create and add a new achievement for the user when the start button is clicked.
// Trying out fetch instead of jQuery AJAX here
$('#startButton').click(function () {
    var url = $(this).data('url');

    // Check if a user is logged in
    if (user) {
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'same-origin'
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                toastr.options.positionClass = 'toast-bottom-right';
                toastr.success('First achievement! Welcome ' + data.username + '!');
            } else {
                toastr.error(data.message);
            }
        })
        .catch(() => {
            toastr.error('There was an error creating the achievement');
        });
    }
});
