function MakeAllCellsEditable() {
    let weightCells = document.querySelectorAll("table tr td");
    weightCells.forEach(td => {
        td.setAttribute('contentEditable', true);
        td.setAttribute('class', 'edit');
    });
    document.getElementById("editButton").disabled = true;
    document.getElementById("cancelButton").disabled = false;
    document.getElementById("saveButton").disabled = false;
    document.getElementById("downloadButton").disabled = true;

}
function CancelChanges() {
    let weightCells = document.querySelectorAll("table tr td");
    weightCells.forEach(td => {
        td.setAttribute('contentEditable', false);
        td.removeAttribute('class');
    });
    DisplaySummary();
    document.getElementById("cancelButton").disabled = true;
    document.getElementById("saveButton").disabled = true;
    document.getElementById("downloadButton").disabled = false;
    document.getElementById("editButton").disabled = false;

}

function SaveChanges() {
    let weightCells = document.querySelectorAll("table tr td");
    weightCells.forEach(td => {
        td.setAttribute('contentEditable', false);
        td.removeAttribute('class');
    });
    var table = document.getElementById("studentSummary");
    var tbody = table.tbody;
    for (var i = 0; i < tbody.rows.length; i++) {
        var row = tbody.rows[i];
        console.log(row);
    }

    document.getElementById("cancelButton").disabled = true;
    document.getElementById("saveButton").disabled = true;
    document.getElementById("editButton").disabled = false;
    document.getElementById("downloadButton").disabled = false;
}

