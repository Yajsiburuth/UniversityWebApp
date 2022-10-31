var studentSummaryList;
function loadEmail() {
    localStorage.clear();
    var serverCall = new ServerCall({ url: "/User/GetUser", callMethod: "GET" });
    serverCall.fetchApiCall().then(response => {
        if (response.user != null) {
            var navTitle = document.getElementById("navTitle");
            console.log(navTitle);
            var userEmail = response['user']['Email'];
            navTitle.innerText = userEmail;
        } else {
            toastr.error("Unable to load");
        }
    })
}

function logout() {
    window.location.href = "/User/Login";
}

function DisplaySummary() {
    let dataCache = JSON.parse(localStorage.getItem('summaryData'));
    if (dataCache === null) {
        var serverCallSummary = new ServerCall({ url: "/Student/GetStudentsSummary", callMethod: "GET" });
        serverCallSummary.fetchApiCall().then(response => {
            studentSummaryList = response.studentsSummary;
            localStorage.setItem('summaryData', JSON.stringify(dataCache));
            AddDataToTable(studentSummaryList);
        });
    } else {
        AddDataToTable(dataCache);
    }
}

function AddDataToTable(studentSummaryList) {
    var tableBody = document.getElementById("studentSummaryTable").getElementsByTagName('tbody')[0];
    tableBody.innerHTML = "";
    studentSummaryList.forEach((summary, index) => {
        var row = tableBody.insertRow();
        row.id = summary.StudentId;
        row.innerHTML = '<td>' + `${summary.StudentId}` + '</td>'
            + '<td>' + `${summary.FirstName}` + '</td>'
            + '<td>' + `${summary.LastName}` + '</td>'
            + '<td>' + `${summary.PhoneNumber}` + '</td>'
            + '<td>' + `${new Date(parseInt(summary.DateOfBirth.replace(/[^0-9 +]/g, ''))).toLocaleDateString()}` + '</td>'
            + '<td>' + `${summary.GuardianName}` + '</td>'
            + '<td>' + `${summary.NationalId}` + '</td>'
            + '<td>' + `${summary.UserId}` + '</td>'
            + '<td>' + `${summary.SubjectsTaken}` + '</td>'
            + '<td>' + `${summary.TotalResult}` + '</td>'
            + '<td>' + `${summary.Status}` + '</td>'
    });
}
const topStudentIds = (studentSummaryList, n) => studentSummaryList.map(x => x[n]);
function approveTopFifteen() {
/*    var ids = studentSummaryList.reduce((ids, summary) => {
        if (summary.TotalResult >= 10) {
            ids.push(summary.StudentId);
        }
        return ids;
    }, []);
    console.log(ids);*/
    let studentIds = studentSummaryList.map(row => row.StudentId);
    //studentIds = studentIds.slice(0, 15);
    //var studentIdList = topStudentIds(studentSummaryList, 1);
    var serverCall = new ServerCall({ url: "/Student/ApproveStudents", parameters: studentIds, callMethod: "POST" });
    serverCall.fetchApiCall().then(response => {

    })
}