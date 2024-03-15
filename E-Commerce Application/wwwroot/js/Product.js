
$(document).ready(function () {
    loadDataTable();
});


// Note : Number of Columns here must be of same number as number of table headers in html 
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/product/getall'
        },
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn btn-group w-75" role="group">
                               <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i clas="bi bi-pencil-square"></i>Edit </a>
                               <a href="/admin/product/delete?id=${data}" class="btn btn-danger mx-2" > <i class="bi bi-thrash-fill"></i> Delete </a>
                            </div>`
                } ,  
                "width": "15%"
            }
        ]
    });
}

