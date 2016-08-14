$(document).ready(function () {

    //var url = "http://192.168.0.20/siginec";
    var url = "http://localhost:49240/";
    var guardaBitacora = 0;

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

        $("#UploadAjax").submit();
        
        $.ajax({
            url: url + "/Bitacora/listarAdjuntos",
            method: "POST",
            datatype: "json",
            success: function (data) {
                $("#contenedor").empty();
                $.each(data, function (i, item) {
                    $("#contenedor").append(
                        '<li type="button" class="list-group-item list-group-item-info">' + item.NomArchivo +' ('+ item.PesoArchivo +' KB) <button type="button" class="close" data-text="'+ item.NomArchivo +'"><span aria-hidden="true">&times;</span></button></li>'
                    );
                });
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });
    });

    $("#contenedor").on("click", "button", function () {
        alert("Vas a borrar algo... be careful...");

        $.ajax({
            url: url + "/bitacora/eliminaregistro",
            data: { adjunto: $(this).data("text") },
            method: "POST",
            datatype: "json",
            success: function (data) {
                $("#contenedor").empty();
                $.each(data, function (i, item) {
                    $("#contenedor").append(
                        '<li type="button" class="list-group-item list-group-item-info">' + item.NomArchivo + ' (' + item.PesoArchivo + ' KB) <button type="button" class="close" data-text="' + item.NomArchivo + '"><span aria-hidden="true">&times;</span></button></li>'
                    );
                });
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });
    });

    $("#guardarNueva").click(function () {

        guardaBitacora = 1;

        $("#UploadAjax").submit();

    });

    $("#UploadAjax").on("submit", function (e) {

        if (guardaBitacora == 1) {

            var Datos = $("#UploadAjax").serialize();

            $.ajax({
                url: url + "/bitacora/nuevaBitacora",
                data: Datos,
                method: "POST"
            });

        }
        else
        {
            e.preventDefault();

            var formdata = new FormData(document.getElementById("UploadAjax"));

            $.ajax({
                url: url + "/bitacora/listAdjuntos",
                type: "POST",
                datatype: "html",
                data: formdata,
                cache: false,
                contentType: false,
                processData: false
            })
        }
    });

    $(".page-bitacora").click(function () {

        var page = parseInt($(this).html());

        $("#bitacora-list").load(url + "/Bitacora/BitacoraList/" + page);
    });

    $(".detBitacora").click(function () {
        location.href = url + "/bitacora/detalleBitacora/" + $(this).data("id");
    });

    $("#verImagenes").click(function () {
        
        var cont = 0;
        $("#modal2").modal("show");
        $("#content1").load(url + "/Bitacora/traeImagenes/" + $(this).data("id"));

        $.ajax({
            url: url + "/Bitacora/conImagenes/" + $(this).data("id"),
            type: "POST",
            datatype: "json",
            success: function (data) {
                $.each(data, function (i, item) {
                    var ruta = url + "/BitacoraImagenes/" + item.Id_Bitacora + "/" + item.Fotografia;
                    if (cont == 0) {
                        $(".carousel-inner").append(
                            '<div class="item active"><img src="' + ruta + '" width="600px" /></div>'
                         );
                    } else
                    {
                        $(".carousel-inner").append(
                            '<div class="item"><img src="' + ruta + '" width="600px" /></div>'
                         );
                    }
                    cont += 1;
                });
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });
    });
});