$(document).ready(function () {

    $("#LogIn").click(function () {
        $("#modal1").modal();
        $("#content").load("/home/Ingresar");
    });

    $("#nuevoDispositivo").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/insertDispositivo");
    });

    $("#nuevaSolicitud").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/nuevaSolicitud");
    });

    $(".detalles").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/verDispositivo/" + $(this).data("id"));
    });

    $(".edita").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/insertDispositivo/" + $(this).data("id"));
    });

    $(".agregar").click(function () {
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

    $(".page-dispositivo").click(function () {

        var page = parseInt($(this).html());

        $("#dispositivo-list").load("/Dispositivo/DispositivoList/" + page);
    });

    $(".page-solicitud").click(function () {
        
        var page = parseInt($(this).html());

        $("#solicitud-list").load("/Dispositivo/SolDispositivoList/" + page);
    });

    $(".page-detalle").click(function () {
        
        var page = parseInt($(this).html());
        
        $("#detalle-list").load("/Dispositivo/SeguimientoList?id="+ $("#detalle-list").data("id") +"&currentpage=" + page );
        
    });

    $("#Nueva").click(function () {

        var mensajes = "";
        var idDispositivo = $("#Id_Dispositivo").val();
        var cantidad = $("#Cantidad").val();
        var idCliente = $("#Id_Cliente").val();
        var observaciones = $("#Observaciones").val();

        if (idDispositivo == "") {
            mensajes = "• Debe seleccionar un dispositivo </br>";
        }
        if (cantidad == "")
        {
            mensajes += "• Debe ingresar una cantidad </br>";
        }
        if (idCliente == "")
        {
            mensajes += "• Debe asignar un cliente </br>";
        }
        if (observaciones == "") {
            mensajes += "• Debe ingresar al menos una observación";
        }

        if (mensajes != "") {
            $("#mensaje").html(mensajes);
            mensajes = "";
        }
        else
        {
            $("#solDispForm").submit();
        }
    });

    $(".seguimiento").click(function () {
        location.href = "/Dispositivo/seguimientoSolicitud/" + $(this).data("id");
    });

    $(".cerrarSolicitud").click(function () {
        $("#modal2").modal("show");
    });

    $("#nuevoSeguimiento").click(function () {
        $("#modal2").modal("show");
        $("#content1").load("/Dispositivo/nuevoSeguimiento/" + $(this).data("id"));
    });

    $("#Cantidad").blur(function () {

        var cantidad = $(this).val();
        var dispositivo = $("#Id_Dispositivo").val();
        var mensaje = "";

        $.ajax({
            url: "/Dispositivo/RetornaCantidad",
            data: { id : dispositivo },
            type: "POST",
            datatype: "json",
            success: function (data) {
                if (data.Cantidad < cantidad) {
                    $("#Nueva").attr("disabled", true);
                    mensaje = "La cantidad supera el stock en inventario " + data.Cantidad + " Unds.";
                    $("#mensaje").html(mensaje);
                } else
                {
                    $("#Nueva").attr("disabled", false);
                    mensaje = "";
                    $("#mensaje").html(mensaje);
                }
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error contacte con el departamento de Sistemas!!!");
            }
        });
    });
});