﻿let datatable;
$(document).ready(function () {
    loadDataTable();  //Sirve para leer la tabla
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        //Seccion de ajax para el´pluggin
        "ajax": { "url": "/Admin/Curso/obtenerTodos" },
        "columns": [
            { "data": "nombre" },
            { "data": "descripcion" },
            { "data": "nivel" },
            { "data": "profesor.nombre" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                           <a href="/Admin/Curso/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                              <i class="bi bi-pencil-square"></i>  
                           </a>
                           <a onclick=Delete("/Admin/Curso/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash3-fill"></i>
                           </a> 
                        </div>
                    `;
                }, "width": "20%"
            }
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json',
            search: "Búsqueda:",
            paginate: {
                previous: "Antes",
                next: "Después",
            },
            info: "Mostrando resultados del 1 al 2 de 2 resultados",
            lengthMenu:"Ver resultados"      
        },
    });
}

function Delete(url) {
    swal({
        title: "¿Estás seguro de eliminar el Curso?",
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