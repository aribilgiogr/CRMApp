// Formatting function for row details - modify as you need
async function format(id) {
    const url = `/activities/lead/${id}`
    const res = await fetch(url, { method: 'POST', headers: { "content-type": 'application/json' } })
    let t = `
    <table class="table text-dark bg-white table-bordered table-striped">
        <tr>
            <th>Type</th>
            <th>Subject</th>
            <th>Description</th>
            <th>Due Date</th>
            <th>Completed Date</th>
            <th>Is Completed</th>
            <th>Sales Person</th>
        </tr>`

    if (res.ok) {
        const result = await res.json()
        if (result.success && result.data.length > 0) {
            result.data.forEach(item => {
                t += `
                <tr>
                    <td>${item["type"]}</td>
                    <td>${item["subject"]}</td>
                    <td>${item["description"]}</td>
                    <td>${item["dueDate"]}</td>
                    <td>${item["completedDate"]}</td>
                    <td>${item["isCompleted"]}</td>
                    <td>${item["assignedSalesPersonName"]}</td>
                </tr>`
            })
        } else {
            return ''
        }
    }

    t += `</table>`

    return t
}

let table = new DataTable('#leads', {
    columns: [
        {
            className: 'dt-control',
            orderable: false,
            data: null,
            defaultContent: ''
        },
        { data: 'Id' },
        { data: 'FullName' },
        { data: 'CompanyName' },
        { data: 'Email' },
        { data: 'Phone' },
        { data: 'Source' },
        { data: 'Status' },
        { data: 'Actions' }
    ],
    order: [[7, 'asc']]
});

// Add event listener for opening and closing details
table.on('click', 'tbody td.dt-control', function (e) {
    let tr = e.target.closest('tr');
    let row = table.row(tr);

    if (row.child.isShown()) {
        // This row is already open - close it
        row.child("").hide();
    }
    else {
        // Open this row
        format(row.data().Id)
            .then(t => {
                if (t !== '') {
                    row.child(t).show()
                } else {
                    row.child("").hide();
                }
            })
    }
});