function Enviar()
{
    var esValido = $("#frmEditarEmpresa").valid();

    if (!esValido)
    {
        return;
    }
    $("#frmEditarEmpresa").submit();
}

function frmEditarEmpresa_Begin(obj)
{
    console.log("Enviando");
    console.log(obj);
}

function frmEditarEmpresa_Failure(obj)
{
    console.log("Fallo");
    console.log(obj);
}

function frmEditarEmpresa_Success(obj)
{
    console.log("hola");
    //window.location.href = '/Empresa';
}