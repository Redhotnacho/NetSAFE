function desactivarb(id) {
    window.location.replace("/Views/Evaluaciones/index.aspx?desactivar=" + id);
}

function activarb(id) {

    document.location.href = "/Views/Evaluaciones/index.aspx?activar=" + id;
    

function Desactivar(id) {
    $.ajax(
        {
            url: "/Evaluaciones/Desactivar?id=" + id,
            type: "GET",
            contentType: "aplication/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                if (response.codigo === 1) {
                    window.location.href = '/Evaluaciones';
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
        url: "/Evaluaciones/Activar?id=" + id,
        type: "GET",
        contentType: "aplication/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.codigo === 1) {
                window.location.href = '/Evaluaciones';
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
    $('#ListaEvaluaciones tbody').html("");
    $.ajax(
        {
            url: "/Evaluaciones/Listar",
            type: "GET",
            contentType: "aplication/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                $.each(response, function (key, value) {

                    var lista =
                        "<tr><td>" + value.id +
                        "</td><td>" + value.fecha +
                        "</td><td>" + value.estado +
                        "</td><td>" + value.nombre +
                        "</td><td>" +
                        "<a href='/Evaluaciones/Editar/" + value.id + "' class='btn-sm btn-primary'>EDITAR</a>" +
                        "</td><td>";

                  

                    if (value.estado === 1) {
                        lista = lista + "<button id='btnDesactivar' onclick='Desactivar(" + value.id + ")' class='btn-sm btn-danger'>DESACTIVAR</button></div></td></tr>'";
                    }
                    else if (value.estado === 0) {
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