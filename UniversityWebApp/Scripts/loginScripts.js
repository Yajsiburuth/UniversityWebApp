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
/*    if (password != confirmPassword) {
        toastr.error('Password does not match');
        return false;
    }*/
    var regObj = { Email: email, Password: password, ConfirmPassword: confirmPassword };
    var serverCall = new ServerCall({ url: "/User/Register", parameters: regObj, callMethod: "POST" })
    serverCall.fetchApiCall().then(response => {
        if (response.result) {
            toastr.success("Successful Registration. Redirecting to Login Page");
            window.location = response.url;
        } else if (response.url != null) {
            toastr.error("Email already exists");
        } else {
            for (var error in response.errors) {
                console.log(response.errors[error][0]);
                toastr.error(response.errors[error][0]);
            }
        }
    })
}

function signIn() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var authObj = { Email: email, Password: password };
    var serverCall = new ServerCall({ url: "/User/Authenticate", parameters: authObj, callMethod: "POST" })
    serverCall.fetchApiCall().then((response) => {
        if (response.result) {
            toastr.success("Authentication Successful");
            window.location = response.url;
        } else {
            toastr.error("Unable to Authenticate");
            return false;
        }
    })
}