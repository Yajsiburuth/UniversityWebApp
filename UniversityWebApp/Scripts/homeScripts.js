function loadData() {
    var serverCall = new ServerCall({ url: "/User/GetUser", callMethod: "GET" });
    serverCall.fetchApiCall().then(response => {
        if (response.user != null) {
            //toastr.success("Logged In");
            var navTitle = document.getElementById("navTitle");
            console.log(navTitle);
            var userEmail = response['user']['Email'];
            navTitle.innerText = userEmail;
        } else {
            toastr.error("Unable to load");
        }
    })
    var serverCallStudent = new ServerCall({ url: "/Student/GetStatus", callMethod: "GET" });
    serverCallStudent.fetchApiCall().then(responseStudent => {
        console.log(responseStudent);
        var statusHeader = document.getElementById("enrollmentStatus");
        var existingText = statusHeader.innerText;
        var outputText = existingText.concat(" ", responseStudent.status);
        statusHeader.innerText = outputText;
    })
}

function redirectToStudentRegistration() {
    window.location.href = "/Student/Register";
}

function RedirectToProfile() {
    window.location.href = "/Student/StudentProfile";
}

function logout() {
    window.location.href = "/User/Login";
}