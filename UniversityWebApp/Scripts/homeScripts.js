function loadEmail() {
    loadData("/User/GetUser").then((response) => {
        if (response.user != null) {
            toastr.success("Logged In");
            var header = document.getElementById("emailDisplay");
            var existingText = header.innerText;
            var userEmail = response['user']['Email'];
            var outputText = existingText.concat(" ", userEmail);
            header.innerText = outputText;
        } else {
            toastr.error("Unable to load");
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!');
        });
}

function loadData(url) {
    return fetch(url)
        .then(response => { return response.json(); })
        .catch((error) => console.log(error))
}

function redirectToStudentRegistration() {
    window.location.href = "/Home/RegisterStudent";
}

function logout() {
    window.location.href = "/User/Login";
}

function addResultRow() {
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
}