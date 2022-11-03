document.addEventListener("DOMContentLoaded", () => {
    localStorage.clear();
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
})

function loadStudentData() {
    let dataCache = JSON.parse(localStorage.getItem('studentDetails'));
    if (dataCache == null) {
        var serverCallStudent = new ServerCall({ url: "/Student/GetDetails", callMethod: "GET" });
        serverCallStudent.fetchApiCall().then(response => {
            console.log(response);
            insertDataToInputs(response.studentDetails);
        })
    } else {
        insertDataToInputs(dataCache);
    }
}

function insertDataToInputs(studentDetails) {
    $("#FirstName").val(studentDetails["FirstName"]).prop("disabled", true);
    $("#LastName").val(studentDetails["LastName"]).prop("disabled", true);
    $("#Address").val(studentDetails["Address"]).prop("disabled", true);
    $("#PhoneNumber").val(studentDetails["PhoneNumber"]).prop("disabled", true);
    $("#DateOfBirth").val(convertMillisecondsToDate(studentDetails["DateOfBirth"].replace(/[^0-9 +]/g, ''))).prop("disabled", true);
    $("#NationalId").val(studentDetails["NationalId"]).prop("disabled", true);
    $("#GuardianName").val(studentDetails["GuardianName"]).prop("disabled", true);
}

function enableInputs() {
    $("#FirstName").prop("disabled", false);
    $("#LastName").prop("disabled", false);
    $("#Address").prop("disabled", false);
    $("#PhoneNumber").prop("disabled", false);
    $("#DateOfBirth").prop("disabled", false);
    $("#NationalId").prop("disabled", false);
    $("#GuardianName").prop("disabled", false);

    $("#cancelButton").prop("disabled", false);
    $("#saveButton").prop("disabled", false);
    $("#editButton").prop("disabled", true);
}

function CancelChanges() {
    loadStudentData();
    $("#cancelButton").prop("disabled", true);
    $("#saveButton").prop("disabled", true);
    $("#editButton").prop("disabled", false);
}

function SaveChanges() {
    $("#cancelButton").prop("disabled", true);
    $("#saveButton").prop("disabled", true);
    $("#editButton").prop("disabled", false);
}

function convertMillisecondsToDate(milliseconds) {
    var date = new Date(parseInt(milliseconds));
    console.log(date);
    var year = date.getFullYear()
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    var day = ("0" + date.getDate()).slice(-2);
    return `${ year }-${ month }-${ day }`
}