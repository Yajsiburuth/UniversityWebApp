$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

function createAccount() {
    window.location.href = "/User/Register/";
}

function register() {
    var email = $("#email").val(); // read email address input
    var password = $("#password").val(); // read password input
    var confirmPassword = $("#confirmPassword").val(); // read password input
    if (password != confirmPassword) {
        toastr.error('Password does not match');
        return false;
    }
    var regObj = { Email: email, Password: password };
    sendData(regObj, "/User/Register").then((response) => {
        if (response.result) {
            toastr.success("Successful Registration. Redirecting to Login Page");
            window.location = response.url;
        } else {
            toastr.error("Email already exists");
            return false;
        }
    })
        .catch((error) => {
            console.log(error);
            toastr.error("Unable to make request!");
        })
}

function signIn() {
    var email = $("#email").val(); // read email address input
    var password = $("#password").val(); // read password input
    // create object to map LoginModel
    var authObj = { Email: email, Password: password };

    sendData(authObj, "/User/Login").then((response) => {
        if (response.result) {
            toastr.success("Authentication Succeed. Redirecting to relevent page.....");
            window.location = response.url;
        }
        else {
            toastr.error('Unable to Authenticate user');
            return false;
        }
    })
        .catch((error) => {
            console.log(error);
            toastr.error('Unable to make request!');
        });
}

/*function sendData(userCredential, url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: url,
            data: userCredential,
            dataType: "json",
            success: function (data) {
                resolve(data)
            },
            error: function (error) {
                reject(error)
            }
        })
    });
}*/

function sendData(userCredential, url) {
    return new Promise((resolve, reject) => {
        let request = new XMLHttpRequest();
        request.open("POST", url, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(JSON.stringify(userCredential));
        request.onload = function () {
            if (this.status >= 200 && this.status < 400) {
                let data = this.response;
                resolve(data)
            }
        }
        request.onerror = function (error) {
            reject(error)
        };
    });
}