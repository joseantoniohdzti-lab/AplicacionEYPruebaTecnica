var dataTable;

const loadDataTable = () => {

    if ($.fn.DataTable.isDataTable('#TblUsuario')) {
        $('#TblUsuario').DataTable().destroy();
    }

    dataTable = $('#TblUsuario').DataTable({

        "ajax": {
            "url": _urlGeneral + "/Admin/Usuarios/GetAll",
            "type": "GET",
            "datatype": "json"
        },

        "language": dataTablesLanguage,

        "columns": [
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center" style="display:flex;justify-content:center;gap:5px;">
                            
                            <a class="btn btn-primary btn-circle btn-sm shadow-sm"
                               title="Editar"
                               onclick="ObtenerDatosModalUsuario(${data})">
                               <i class="fas fa-edit"></i>
                            </a>

                            <a onclick="Delete('${_urlGeneral}/Admin/Usuarios/Delete/${data}','El usuario del sistema')"
                               title="Eliminar"
                               class="btn btn-danger btn-circle btn-sm shadow-sm">
                               <i class="fas fa-trash"></i>
                            </a>

                            <a class="btn btn-warning btn-circle btn-sm shadow-sm"
                               title="Cambiar Contraseña"
                               onclick="ModalPassword(${data})">
                               <i class="fas fa-key"></i>
                            </a>

                        </div>
                    `;
                },
                "width": "120px"
            },

            { "data": "nombreCompleto" },

            { "data": "userName" },

            { "data": "correo" },

            {
                "data": "estatus",
                "render": function (data) {
                    return data
                        ? '<span class="badge bg-success">SI</span>'
                        : '<span class="badge bg-danger">NO</span>';
                },
                "width": "80px",
                "className": "text-center"
            }
        ],

        "drawCallback": function () {

            $('.btn-circle').tooltip();

        }

    });

};




