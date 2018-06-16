$(document).ready(function () {
    GetAll();
});


function Desactivar(id) {
    $.ajax(
        {
            url: "/Perfil/Desactivar?id=" + id,
            type: "GET",
            contentType: "aplication/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                if (response.codigo == 1) {
                    window.location.href = '/Perfil';
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



function Activar(id) {
    $.ajax({
        url: "/Perfil/Activar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.codigo == 1) {
                window.location.href = '/Perfil';
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



function GetAll() {
    $('#ListaPerfiles tbody').html("");
    $.ajax(
        {
            url: "/Perfil/Listar",
            type: "GET",
            contentType: "aplication/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                $.each(response, function (key, value) {
                    
                    var lista = "<tr><td>" + value.id +
                        "</td><td>" + value.perfil +
                        "</td><td>" + value.descripcion +
                        "</td><td>" +
                        "<a href='/Perfil/Editar/" + value.id + "' class='btn-sm btn-primary'>EDITAR</a>" +
                        "</td><td>";

                    if (value.estado == 1) {
                        lista = lista + "<button id='btnDesactivar' onclick='Desactivar(" + value.id + ")' class='btn-sm btn-danger'>DESACTIVAR</button></div></td></tr>'";
                    }
                    else if (value.estado == 0) {
                        lista = lista + "<button id='btnActivar' onclick='Activar(" + value.id + ")' class='btn-sm btn-success'>ACTIVAR</button></div></td></tr>'";
                    }
                    $('#ListaPerfiles tbody').append(lista);
                });
            },
            error: function (response) {
                alert("Do!");
            }
        });
}