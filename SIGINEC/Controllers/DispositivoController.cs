using Model;
using Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class DispositivoController : Controller
    {
        #region Variables y Objetos
            static string mensaje = "";
            string asunto = "";
            string cuerpo = "";
            string mailResp = "";
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Dispositivo dispositivo = new Dispositivo();
            Ingreso_Dispositivo inDisp = new Ingreso_Dispositivo();
            Solicitud_Dispositivo solDisp = new Solicitud_Dispositivo();
            Seguimiento_SolDispositivo seguimiento = new Seguimiento_SolDispositivo();
            Solicitud_BajoStock solBajoStock = new Solicitud_BajoStock();
            Detalle_Solicitud_BajoStock detSolBajoStock = new Detalle_Solicitud_BajoStock();
            Seguimiento_BajoStock segBajoStock = new Seguimiento_BajoStock();
            Cliente cliente = new Cliente();
            Estados_Op estadosSolicitud = new Estados_Op();
            Usuario usuario = new Usuario();
            Persona persona = new Persona();
            Mail mail = new Mail();
            public const int PageSize = 10;
            static List<ViewSolBajoStock> lstSolBajoStock = new List<ViewSolBajoStock>();
        #endregion

        //Index Pagina Dispositivos Principal
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(1);
                ViewBag.Perfil = Session["Perfil"].ToString();
                
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        //Lógica para el mantenimiento de Dispositivos
        #region Ingreso Dispositivo
            public ActionResult IngDispositivo()
            {
                if (Session["Usuario"] != null)
                {
                    ViewBag.Menu1 = menu.listaMenu1();
                    ViewBag.Menu2 = menu2.listarMenu2(1);
                    ViewBag.Perfil = Session["Perfil"].ToString();
                    ViewBag.Mensaje = mensaje;
                    mensaje = "";

                    return View(dispositivo.listarDispositivo(PageSize, 1));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }

            public ActionResult DispositivoList(int id)
            {
               return PartialView(dispositivo.listarDispositivo(PageSize, id));
            }

            public ActionResult insertDispositivo(int id = 0)
            {
                return View(id > 0 ? dispositivo.ConsultaDispositivo(id) : dispositivo);
            }

            [HttpPost]
            public ActionResult insertDispositivo(Dispositivo dispositivo)
            {
                if (Session["Usuario"] != null)
                {
                    int id = 0;
                    if (ModelState.IsValid == true)
                    {
                        id = dispositivo.Id_Dispositivo;
                        dispositivo.guardarDispositivo(id);

                        if (id > 0)
                        {
                            mensaje = "Se ha editado exitosamente el dispositivo " + dispositivo.Nombre;
                        }
                        else
                        {
                            mensaje = "Se ha creado exitosamente el dispositivo " + dispositivo.Nombre;
                        }
                        

                        if (id > 0)
                        {
                            inDisp.guardaRegistro("Modifica articulo existente", id, Convert.ToInt32(Session["IdUsuario"]));
                        }

                        return RedirectToAction("IngDispositivo");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            public ActionResult verDispositivo(int id)
            {
                return View(dispositivo.ConsultaDispositivo(id));
            }

            public ActionResult sumarCantidades(int id)
            {
                return View(dispositivo.consDispositivo(id));
            }

            [HttpPost]
            public ActionResult sumarCantidades(ViewSumarCantidades vSumarCant)
            {
                if (Session["Usuario"] != null)
                {
                    if (ModelState.IsValid == true)
                    {
                        dispositivo.SumarUnidades(vSumarCant.Id_Dispositvo, vSumarCant.cantSumar);
                        mensaje = "Se han adherido exitosamente la cantidad de "+ vSumarCant.cantSumar +" unidad(es) para el dispositivo " + vSumarCant.Nom_Dispositivo;
                        inDisp.guardaRegistro("Modifica cantidad de articulos", vSumarCant.Id_Dispositvo, Convert.ToInt32(Session["IdUsuario"]));

                        return RedirectToAction("IngDispositivo");
                    }
                    else
                    {
                        return View(dispositivo.consDispositivo(vSumarCant.Id_Dispositvo));
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        #endregion

        //Lógica para el mantenimiento de Solicitudes de Dispositivos
        #region Solicitud Dispositivo

            public ActionResult solDispositivo()
            {
                if (Session["Usuario"] != null)
                {
                    ViewBag.Menu1 = menu.listaMenu1();
                    ViewBag.Menu2 = menu2.listarMenu2(1);
                    ViewBag.Perfil = Session["Perfil"].ToString();
                    ViewBag.Mensaje = mensaje;
                    mensaje = "";

                    return View(solDisp.listarSolicitudes(PageSize, 1));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            public ActionResult SolDispositivoList(int id)
            {
                return PartialView(solDisp.listarSolicitudes(PageSize, id));
            }

            public ActionResult nuevaSolicitud()
            {
                ViewBag.Dispositivos = dispositivo.listarDispositivoDropDown();
                ViewBag.Clientes = cliente.listarCliente();
                return View();
            }

            [HttpPost]
            public ActionResult nuevaSolicitud(Solicitud_Dispositivo solicitud)
            {
                if (Session["Usuario"] != null)
                {
                    if (ModelState.IsValid == true)
                    {
                        int idSolicitud = 0;

                        solicitud.Estado_Solicitud = 1;
                        solicitud.Usuario_SolDispositivo = Convert.ToInt32(Session["IdUsuario"]);
                        solicitud.Fecha_Solicitud = System.DateTime.Now;
                        solDisp.crearSolicitud(solicitud);

                        idSolicitud = solDisp.retornaIdSolDisp(Convert.ToInt16(Session["IdUsuario"]));

                        usuario = usuario.traeUsuarioResp(1);

                        mensaje = "Se ha creado exitosamente la solicitud del dispositivo # "+ idSolicitud;

                        asunto = "Solicitud de Dispositivo # " + idSolicitud;

                        cuerpo = "Han realizado una solicitud de un dispositivo la cual quedo almacenada con el número " + idSolicitud + " \n" +
                                 "Por favor ingrese a SIGINEC y de respuesta a esta lo más pronto posible, recuerde que solo se dispone de 2 horas como máximo para dar respuesta al cliente. \n" +
                                 "Gracias.";

                        mailResp = persona.traeCorreoResp(Convert.ToInt16(usuario.Id_Persona));

                        mail.sendMail(asunto, cuerpo, mailResp);

                        return RedirectToAction("solDispositivo");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            public ActionResult seguimientoSolicitud(int id)
            {
                if (Session["Usuario"] != null)
                {
                    ViewBag.Menu1 = menu.listaMenu1();
                    ViewBag.Menu2 = menu2.listarMenu2(1);
                    ViewBag.Datos = seguimiento.ListarSeguimientos(id, PageSize, 1);
                    ViewBag.Perfil = Session["Perfil"].ToString();
                    ViewBag.Mensaje = mensaje;
                    mensaje = "";

                    return View(solDisp.verSolicitud(id));
                }
                else 
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            public ActionResult SeguimientoList(int id, int currentPage)
            {
                ViewBag.Datos = seguimiento.ListarSeguimientos(id, PageSize, currentPage);
                return PartialView();
            }

            public ActionResult nuevoSeguimiento(int id)
            {
                ViewBag.IdSolicitud = id;
                return View();
            }

            [HttpPost]
            public ActionResult nuevoSeguimiento(Seguimiento_SolDispositivo seg)
            {
                if (Session["Usuario"] != null)
                {
                    if (ModelState.IsValid == true)
                    {
                        seg.Usuario_Seguimiento = Convert.ToInt32(Session["IdUsuario"]);
                        seg.Fecha_Seguimiento = System.DateTime.Now;
                        seg.creaSeguimiento();

                        mensaje = "Se ha creado exitosamente el seguimiento para la solicitud "+ seg.Id_SolicitudDisp;

                        return RedirectToAction("seguimientoSolicitud/" + seg.Id_SolicitudDisp);
                    }
                    else
                    {
                        return View();
                    }
                }
                else 
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            [HttpPost]
            public JsonResult RetornaCantidad(int id)
            {
                var Dispositivo = dispositivo.ConsultaDispositivo(id);
                return Json(Dispositivo, JsonRequestBehavior.AllowGet);
            }

            public ActionResult cerrarSolicitud(int id)
            {
                ViewBag.ID = id;
                return View();
            }

            [HttpPost]
            public ActionResult cerrarSolicitud(ViewCerrarSolicitudDispositivo cerrarSolicitud)
            {
                if (Session["Usuario"] != null)
                {
                    Seguimiento_SolDispositivo seguimiento = new Seguimiento_SolDispositivo();

                    if (ModelState.IsValid == true)
                    {
                        seguimiento.Seguimiento = cerrarSolicitud.Observaciones;
                        seguimiento.Usuario_Seguimiento = Convert.ToInt32(Session["IdUsuario"]);
                        seguimiento.Fecha_Seguimiento = System.DateTime.Now;
                        seguimiento.Id_SolicitudDisp = cerrarSolicitud.Id;
                        seguimiento.creaSeguimiento();

                        mensaje = "Se ha cerrado exitosamente la solicitud No. " + cerrarSolicitud.Id;

                        solDisp.CerrarSolicitudDispositivo(cerrarSolicitud.Id);

                        if (cerrarSolicitud.ResuelveSolicitud == 1)
                        {
                            var disp = solDisp.Solicitud(cerrarSolicitud.Id);
                            dispositivo.DescontarUnidades(Convert.ToInt16(disp.Id_Dispositivo), Convert.ToInt16(disp.Cantidad));
                        }

                        return RedirectToAction("solDispositivo");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        #endregion

        //Lógica para el mantenimiento de Solicitudes cuando el stock esta bajo
        #region Solicitud_BajoStock

            public ActionResult SolBajoStock()
            {
                if (Session["Usuario"] != null)
                {
                    ViewBag.Menu1 = menu.listaMenu1();
                    ViewBag.Menu2 = menu2.listarMenu2(1);
                    ViewBag.Perfil = Session["Perfil"].ToString();
                    ViewBag.Mensaje = mensaje;
                    mensaje = "";

                    return View(solBajoStock.listarSolicitudesBS(PageSize, 1));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            public ActionResult SolBajoStockList(int id)
            {
                return PartialView(solBajoStock.listarSolicitudesBS(PageSize, id));
            }

            public ActionResult nuevaSolBajoStock()
            {
                ViewBag.Dispositivos = dispositivo.listarDispositivoDropDown();
                lstSolBajoStock = new List<ViewSolBajoStock>();

                return View();
            }

            [HttpPost]
            public ActionResult nuevaSolBajoStock(ViewSolBajoStock solBSTK)
            {
                if (Session["Usuario"] != null)
                {
                    int idSolicitud = 0;
                    usuario = usuario.traeUsuarioResp(2);

                    solBajoStock.Observaciones = solBSTK.Observaciones;
                    solBajoStock.Estado_Solicitud = 1;
                    solBajoStock.Usuario_SolBajoStock = Convert.ToInt16(Session["IdUsuario"]);
                    solBajoStock.Fecha_Solicitud = System.DateTime.Now;
                    solBajoStock.Usuario_Responsable = usuario.Id_Usuario;

                    solBajoStock.guardarSolicitud();
                    idSolicitud = solBajoStock.retornaIdSol(Convert.ToInt16(Session["IdUsuario"]));

                    foreach (var item in lstSolBajoStock)
                    {
                        detSolBajoStock.Id_Dispositivo = item.Id_Dispositivo;
                        detSolBajoStock.Cantidad = item.CantidadDisp;
                        detSolBajoStock.Id_Solicitud_Stock = idSolicitud;
                        detSolBajoStock.guardaDetalle();
                    }

                    mensaje = "Se ha creado exitosamente la solicitud de bajo stock "+ idSolicitud;

                    asunto = "Solicitud de Bajo Stock # " + idSolicitud;

                    cuerpo = "Han realizado una solicitud de bajo stock la cual quedo almacenada con el número " + idSolicitud +" \n"+
                             "Por favor ingrese a SIGINEC y de respuesta a esta lo más pronto posible. \n" +
                             "Gracias.";

                    mailResp = persona.traeCorreoResp(Convert.ToInt16(usuario.Id_Persona));

                    mail.sendMail(asunto, cuerpo, mailResp);

                    return RedirectToAction("SolBajoStock");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }

            [HttpPost]
            public JsonResult FillTableSolBajoStock(ViewSolBajoStock solBS)
            {

                ViewSolBajoStock obj1 = new ViewSolBajoStock();
                int? id = solBS.Id_Dispositivo;

                if (lstSolBajoStock.Count() == 0)
                {
                    lstSolBajoStock.Add(solBS);
                }
                else
                {
                    obj1 = lstSolBajoStock.Where(s => s.Id_Dispositivo == id).FirstOrDefault();

                    if (obj1 == null)
                    {
                        lstSolBajoStock.Add(solBS);
                    }
                }

                return Json(lstSolBajoStock, JsonRequestBehavior.AllowGet);
            }

            public ActionResult SeguimientoSolicitudBS(int id)
            {
                if (Session["Usuario"] != null)
                {
                    ViewBag.Menu1 = menu.listaMenu1();
                    ViewBag.Menu2 = menu2.listarMenu2(1);
                    ViewBag.Datos = segBajoStock.ListarSeguimientosBS(id, PageSize, 1);
                    ViewBag.Perfil = Session["Perfil"].ToString();
                    ViewBag.Mensaje = mensaje;
                    mensaje = "";

                    return View(solBajoStock.detalleSolBS(id));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }

            public ActionResult SeguimientoBSList(int id, int currentPage)
            {
                ViewBag.Datos = segBajoStock.ListarSeguimientosBS(id, PageSize, currentPage);
                return PartialView();
            }

            public ActionResult crearSeguimientoBS(int id)
            {
                ViewBag.IdSolicitudBS = id;

                return View(); 
            }

            [HttpPost]
            public ActionResult crearSeguimientoBS(Seguimiento_BajoStock segBS)
            {
                if (Session["Usuario"] != null && Session["Usuario"] != null)
                {
                    if (ModelState.IsValid == true)
                    {
                        segBS.Usuario_Seguimiento = Convert.ToInt16(Session["IdUsuario"]);
                        segBS.Fecha_Seguimiento = System.DateTime.Now;

                        segBS.crearSeguimientoBS();

                        mensaje = "Se ha creado exitosamente el seguimiento para la solicitud de Bajo Stock No. "+ segBS.Id_Solicitud;

                        return RedirectToAction("SeguimientoSolicitudBS/" + segBS.Id_Solicitud);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }

            public ActionResult cerrarSolBS(int id)
            {
                ViewBag.ID = id;

                return View();
            }

            [HttpPost]
            public ActionResult cerrarSolBS(ViewCerrarSolicitudDispositivo cerrarSolBS)
            {
                if (Session["Usuario"] != null && Session["Usuario"] != null)
                {
                    Seguimiento_BajoStock seguimientoBS = new Seguimiento_BajoStock();

                    if (ModelState.IsValid == true)
                    {
                        seguimientoBS.Seguimiento = cerrarSolBS.Observaciones;
                        seguimientoBS.Usuario_Seguimiento = Convert.ToInt16(Session["IdUsuario"]);
                        seguimientoBS.Fecha_Seguimiento = System.DateTime.Now;
                        seguimientoBS.Id_Solicitud = cerrarSolBS.Id;
                        seguimientoBS.crearSeguimientoBS();

                        solBajoStock.CerrarSolicitudDispositivoBS(cerrarSolBS.Id);

                        mensaje = "Se ha cerrado exitosamente la solicitud de Bajo Stock No. " + cerrarSolBS.Id;

                        return RedirectToAction("solBajoStock");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        #endregion

    }
}
