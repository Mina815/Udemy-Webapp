var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { "url": "/Admin/Order/GetAll" },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            {"data":"orderStatus", "width":"15%"},
            {"data":"orderTotal", "width":"15%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Order/Details?OrderId=${data}" class="btn btn-primary btn-sm px-4 mx-3"> <i class="bi bi-pencil-square"></i>  Details</a>
                        </div>
                    `
                },
                 "width": "15%"
            },
        ]
    });
}

