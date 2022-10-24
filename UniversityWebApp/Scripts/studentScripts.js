var counter = 0
var subjectList;
var dictSubjectList = [];

$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

function addResultRow() {
    counter++;
    if (counter < 3) {
        var table = document.getElementById("AddItemsTable");
        var row = table.insertRow(-1);

        /*row.innerHTML = '<th><label for="dropdownSubjects" class="control-label col-md-2">Subject</label></th>' +
            '<th><select onchange="checkValue()" id="dropdownSubjects'+ counter +'" name="dropdownSubjects'+ counter +'" class="form-control"></select></th>' +
            '<th><label for="Grade[' + counter + '].Result" class="control-label col-md-2">Result</label></th>' +
            '<th><input type="text" id="Grade[' + counter + '].Result" "name="Grade[' + counter + '].Result"/></th>' +
            '<th><button class="fa fa-close" style="color:red; border:0;background-color: transparent;font-size: large;" onclick="removeResultRow()" id="removeButton' + counter + '" /></th>';*/

        row.innerHTML = '<th><label for="dropdownSubjects" class="control-label col-md-2">Subject</label></th>' +
            '<th><select required onchange="checkValue()" id="dropdownSubjects' + counter + '" name="dropdownSubjects' + counter + '" class="form-control"></select></th>' +
            '<th><label for="Grade[' + counter + '].Result" class="control-label col-md-2">Result</label></th>' +
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
        $('#removeButton' + (counter - 1)).hide();
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
        $('#removeButton' + counter).show();
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

function loading() {
    loadDropdown("/Home/GetSubjects").then((response) => {
        if (response.result) {
            toastr.success("Loaded");
        } else {
            toastr.error("Unable to load");
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!');
        });
}

function loadDropdown(url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                subjectList = data['subjectList'];
                var dropdown = document.getElementById("dropdownSubjects0");
                addSubjectsToDropdown(dropdown);
            },
            error: function (error) {
                reject(error)
            }
        })
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
    var firstName = $('#FirstName').val();
    var lastName = $('#LastName').val();
    var phoneNumber = $('#PhoneNumber').val();
    var dateOfBirth = $('#DoB').val();
    var nationalId = $('#Nid').val();
    var guardianId;
    var studentId;
    var guardianFirstName = $('#guardianFirstName').val();
    var guardianLastName = $('#guardianLastName').val();
    var guardianPhoneNumber = $('#guardianPhoneNumber').val();
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

function sendData(Data, url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: url,
            data: Data,
            dataType: "json",
            success: function (data) {
                resolve(data)
            },
            error: function (error) {
                reject(error)
            }
        })
    });
}