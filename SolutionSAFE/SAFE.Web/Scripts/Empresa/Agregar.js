function Enviar()
{
    var esValido = $("#frmAgregarEmpresa").valid();

    if (!esValido)
    {
        return;
    }
    $("#frmAgregarEmpresa").submit();
}

function AgregarEmpresa_Begin(obj)
{
    console.log(obj);
}

function AgregarEmpresa_Failure(obj)
{
    console.log("Error en la peticion de Agregar");
    console.log(obj);
}

function AgregarEmpresa_Success(obj)
{
    if (obj.codigo == 1)
    {
        window.location.href = '/Empresa';
    }
    else
    {
        console.log(obj.mensaje);
    }
}