$(document).ready(function () {

    $("#LogIn").click(function () {
        $("#modal1").modal();
        $("#content").load("/home/Ingresar");
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
});