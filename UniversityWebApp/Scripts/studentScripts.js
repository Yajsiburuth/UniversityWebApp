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

function loadData(url) {
    return fetch(url)
               .then(response => { return response.json(); })
               .catch((error) => console.log(error))
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

function addResultRow() {
    counter++;
    if (counter < 3) {
        var table = document.getElementById("AddItemsTable");
        var row = table.insertRow(-1);

        row.innerHTML =
            '<th><select required onchange="checkValue()" id="dropdownSubjects' + counter + '" name="dropdownSubjects' + counter + '" class="form-control"></select></th>' +
            '<th><select required id="Grade[' + counter + '].Result" "name="Grade[' + counter + '].Result" class="form-control">' +
            '<option value="">-</option>' +
            '<option value="A">A</option>' + 
            '<option value= "B">B</option>' +
            '<option value="C">C</option>' +
            '<option value="D">D</option>' +
            '<option value="E">E</option>' +
            '<option value="F">F</option>' +
            '</select></th>' +
            '<th><button class="fa fa-close" style="color:red; border:0;background-color: transparent;font-size: large;" onclick="removeResultRow()" id="removeButton' + counter + '" /></th>';

        var dropdown = document.getElementById("dropdownSubjects" + counter);
        addSubjectsToDropdown(dropdown);
        document.querySelector('#removeButton' + (counter - 1)).hide();
        if (counter > 1) {
            document.getElementById('addButton').style.display = 'none';
        }
    }
}

function removeResultRow() {
    var td = event.target.parentNode;
    var tr = td.parentNode;
    tr.parentNode.removeChild(tr);
    for (var i = 0; i < dictSubjectList.length; i++) {
        if (dictSubjectList[i].dropdownId == ("dropdownSubjects" + counter)) {
            dictSubjectList.splice(i, 1);
        }
    }
    counter--;
    if (counter > 0) {
        document.querySelector('#removeButton' + counter).show();
    }
    if (counter < 3) {
        document.getElementById('addButton').style.display = 'block';
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
                alert("Cannot select same subject");
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
    loadData("/Home/GetSubjects").then((response) => {
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
        .catch((error) => {
            toastr.error('Unable to make request!');
        });
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
    var dateOfBirth = document.querySelector('#DoB').value;
    var nationalId = document.querySelector('#Nid').value;
    var guardianId;
    var studentId;
    var guardianFirstName = document.querySelector('#guardianFirstName').value;
    var guardianLastName = document.querySelector('#guardianLastName').value;
    var guardianPhoneNumber = document.querySelector('#guardianPhoneNumber').value;
    var selectedSubjectList = [];
    var selectedSubjectResultList = [];
    console.log(firstName, lastName, phoneNumber, dateOfBirth, nationalId, guardianFirstName, guardianLastName, guardianPhoneNumber);

    for (var i = 0; i < dictSubjectList.length; i++) {
        var dropdownId = dictSubjectList[i].dropdownId;
        var subjectId = dictSubjectList[i].subjectId;
        var result = document.getElementById("Grade[" + dropdownId.charAt(dropdownId.length - 1) + "].Result").value;
        selectedSubjectList.push(subjectId);
        selectedSubjectResultList.push(result);
    }
    console.table(selectedSubjectList);
    console.table(selectedSubjectResultList);
    guardianDataObj = { FirstName: guardianFirstName, LastName: guardianLastName, PhoneNumber: guardianPhoneNumber };

    sendData(guardianDataObj, "/Home/CreateGuardian").then((response) => {
        if (response.result) {
            guardianId = response.guardianId;
            console.log(guardianId);
            studentDataObj = { FirstName: firstName, LastName: lastName, PhoneNumber: phoneNumber, DoB: dateOfBirth, Nid: nationalId, GuardianId: guardianId }
            sendData(studentDataObj, "/Home/CreateStudent").then((response) => {
                if (response.result) {
                    studentId = response.studentId;
                    console.log(studentId);
                    gradeDataObj = { StudentId: studentId, subjectId: selectedSubjectList, result: selectedSubjectResultList };
                    console.table(gradeDataObj);
                    sendData(gradeDataObj, "/Home/AddResults").then((response) => {
                        if (response.result) {
                            toastr.success("Registered sucessfully!");
                            window.location = "/Home/Index"
                        } else {
                            toastr.error("Unable to add results");
                            return false;
                        }
                    })
                        .catch((error) => {
                            console.log(error);
                        });

                } else {
                    toastr.error('Unable to create new Student');
                    return false;
                }
            })
                .catch((error) => {
                    console.log(error);
                });

        } else {
            toastr.error('Unable to create new Guardian');
            return false;
        }
    })
        .catch((error) => {
            console.log(error);
        });

}
