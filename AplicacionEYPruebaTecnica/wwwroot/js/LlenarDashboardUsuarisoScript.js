const llenarDashboard = async () => {
    try {

        const url = "llenarDashboard";

        const result = await $.get(url);

        if (result.data != null) {

            $("#txtTotal").text(result.data.total);
            $("#idUsuariHabilitado").text(result.data.activos);
            $("#idUsuariInHabilitado").text(result.data.inActivos);
        }

    } catch (error) {
        console.log("Error en llenarDashboard: "+error);
    }
};


$(document).ready(function () {
    llenarDashboard();
});