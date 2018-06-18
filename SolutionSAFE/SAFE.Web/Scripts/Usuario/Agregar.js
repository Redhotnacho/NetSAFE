function Enviar()
{
    var esValido = $("#frmAgregarUsuario").valid();

    if (!esValido)
    {
        return;
    }
    $("#frmAgregarUsuario").submit();
}

function AgregarUsuario_Begin(obj)
{
    console.log(obj);
}

function AgregarUsuario_Failure(obj)
{
    console.log("Error en la peticion de Agregar");
    console.log(obj);
}

function AgregarUsuario_Success(obj)
{
    if (obj.codigo == 1)
    {
        window.location.href = '/Usuario/Index';
    }
    else
    {
        console.log(obj.mensaje);
    }
}
