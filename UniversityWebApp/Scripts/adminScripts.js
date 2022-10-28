
function DisplaySummaryTable() {
    
}

function loadEmail() {
    var serverCall = new ServerCall({ url: "/User/GetUser", callMethod: "GET" });
    serverCall.fetchApiCall().then(response => {
        if (response.user != null) {
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

function logout() {
    window.location.href = "/User/Login";
}

function DisplaySummary() {
    loadData("/Home/GetStudentsSummary").then((response) => {
        if (response.result) {
            toastr.success("Logged In");
        } else {
            toastr.error("Unable to load");
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!');
        });

}