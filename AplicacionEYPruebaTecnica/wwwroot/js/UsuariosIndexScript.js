const idAgregar = document.getElementById("idAgregar");
const idModalUsuarios = document.getElementById("idModalUsuarios");
const idModalPassWord = document.getElementById("idModalPassWord");
const idGuardar = document.getElementById("idGuardar");
const idNombre = document.getElementById("idNombre");
const idUserName = document.getElementById("idUserName");
const idPassword = document.getElementById("idPassword");
const idCorreo = document.getElementById("idCorreo");
const idEstatus = document.getElementById("idEstatus");
const Id = document.getElementById("Id");
const idEditar = document.getElementById("idEditar");
const IdPassword = document.getElementById("IdPassword");
const idGuardarPassword = document.getElementById("idGuardarPassword");
const idPasswordAnt = document.getElementById("idPasswordAnt");
const idPasswordNew = document.getElementById("idPasswordNew");

const mostrarModal = () => {
    const modal = new bootstrap.Modal(idModalUsuarios);
    modal.show();

    $('#idModalUsuarios').on('shown.bs.modal', function () {
        $.validator.unobtrusive.parse("#formUsuarios");
    });
};


const mostrarModalPassword = () => {
    const modal = new bootstrap.Modal(idModalPassWord);
    modal.show();

    $('#idModalPassWord').on('shown.bs.modal', function () {
        $.validator.unobtrusive.parse("#formPassWord");
    });
};

const UsuarioUpsert = () => {
    try {

        const url = "/Admin/Usuarios/UsuariosUpsert";

        let formulario = $("#idFormUsers");

        if (!formulario.valid()) {
            return;
        }


        const data = {

            Id: Id.value ? parseInt(Id.value) : 0,
            NombreCompleto: idNombre.value,
            UserName: idUserName.value,
            Password: idPassword.value,
            Correo: idCorreo.value,
            Estatus: idEstatus.checked

        };

        const modal = `idModalUsuarios`;

        PostGlobal(data, url, modal);

        

    } catch (error) {
        console.log("Error en UsuarioUpsert: " + error);
    }
};


const ObtenerDatosModalUsuario = async(id) => {

    try {
        const url = "obtenerUsuario";

        const data = {
            id : id
        };

        const idPrincipal = parseInt(id);

        if (idPrincipal != 0) {
            $("#SeccionAgregar").hide();
        } else {
            $("#SeccionAgregar").show();
        }

        const result = await $.get(url, data);

        //console.log(result.data);

        if (result.data != null) {

            idNombre.value = result.data.nombreCompleto; 
            idUserName.value = result.data.userName;
            idPassword.value = result.data.password;
            idCorreo.value = result.data.correo;
            idEstatus.checked = result.data.estatus;
            Id.value = result.data.id;

            mostrarModal();
        }


    } catch (error) {
        console.log("Error en ActualizarUsuario: " + error);
    }
};

//ModalPassword

const ModalPassword = async (id) => {

    try {

        IdPassword.value = id;
        mostrarModalPassword();

    } catch (error) {
        console.log("Error en ActualizarUsuario: " + error);
    }
};


//ActualizarPassWord

const ActualizarPassWord = () => {
    try {

        const url = "/Admin/Usuarios/ActualizarPassWord";

        let formulario = $("#idFormPassWord");

        if (!formulario.valid()) {
            return;
        }


        const data = {

            Id: IdPassword.value ? parseInt(IdPassword.value) : 0,
            Password: idPasswordNew.value,
            PasswordAnterior: idPasswordAnt.value,
        };

        const modal = `idModalPassWord`;

        PostGlobal(data, url, modal);



    } catch (error) {
        console.log("Error en ActualizarPassWord: " + error);
    }
};


//idGuardarPassword
idAgregar.addEventListener("click", mostrarModal);
idGuardar.addEventListener("click", UsuarioUpsert);
idGuardarPassword.addEventListener("click", ActualizarPassWord);