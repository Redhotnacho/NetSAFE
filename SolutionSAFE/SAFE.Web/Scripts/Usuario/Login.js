$(document).ready(function ()
{
    $('#btnLogin').click(function ()
    {
        var obj = new Object();
        obj.username = $('#login-name').val();
        obj.contrasena = $('#login-pass').val();

        if (obj.username == "" || obj.contraseña == "")
        {
            $("#mensaje").html('<label class="alert alert-danger">Debe ingresar Username y Contraseña </label >');
        }
        else
        {
            LogIn(obj);
        }
    });
});


function LogIn(obj)
{
    $.ajax({
        url: '/Login/LogIn',
        type: 'POST',
        data: obj,
        dataType: 'json',
        async: true,
        success: function (response)
        {
            if (response.codigo == 1)
            {
                window.location.href = response.vista;
            }
            else
            {
                $("#mensaje").html('<label class="alert alert-danger">' + response.mensaje + '</label >');
            }
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
            console.log(jqXHR, textStatus, errorThrown);
        }
    });
}