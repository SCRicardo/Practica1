let datatable;
$(document).ready(function () {
    loadDataTable();  //Sirve para leer la tabla
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        //Seccion de ajax para el´pluggin
        "ajax": { "url": "/Admin/Departamento/obtenerTodos" },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "40%" },
            { "data": "jefe", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {  //Alt + 96 para estos simbolos
                    return ` 
                        <div class="text-center">
                            <a href="/Admin/Departamento/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/Departamento/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="bi bi-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json'
        }
    });
}

function Delete(url) {
    swal({
        title: "¿Estás seguro de eliminar la Departamento?",
        text: "Este registro no será recuperado",
        icon: "warning",
        buttons: true,
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "Post",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    });
}