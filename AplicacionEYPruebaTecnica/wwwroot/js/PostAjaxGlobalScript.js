const PostGlobal = (_modelo, _url, _modal) => {

    try {

        const idModal = "#" + _modal;

        Swal.fire({
            title: 'Procesando...',
            text: 'Por favor espera',
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading();
            }
        });

        $.ajax({
            url :_url,
            type : "POST",
            contentType : "application/json",
            data: JSON.stringify(_modelo),
            success: (result) => {
                $(idModal).modal("hide");
                Swal.close();
                window.location.reload();
            }, error: (Merror) => {

                $(idModal).modal("hide");
                Swal.close();

                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: Merror
                });
            }
        });

    } catch (error) {
        console.log("Error en PostGlobal: "+error);
    }
};