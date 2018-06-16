$(document).ready(function ()
{
    GetAll();
});


function Desactivar(id)
{
    $.ajax(
    {
        url: "/Empresa/Desactivar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.codigo == 1) {
                window.location.href = '/Empresa';
            }
            else {
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
    $.ajax({
        url: "/Empresa/Activar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.codigo == 1) {
                window.location.href = '/Empresa';
            }
            else {
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
    $('#ListaEmpresas tbody').html("");
    $.ajax(
    {
        url: "/Empresa/Listar",
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $.each(response, function (key, value) {
                
                var lista = 
                    "<tr><td>" + value.id +
                    "</td><td>" + value.nombre +
                    "</td><td>" + value.telefono +
                    "</td><td>" + value.direccion +
                    "</td><td>" +
                    "<a href='/Empresa/Editar/" + value.id + "' class='btn-sm btn-primary'>EDITAR</a>" +
                    "</td><td>";

                if (value.estado == 1) {
                    lista = lista + "<button id='btnDesactivar' onclick='Desactivar(" + value.id + ")' class='btn-sm btn-danger'>DESACTIVAR</button></div></td></tr>'";
                }
                else if (value.estado == 0) {
                    lista = lista + "<button id='btnActivar' onclick='Activar(" + value.id + ")' class='btn-sm btn-success'>ACTIVAR</button></div></td></tr>'";
                }
                $('#ListaEmpresas tbody').append(lista);
            });
        },
        error: function (response) {
            alert("Do!");
        }
    });
}