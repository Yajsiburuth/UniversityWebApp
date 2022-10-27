
function DisplaySummaryTable() {
    
}

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



/*function addResultRow() {
    if (counter < 3) {
        var table = document.getElementById("AddItemsTable");

        var row = table.insertRow(-1);
        var cell1 = row.insertCell(0);

        cell1.innerHTML = '<input type="text" id="' + counter + '"name="Grade[' + counter + '].Result"/> <button class="fa fa-close" style="color:red; border:0;background-color: transparent;font-size: large;" onclick="removeResultRow(this.id)" id="' + counter + '"/>';
        counter++;
        if (counter != 0) {
            var button = document.getElementById(counter);
            button.remove();
        }
    }
}

function removeResultRow() {
    var td = event.target.parentNode;
    var tr = td.parentNode;
    tr.parentNode.removeChild(tr);
    counter--;
}*/