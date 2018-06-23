function Enviar()
{
    var esValido = $("#frmEditarUsuario").valid();

    if (!esValido)
    {
        return;
    }
    $("#frmEditarUsuario").submit();
}

function frmEditarUsuario_Begin(obj)
{

    console.log("Enviando");
    console.log(obj);
}

function frmEditarUsuario_Failure(obj)
{
    console.log("Fallo");
    console.log(obj);
}

function frmEditarUsuario_Success(obj)
{
    window.location.href = '/Usuario';
}