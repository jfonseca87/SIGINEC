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
        #region Variables
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Dispositivo dispositivo = new Dispositivo();
            Ingreso_Dispositivo inDisp = new Ingreso_Dispositivo();
            Solicitud_Dispositivo solDisp = new Solicitud_Dispositivo();
            Seguimiento_SolDispositivo seguimiento = new Seguimiento_SolDispositivo();
            Solicitud_BajoStock solBajoStock = new Solicitud_BajoStock();
            Detalle_Solicitud_BajoStock detSolBajoStock = new Detalle_Solicitud_BajoStock();
            Cliente cliente = new Cliente();
            Estados_Op estadosSolicitud = new Estados_Op();
            public const int PageSize = 2;
            static List<ViewSolBajoStock> lstSolBajoStock = new List<ViewSolBajoStock>();
        #endregion

        //Index Pagina Dispositivos Principal
        public ActionResult Index()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(1);
            return View();
        }

        //Lógica para el mantenimiento de Dispositivos
        #region Ingreso Dispositivo
            public ActionResult IngDispositivo()
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(1);

                return View(dispositivo.listarDispositivo(PageSize, 1));
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
                int id = 0;
                if (ModelState.IsValid == true)
                {
                    id = dispositivo.Id_Dispositivo;
                    dispositivo.guardarDispositivo(id);

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

            public ActionResult verDispositivo(int id)
            {
                return View(dispositivo.ConsultaDispositivo(id));
            }

            public ActionResult sumarCantidades(int id)
            {
                return View(dispositivo.ConsultaDispositivo(id));
            }

            [HttpPost]
            public ActionResult sumarCantidades(Dispositivo dispositivo)
            {
                int id = dispositivo.Id_Dispositivo;
                if (ModelState.IsValid == true)
                {
                    dispositivo.guardarDispositivo(id);
                    inDisp.guardaRegistro("Modifica cantidad de articulos", id, Convert.ToInt32(Session["IdUsuario"]));
                    return RedirectToAction("IngDispositivo");
                }
                else
                {
                    return View(dispositivo.ConsultaDispositivo(id));
                }
            }
        #endregion

        //Lógica para el mantenimiento de Solicitudes de Dispositivos
        #region Solicitud Dispositivo

            public ActionResult solDispositivo()
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(1);

                return View(solDisp.listarSolicitudes(PageSize, 1));
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
                if (ModelState.IsValid == true)
                {
                    solicitud.Estado_Solicitud = 1;
                    solicitud.Usuario_SolDispositivo = Convert.ToInt32(Session["IdUsuario"]);
                    solicitud.Fecha_Solicitud = System.DateTime.Now;
                    solDisp.crearSolicitud(solicitud);

                    return RedirectToAction("solDispositivo");
                }
                else
                {
                    return View();
                }
            }

            public ActionResult seguimientoSolicitud(int id)
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(1);
                ViewBag.Datos = seguimiento.ListarSeguimientos(id, PageSize, 1);

                return View(solDisp.verSolicitud(id));
            }

            public ActionResult nuevoSeguimiento(int id)
            {
                ViewBag.IdSolicitud = id;
                return View();
            }

            public ActionResult SeguimientoList(int id, int currentPage)
            {
                ViewBag.Datos = seguimiento.ListarSeguimientos(id, PageSize, currentPage);
                return PartialView();
            }

            [HttpPost]
            public ActionResult nuevoSeguimiento(Seguimiento_SolDispositivo seg)
            {
                if (ModelState.IsValid == true)
                {
                    seg.Usuario_Seguimiento = Convert.ToInt32(Session["IdUsuario"]);
                    seg.Fecha_Seguimiento = System.DateTime.Now;
                    seg.creaSeguimiento();

                    return RedirectToAction("seguimientoSolicitud/" + seg.Id_SolicitudDisp);
                }
                else
                {
                    return View();
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
                Seguimiento_SolDispositivo seguimiento = new Seguimiento_SolDispositivo();

                if (ModelState.IsValid == true)
                {
                    seguimiento.Seguimiento = cerrarSolicitud.Observaciones;
                    seguimiento.Usuario_Seguimiento = Convert.ToInt32(Session["IdUsuario"]);
                    seguimiento.Fecha_Seguimiento = System.DateTime.Now;
                    seguimiento.Id_SolicitudDisp = cerrarSolicitud.Id;
                    seguimiento.creaSeguimiento();

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
            
        #endregion

        //Lógica para el mantenimiento de Solicitudes cuando el stock esta bajo
        #region Solicitud_BajoStock

            public ActionResult SolBajoStock()
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(1);

                return View(solBajoStock.listarSolicitudesBS(PageSize,1));
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
                int idSolicitud = 0;

                solBajoStock.Observaciones = solBSTK.Observaciones;
                solBajoStock.Estado_Solicitud = 1;
                solBajoStock.Usuario_SolBajoStock = Convert.ToInt16(Session["IdUsuario"]);
                solBajoStock.Fecha_Solicitud = System.DateTime.Now;
                solBajoStock.Usuario_Responsable = Convert.ToInt16(Session["IdUsuario"]);

                solBajoStock.guardarSolicitud();
                idSolicitud = solBajoStock.retornaIdSol(Convert.ToInt16(Session["IdUsuario"]));

                foreach (var item in lstSolBajoStock)
                {
                    detSolBajoStock.Id_Dispositivo = item.Id_Dispositivo;
                    detSolBajoStock.Cantidad = item.CantidadDisp;
                    detSolBajoStock.Id_Solicitud_Stock = idSolicitud;
                    detSolBajoStock.guardaDetalle();
                }

                return RedirectToAction("SolBajoStock");
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
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(1);
                //ViewBag.Datos = ;

                return View(solBajoStock.detalleSolBS(id));
            }

        #endregion
    }
}
