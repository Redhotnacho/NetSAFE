function Enviar() {
    var esValido = $("#frmAgregarPerfil").valid();

    if (!esValido) {
        return;
    }
    $("#frmAgregarPerfil").submit();
}

function AgregarPerfil_Begin(obj) {
    console.log(obj);
}

function AgregarPerfil_Failure(obj) {
    console.log("Error en la peticion de Agregar");
    console.log(obj);
}

function AgregarPerfil_Success(obj) {
    if (obj.codigo == 1) {
        window.location.href = '/Perfil/Index';
    }
    else {
        console.log(obj.mensaje);
    }
}