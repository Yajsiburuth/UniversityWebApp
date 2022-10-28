var counter = 0
var subjectList;
var dictSubjectList = [];

document.addEventListener("DOMContentLoaded", () => {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
})

function addResultRow() {
    counter++;
    if (counter < 3) {
        var table = document.getElementById("AddItemsTable");
        var row = table.insertRow(-1);

        row.innerHTML =
            '<th><select required onchange="checkValue()" id="dropdownSubjects' + counter + '" name="dropdownSubjects' + counter + '" class="form-control"></select></th>' +
            '<th><select required id="SubjectResult[' + counter + '].Result" "name="SubjectResult[' + counter + '].Result" class="form-control">' +
            '<option value="">-</option>' +
            '<option value="A">A - 10 Points</option>' + 
            '<option value= "B">B - 8 Points</option>' +
            '<option value="C">C - 6 Points</option>' +
            '<option value="D">D - 4 Points</option>' +
            '<option value="E">E - 2 Points</option>' +
            '<option value="F">F - 0 Points</option>' +
            '</select></th>' +
            '<th><button class="fa fa-close" style="color:red; border:0;background-color: transparent;font-size: large;" onclick="removeResultRow()" id="removeButton' + counter + '" /></th>';

        var dropdown = document.getElementById("dropdownSubjects" + counter);
        addSubjectsToDropdown(dropdown);
        $('#removeButton' + (counter - 1)).hide();
        if (counter > 1) {
            $('#addButton').hide();
        }
    }
}

function removeResultRow() {
    counter--;
    var td = event.target.parentNode;
    var tr = td.parentNode;
    tr.parentNode.removeChild(tr);
    for (var i = 0; i < dictSubjectList.length; i++) {
        if (dictSubjectList[i].dropdownId == ("dropdownSubjects" + counter)) {
            dictSubjectList.splice(i, 1);
        }
    }
    if (counter > 0) {
        $('#removeButton' + counter).show();
    }
    if (counter < 3) {
        $('#addButton').show();
    }

}

function checkValue() {
    var existId = false;
    var existingSubject = false;
    var pair = { dropdownId: event.target.id, subjectId: event.target.value };
    for (var i = 0; i < dictSubjectList.length; i++) {
        if (dictSubjectList[i].dropdownId == event.target.id) {
            existId = true;
            if (event.target.value == "") {
                dictSubjectList.splice(i, 1);
            } else {
                dictSubjectList[i].subjectId = event.target.value;
            }
        }
    }
    if (!existId) {
        for (var i = 0; i < dictSubjectList.length; i++) {
            if (dictSubjectList[i].subjectId == event.target.value) {
                //alert("Cannot select same subject");
                event.target.value = "";
                existingSubject = true;
            }
        }
    }
    if (!existId && !existingSubject) {
        dictSubjectList.push(pair);
    }
    console.table(dictSubjectList);
}

function loadDropdown() {
    var serverCall = new ServerCall({ url: "/Subject/GetSubjects", callMethod: "GET" });
    serverCall.fetchApiCall().then(response => {
        if (response.result) {
            //toastr.success("Loaded");
            subjectList = response.subjectList;
            var dropdown = document.getElementById("dropdownSubjects0");
            addSubjectsToDropdown(dropdown);
        } else {
            console.log()
            toastr.error("Unable to load");
        }
    })
}

function addSubjectsToDropdown(dropdown) {
    var elementOption = document.createElement("option");
    elementOption.label = "---- Select Subject ----";
    elementOption.value = "";
    dropdown.add(elementOption);

    for (var i = 0; i < subjectList.length; i++) {
        var option = subjectList[i];
        var elementOption = document.createElement("option");
        elementOption.text = option['SubjectName'];
        elementOption.value = option['SubjectId'];
        dropdown.add(elementOption);
    }
}

function createStudent() {
    var firstName = document.querySelector('#FirstName').value;
    var lastName = document.querySelector('#LastName').value;
    var phoneNumber = document.querySelector('#PhoneNumber').value;
    var dateOfBirth = document.querySelector('#DateOfBirth').value;
    var nationalId = document.querySelector('#NationalId').value;
    var guardianName = document.querySelector('#GuardianName').value;
    var studentId;
    var selectedSubjectList = [];
    var selectedSubjectResultList = [];
    console.log(firstName, lastName, phoneNumber, dateOfBirth, nationalId, guardianName);

    for (var i = 0; i < dictSubjectList.length; i++) {
        var dropdownId = dictSubjectList[i].dropdownId;
        var subjectId = dictSubjectList[i].subjectId;
        var result = document.getElementById("SubjectResult[" + dropdownId.charAt(dropdownId.length - 1) + "].Result").value;
        selectedSubjectList.push(subjectId);
        selectedSubjectResultList.push(result);
    }
    console.table(selectedSubjectList);
    console.table(selectedSubjectResultList);

    studentDataObj = { FirstName: firstName, LastName: lastName, PhoneNumber: phoneNumber, DateOfBirth: dateOfBirth, NationalId: nationalId, GuardianName: guardianName }
    var studentServerCall = new ServerCall({ url: "/Student/CreateStudent", parameters: studentDataObj, callMethod: "POST" });
    studentServerCall.fetchApiCall().then(response => {
        if (response.result) {
            studentId = response.studentId;
            console.log(studentId);
            subjectResultsDataObj = { StudentId: studentId, subjectId: selectedSubjectList, result: selectedSubjectResultList };
            console.table(subjectResultsDataObj);
            var subjectResultsServerCall = new ServerCall({ url: "/SubjectResult/CreateResults", parameters: subjectResultsDataObj, callMethod: "POST" });
            subjectResultsServerCall.fetchApiCall().then(response => {
                if (response.result) {
                    toastr.success("Registered sucessfully!");
                    window.location = "/Home/Index"
                } else {
                    toastr.error("Unable to add results");
                    return false;
                }
            })
        } else {
            toastr.error('Unable to create new Student');
            return false;
        }
    })
}
