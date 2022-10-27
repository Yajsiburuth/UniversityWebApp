function loadEmail() {
    var serverCall = new ServerCall({ url: "/User/GetUser", callMethod: "GET" });
    serverCall.fetchApiCall().then(response => {
        if (response.user != null) {
            //toastr.success("Logged In");
            var header = document.getElementById("emailDisplay");
            var existingText = header.innerText;
            var userEmail = response['user']['Email'];
            var outputText = existingText.concat(" ", userEmail);
            header.innerText = outputText;
        } else {
            toastr.error("Unable to load");
        }
    })
}

function redirectToStudentRegistration() {
    window.location.href = "/Home/RegisterStudent";
}

function logout() {
    window.location.href = "/User/Login";
}