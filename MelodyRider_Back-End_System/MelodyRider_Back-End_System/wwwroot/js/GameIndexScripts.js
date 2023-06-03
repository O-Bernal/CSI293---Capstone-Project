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
productName: "Rhythm Game Web App",
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
// Get the modal and the sign in button
var modal = document.getElementById('login-modal');
var signInButton = document.getElementById('sign-in-button');

// Show the modal when the sign in button is clicked
signInButton.onclick = function (event) {
    event.preventDefault();
modal.classList.add("show");
        }

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
window.onclick = function (event) {
            if (event.target == modal) {
    modal.classList.remove("show");
            }
        }

/* Submit the form using AJAX
    * ==========================
    * This script will prevent the form from being submitted in the usual way and instead submit it using AJAX. 
    * The serialize method is used to create a string of the form data, which is sent to the server. 
    * If the server responds with a success status, a message is shown in the modal form. 
    * If an error occurs, an error message is shown.
    */
$('#signup-form').on('submit', function (e) {
    e.preventDefault();

var username = $(this).find('input[name="Username"]').val();
var password = $(this).find('input[name="Password"]').val();
var email = $(this).find('input[name="Email"]').val();

$.ajax({
    type: 'POST',
url: '/User/Create',
data: {
    Username: username,
Password: password,
Email: email
                },
success: function (data) {
    alert("User " + data.username + " created successfully!");
                },
error: function (jqXHR) {
                    var response = jqXHR.responseJSON;
if (response.error) {
    alert("An error occurred: " + response.error.join("\n"));
                    }
                }
            });
        });

// Password strength checker
$(document).ready(function () {
    $('#Password').on('input', function () {
        var password = $(this).val();
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        var hasNumbers = /\d/.test(password);
        var hasNonalphas = /\W/.test(password);
        if (hasUpperCase && hasLowerCase && hasNumbers && hasNonalphas) {
            $('#passwordHelp').text('Password is strong').css('color', 'green');
        } else {
            $('#passwordHelp').text('Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character').css('color', 'red');
        }
    });
});
