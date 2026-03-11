let Delete = (url, titulo) => {


    swal.fire({
        title: `¿Está seguro de eliminar`,
        text: `${titulo}`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, procesar eliminación',
        cancelButtonText: 'Cancelar'

    }).then((borra) => {

        if (borra.isConfirmed) {
            $.ajax({
                type: "POST",
                url: url,
                success: (data) => {
                    if (data.success) {
                        window.location.reload();
                    }
                }
            });
        }
    });
};
