//$(document).ready(function(){
//    loadDataTable();
//});

//function loadDataTable()
//{
//    dataTable = $('#tblData').DataTable({
//        "ajax": { url: '/admin/product/getall'},
//          "columns": [
//            {data :'title', "width": "15%"},
//            { data: 'isbn', "width": "15%"},
//            { data: 'listPrice', "width": "15%"},
//            { data: 'author', "width": "15%" },
//            { data: 'category.name', "width": "15%" },
//    ]
//    });
//}
var js = jQuery.noConflict(true);
js(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = js('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },            
        ]
    });
}

