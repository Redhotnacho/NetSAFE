function Enviar()
{
    var esValido = $("#frmAgregarPersona").valid();

    if (!esValido)
    {
        return;
    }
    $("#frmAgregarPersona").submit();
}

function AgregarPersona_Begin(obj)
{
    console.log(obj);
}

function AgregarPersona_Failure(obj)
{
    console.log("Error en la peticion de Agregar");
    console.log(obj);
}

function AgregarPersona_Success(obj)
{
    if (obj.codigo == 1)
    {
        window.location.href = '/Persona/Index';
    }
    else
    {
        console.log(obj.mensaje);
    }
}
