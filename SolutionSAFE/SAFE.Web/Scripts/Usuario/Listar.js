$(document).ready(function ()
{
    GetAll();
});



function Desactivar(id)
{
    
    $.ajax(
    {
        url: "/Usuario/Desactivar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.codigo == 1) {
                window.location.href = '/Usuario';
            } else {
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
        url: "/Usuario/Activar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.codigo == 1) {
                window.location.href = '/Usuario';
            } else {
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
    $('#ListaUsuarios tbody').html("");
    $.ajax(
    {
        url: "/Usuario/Listar",
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $.each(response, function (key, value)
            {
                var lista = "<tr><td>" + value.id +
                    "</td><td>" + value.username +
                    "</td><td>" + value.id_persona +
                    "</td><td>" + value.perfil +
                    "</td><td>" + value.empresa +
                    "</td><td>" +
                    "<a href='/Usuario/Editar/" + value.id + "' class='btn-sm btn-primary'>EDITAR</a>" +
                    "</td><td>";

                if (value.estado == 1)
                {
                    lista = lista + "<button id='btnDesactivar' onclick='Desactivar("+ value.id + ")' class='btn-sm btn-danger'>DESACTIVAR</button></div></td></tr>'";
                }
                else if (value.estado == 0)
                {
                    lista = lista + "<button id='btnActivar' onclick='Activar("+ value.id + ")' class='btn-sm btn-success'>ACTIVAR</button></div></td></tr>'";                    
                }
                $('#ListaUsuarios tbody').append(lista);
            });
        },
        error: function (response)
        {
            alert("Do!");
        }
    });

    
}