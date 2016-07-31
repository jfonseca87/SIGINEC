$(document).ready(function () {

    var url = "http://192.168.0.20/siginec";
    //var url = "http://localhost:49240/";

    $("#LogIn").click(function () {
        $("#modal1").modal();
        $("#content").load(url + "/Home/Ingresar");
    });

    $("#nuevoDispositivo").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/insertDispositivo");
    });

    $("#nuevaSolicitud").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/nuevaSolicitud");
    });

    $(".detalles").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/verDispositivo/" + $(this).data("id"));
    });

    $(".edita").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/insertDispositivo/" + $(this).data("id"));
    });

    $(".agregar").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/sumarCantidades/" + $(this).data("id"));
    });

    $("#LogOut").click(function () {
        location.href = url + "/home/Salir";
    });

    $("#Entrar").click(function () {

        var datos = $("#ingForm").serialize();

        $.ajax({
            url: url + '/home/ingresar',
            data: datos,
            type: "POST",
            datatype: "json",
            success: function (data) {
                if (data.IdUsuario != 0) {
                    location.href = url + "/home/indexaf";
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

        $("#dispositivo-list").load(url + "/Dispositivo/DispositivoList/" + page);
    });

    $(".page-solicitud").click(function () {
        
        var page = parseInt($(this).html());

        $("#solicitud-list").load(url + "/Dispositivo/SolDispositivoList/" + page);
    });

    $(".page-detalle").click(function () {
        
        var page = parseInt($(this).html());
        
        $("#detalle-list").load(url + "/Dispositivo/SeguimientoList?id="+ $("#detalle-list").data("id") +"&currentpage=" + page );
        
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
        location.href = url + "/Dispositivo/seguimientoSolicitud/" + $(this).data("id");
    });

    $(".cerrarSolicitud").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/cerrarSolicitud/" + $(this).data("id"));
    });

    $("#nuevoSeguimiento").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/nuevoSeguimiento/" + $(this).data("id"));
    });

    $("#Cantidad").blur(function () {

        var cantidad = $(this).val();
        var dispositivo = $("#Id_Dispositivo").val();
        var mensaje = "";

        $.ajax({
            url: url + "/Dispositivo/RetornaCantidad",
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

    $("#nuevaSolicitudBajoStock").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/nuevaSolBajoStock");
    });

    $("#selDispositivos").click(function () {

        var id = $("#Id_Dispositivo").val();
        var dispositivo = $("#Id_Dispositivo option:selected").html();
        var cantidad = $("#CantidadDisp").val();

        if (id != "" && cantidad != "")
        {
            $.ajax({
                url: url + "/Dispositivo/FillTableSolBajoStock",
                data:
                {
                    Id_Dispositivo: id,
                    Dispositivo: dispositivo,
                    CantidadDisp: cantidad
                },
                method: "POST",
                datatype: "json",
                success: function (data) {
                    $("#tDispositivos").empty();
                    if (id != 0 && cantidad != 0) {
                        $.each(data, function (i, item) {
                            $("#tDispositivos").append(
                                "<tr>" +
                                "<td>" + item.Id_Dispositivo + "</td>" +
                                "<td>" + item.Dispositivo + "</td>" +
                                "<td>" + item.CantidadDisp + "</td>" +
                                "</tr>"
                            );
                        });

                        $("#Id_Dispositivo").prop("selectedIndex", 0);
                        $("#CantidadDisp").val("");
                        $("#NuevaBS").removeClass("disabled");
                    }
                },
                fail: function (mensaje) {
                    alert("Ha ocurrido un error gravisimo!!!")
                }
            });
        }
    });

    $(".page-solicitudBS").click(function () {

        var page = parseInt($(this).html());

        $("#solicitud-list").load(url + "/Dispositivo/SolBajoStockList/" + page);
    });

    $(".seguimientoBS").click(function () {
        location.href = url + "/Dispositivo/seguimientoSolicitudBS/" + $(this).data("id");
    });

    $("#nSeguimientoBS").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/crearSeguimientoBS/" + $(this).data("id"));
    });

    $(".page-detalleBS").click(function () {

        var page = parseInt($(this).html());

        $("#detalle-list").load(url + "/Dispositivo/SeguimientoBSList?id=" + $("#detalle-list").data("id") + "&currentpage=" + page);
    });

    $(".cerrarSolicitudBS").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Dispositivo/cerrarSolBS/" + $(this).data("id"));
    });

    $("#nuevaBitacora").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Bitacora/nuevaBitacora");
    });

    $("#adjunto").change(function () {

        $.ajax({
            url: url + "/Bitacora/listAdjuntos",
            data:
            {
                Fotografia : $(this).val()
            },
            method: "POST",
            datatype: "json",
            success: function (data) {
                $("#contenedor").empty();
                $.each(data, function (i, item) {
                    $("#contenedor").append(
                        '<li class="list-group-item list-group-item-info">'+ item.Fotografia +'<button type="button" id="elimina-item" class="close" data-text="'+ item.Fotografia +'"><span aria-hidden="true">&times;</span></button></li>'
                    );
                });
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });
    });
});