document.addEventListener("DOMContentLoaded", () => {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
})

function createAccount() {
    window.location.href = "/User/Register/";
}

function goToLoginPage() {
    window.location.href = "/User/Login";
}

function register() {
    var email = document.querySelector("#email").value;
    var password = document.querySelector("#password").value;
    var confirmPassword = document.querySelector("#confirmPassword").value;
    if (password != confirmPassword) {
        toastr.error('Password does not match');
        return false;
    }
    var regObj = { Email: email, Password: password };

    sendData(regObj, "/User/Register").then(response => {
        if (response.result) {
            toastr.success("Successful Registration. Redirecting to Login Page");
            window.location = response.url;
        } else {
            toastr.error("Email already exists");
            return false;
        }
    })
}

function signIn() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var authObj = { Email: email, Password: password };
    sendData(authObj, "/User/Login").then((response) => {
        if (response.result) {
            toastr.success("Authentication Successful");
            window.location = response.url;
        } else {
            toastr.error("Unable to Authenticate");
            return false;
        }
    });
}

function sendData(dataObj, url) {
    return fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dataObj)
        })
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}