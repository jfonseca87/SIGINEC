$(document).ready(function () {

    $("#LogIn").click(function () {
        $("#modal1").modal();
        $("#content").load("/home/Ingresar");
    });

    $("#nuevoDispositivo").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/insertDispositivo");
    });

    $(".btn-success").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/verDispositivo/" + $(this).data("id"));
    });

    $(".btn-warning").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/insertDispositivo/" + $(this).data("id"));
    });

    $(".btn-danger").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/sumarCantidades/" + $(this).data("id"));
    });

    $("#LogOut").click(function () {
        location.href = "/home/Salir";
    });

    $("#Entrar").click(function () {

        var datos = $("#ingForm").serialize();

        $.ajax({
            url: "/home/Ingresar",
            data: datos,
            type: "POST",
            datatype: "json",
            success: function (data) {
                if (data.IdUsuario != 0) {
                    location.href = "/home/indexaf";
                } else
                {
                    $("#mensaje").show();
                }
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error contacte con el departamento de Sistemas!!!");
            }
        });
    });

    $(".page-number").click(function () {

        var page = parseInt($(this).html());

        $("#dispositivo-list").load("/Dispositivo/DispositivoList/" + page);
    });
});