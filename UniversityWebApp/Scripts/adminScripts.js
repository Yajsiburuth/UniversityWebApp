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
            + '<td>' + `${summary.Address}` + '</td>'
            + '<td>' + `${summary.PhoneNumber}` + '</td>'
            + '<td>' + `${new Date(parseInt(summary.DateOfBirth.replace(/[^0-9 +]/g, ''))).toLocaleDateString()}` + '</td>'
            + '<td>' + `${summary.GuardianName}` + '</td>'
            + '<td>' + `${summary.NationalId}` + '</td>'
            + '<td>' + `${summary.UserId}` + '</td>'
            + '<td>' + `${summary.SubjectsTaken}` + '</td>'
            + '<td>' + `${summary.TotalResult}` + '</td>'
            + '<td>' + `${summary.Status}` + '</td>'
/*
            + '<td> <select id="dropdownStatus"><option value="Rejected" <c:if test="${summary.Status == ' + '"Rejected"' + ' }"> selected  </c:if>>Rejected</option>'
            +'<option value="Waiting" <c: if test = "${summary.Status == ' + "Waiting" + '}"> selected  </c:if>' + '>Waiting</option>'
            +'<option value="Approved" <c: if test = "${summary.Status == ' + "Approved" + '}"> selected  </c:if>' + '>Approved</option>'

        +   '</td>'*/
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

function DownloadTableAsCSV(separator = ',') {
    var table_id = "studentSummaryTable";
    // Select rows from table_id
    var rows = document.querySelectorAll('table#' + table_id + ' tr');
    // Construct csv
    var csv = [];
    for (var i = 0; i < rows.length; i++) {
        var row = [], cols = rows[i].querySelectorAll('td, th');
        for (var j = 0; j < cols.length; j++) {
            // Clean innertext to remove multiple spaces and jumpline (break csv)
            var data = cols[j].innerText.replace(/(\r\n|\n|\r)/gm, '').replace(/(\s\s)/gm, ' ')
            // Escape double-quote with double-double-quote (see https://stackoverflow.com/questions/17808511/properly-escape-a-double-quote-in-csv)
            data = data.replace(/"/g, '""');
            // Push escaped string
            row.push('"' + data + '"');
        }
        csv.push(row.join(separator));
    }
    var csv_string = csv.join('\n');
    // Download it
    var filename = 'export_' + table_id + '_' + new Date().toLocaleDateString() + '.csv';
    var link = document.createElement('a');
    link.style.display = 'none';
    link.setAttribute('target', '_blank');
    link.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(csv_string));
    link.setAttribute('download', filename);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}