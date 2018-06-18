function Enviar() {
    var esValido = $("#frmEditarPerfil").valid();

    if (!esValido) {
        return;
    }
    $("#frmEditarPerfil").submit();
}

function frmEditarPerfil_Begin(obj) {

    console.log("Enviando");
    console.log(obj);
}

function frmEditarPerfil_Failure(obj) {
    console.log("Fallo");
    console.log(obj);
}

function frmEditarPerfil_Success(obj) {
    window.location.href = '/Perfil/Index';
}