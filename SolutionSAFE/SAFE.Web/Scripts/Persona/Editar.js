function Enviar()
{
    var esValido = $("#frmEditarPersona").valid();

    if (!esValido)
    {
        return;
    }
    $("#frmEditarPersona").submit();
}

function frmEditarPersona_Begin(obj)
{
    console.log("Enviando");
    console.log(obj);
}

function frmEditarPersona_Failure(obj)
{
    console.log("Fallo");
    console.log(obj);
}

function frmEditarPersona_Success(obj)
{
    if (obj.codigo == 1)
    {
        window.location.href = '/Persona';
    }
    else
    {
        console.log(obj.mensaje);
    }
}