/// <reference path="Logic.js" />
$(document).ready(function () {

    //var url = "http://192.168.0.20/siginec";
    var url = "http://localhost:49240";
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
                if (data != "") {
                    $.each(data, function (i, item) {
                        var ruta = url + "/BitacoraImagenes/" + item.Id_Bitacora + "/" + item.Fotografia;
                        if (cont == 0) {
                            $(".carousel-inner").append(
                                '<div class="item active"><img src="' + ruta + '" width="600px" /></div>'
                             );
                        } else {
                            $(".carousel-inner").append(
                                '<div class="item"><img src="' + ruta + '" width="600px" /></div>'
                             );
                        }
                        cont += 1;
                    });
                } else
                {
                    $(".contImagenes").empty();
                    $(".contImagenes").append(
                        '<h4>No se cargaron imágenes para la bitacora No. <b>' + $("#verImagenes").data("id") + '</b></h4>'
                    );
                    $(".imagenFooter").append(
                        '<button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>'
                    );
                }

            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });
    });

    $("#nuevaPersona").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Persona/nuevaPersona");
    });

    $(".page-persona").click(function () {
        var page = parseInt($(this).html());

        $("#persona-list").load(url + "/Persona/PersonasList/" + page);
    });

    $(".page-usuario").click(function () {
        var page = parseInt($(this).html());

        $("#usuario-list").load(url + "/Usuario/UsuarioList/" + page);
    });

    $(".page-cliente").click(function () {
        var page = parseInt($(this).html());

        $("#cliente-list").load(url + "/Cliente/ClienteList/" + page);
    });

    $(".editaPersona").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Persona/editaPersona/" + $(this).data("id"));
    });

    $("#nuevoUsuario").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Usuario/nuevoUsuario");
    });

    $("#Numero_Documento").blur(function () {
        var documento = $(this).val();
        
        $.ajax({
            url: url + "/persona/conspersona",
            data: { documento: documento },
            type: "POST",
            datatype: "json",
            success: function (data) {
                if (data != "") {
                    $.each(data, function (i, item) {
                        $(".validacion-persona").empty();
                        $(".validacion-persona").append(
                            'Ya existe una persona con este número de documento ' + item.Numero_Documento
                        );
                        $("#Numero_Documento").val("");
                    });
                }
                else {
                    $(".validacion-persona").empty();
                }
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });

    });

    $("#Usuario").blur(function () {
        var usuario = $(this).val();

        $.ajax({
            url: url + "/usuario/consusuario",
            data: { user: usuario },
            type: "POST",
            datatype: "json",
            success: function (data) {
                if (data != "") {
                    $.each(data, function (i, item) {
                        $(".validacion-usuario").empty();
                        $(".validacion-usuario").append(
                            'Ya existe una persona con este usuario ' + item.Nick_usuario 
                        );
                        $("#Usuario").val("");
                    });
                }
                else
                {
                    $(".validacion-usuario").empty();
                }
            },
            fail: function (mensaje) {
                alert("Ha ocurrido un error gravisimo!!!")
            }
        });

    });

    $(".desUsuario").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Usuario/desactivaUsuario/"+ $(this).data("id"));
    });

    $("#nuevoCliente").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Cliente/nuevoCliente");
    });

    $(".desCliente").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Cliente/desactivaCliente/" + $(this).data("id"));
    });

    $("#respBodega").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Responsable/respBodega");
    });

    $("#respBajoStock").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Responsable/respBajoStock");
    });

    $(".cambiaPassword").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Usuario/cambiaPassword/" + $(this).data("id"));
    });

    $("#cambioPassword").click(function () {
        $("#modal2").modal("show");
        $("#content1").load(url + "/Home/cambioPassword/" + $(this).data("id"));
    });

    $("#btnInfSolDisp").click(function () {
       
        $("#table-content").empty();

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: url + '/Informes/traeInformeSolDisp',
            cache: false,
            success: function (chartsdata) {

                var data = new google.visualization.DataTable();

                data.addColumn('string', 'Mes');
                data.addColumn('number', 'Cant. Solicitudes de Dispositivos');

                for (var i = 0; i < chartsdata.length; i++) {
                    data.addRow([chartsdata[i].Mes, chartsdata[i].cantSolDisp]);

                    $("#table-content").append(
                        '<tr><td> ' + chartsdata[i].Mes + '</td><td style="text-align:center;">' + chartsdata[i].cantSolDisp + '</td></tr>'
                    );
                }

                // Instantiate and draw our chart, passing in some options    
                var chart = new google.visualization.PieChart(document.getElementById('grafico'));

                chart.draw(data,
                  {
                      title: "Informe Solicitud de Dispositivos por Mes",
                      position: "center",
                      fontsize: "48px",
                      chartArea: { width: '50%' },
                });

                $(".contenedor").css("visibility", "visible");

            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    });

    $("#btnInfSolBS").click(function () {

        $("#table-content").empty();

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: url + '/Informes/traeInformeSolBajoStock',
            cache: false,
            success: function (chartsdata) {

                var data = new google.visualization.DataTable();

                data.addColumn('string', 'Mes');
                data.addColumn('number', 'Cant. Solicitudes de Bajo Stock');

                for (var i = 0; i < chartsdata.length; i++) {
                    data.addRow([chartsdata[i].Mes, chartsdata[i].solBajoStock]);

                    $("#table-content").append(
                        '<tr><td> ' + chartsdata[i].Mes + '</td><td style="text-align:center;">' + chartsdata[i].solBajoStock + '</td></tr>'
                    );
                }

                // Instantiate and draw our chart, passing in some options    
                var chart = new google.visualization.PieChart(document.getElementById('grafico'));

                chart.draw(data,
                  {
                      title: "Informe Solicitud de Bajo Stock por Mes",
                      position: "center",
                      fontsize: "48px",
                      chartArea: { width: '50%' },
                  });

                $(".contenedor").css("visibility", "visible");

            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    });

    $("#btnInfBitacora").click(function () {

        $("#table-content").empty();

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: url + '/Informes/traeInformeBitacoras',
            cache: false,
            success: function (chartsdata) {

                var data = new google.visualization.DataTable();

                data.addColumn('string', 'Mes');
                data.addColumn('number', 'Cant. Bitacoras');

                for (var i = 0; i < chartsdata.length; i++) {
                    data.addRow([chartsdata[i].Mes, chartsdata[i].cantBitacoras]);

                    $("#table-content").append(
                        '<tr><td> ' + chartsdata[i].Mes + '</td><td style="text-align:center;">' + chartsdata[i].cantBitacoras + '</td></tr>'
                    );
                }

                // Instantiate and draw our chart, passing in some options    
                var chart = new google.visualization.PieChart(document.getElementById('grafico'));

                chart.draw(data,
                  {
                      title: "Informe Bitacoras por Mes",
                      position: "center",
                      fontsize: "48px",
                      chartArea: { width: '50%' },
                  });

                $(".contenedor").css("visibility", "visible");

            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    });
});