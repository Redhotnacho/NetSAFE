$(document).ready(function ()
{
    GetAll();
});


function Desactivar(id)
{
    $.ajax(
    {
        url: "/Persona/Desactivar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response)
        {
            if (response.codigo == 1)
            {
                window.location.href = '/Persona';
            }
            else
            {
                alert(response.mensaje);
            }
        },
        error: function () {
            alert("Do!");
        }
    });
}



function Activar(id)
{
    $.ajax(
    {
        url: "/Persona/Activar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response)
        {
            if (response.codigo == 1)
            {
                window.location.href = '/Persona';
            }
            else
            {
                alert(response.mensaje);
            }
        },
        error: function () {
            alert("Do!");
        }
    });
}



function GetAll()
{
    $('#ListaPersonas tbody').html("");
    $.ajax(
    {
        url: "/Persona/Listar",
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $.each(response, function (key, value)
            {
                value.fecha_nac = new Date(value.fecha_nac.match(/\d+/)[0] * 1);
                var lista = "<tr><td>" + value.id +
                    "</td><td>" + value.rut +
                    "</td><td>" + value.nombre +
                    "</td><td>" + value.ap_materno +
                    "</td><td>" + value.ap_paterno +
                    "</td><td>" + value.correo +
                    "</td><td>" + value.telefono +
                    "</td><td>" + value.fecha_nac.getDate() + '/' + (value.fecha_nac.getMonth() + 1) + '/' + value.fecha_nac.getFullYear() +
                    "</td><td>" +
                    "<a href='/Persona/Editar/" + value.id + "' class='btn-sm btn-primary'>EDITAR</a>" +
                    "</td><td>";

                if (value.estado == 1)
                {
                    lista = lista + "<button id='btnDesactivar' onclick='Desactivar(" + value.id + ")' class='btn-sm btn-danger'>DESACTIVAR</button></div></td></tr>'";
                }
                else if (value.estado == 0)
                {
                    lista = lista + "<button id='btnActivar' onclick='Activar(" + value.id + ")' class='btn-sm btn-success'>ACTIVAR</button></div></td></tr>'";
                }
                $('#ListaPersonas tbody').append(lista);
            });
        },
        error: function (response)
        {
            alert("Do!");
        }
    });
}