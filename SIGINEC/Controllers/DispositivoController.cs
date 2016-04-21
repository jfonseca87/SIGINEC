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
            Cliente cliente = new Cliente();
            Estados_Op estadosSolicitud = new Estados_Op();
            public const int PageSize = 10;
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
                    //solDisp.guardarSolicitud();

                    return RedirectToAction("solDispositivo");
                }
                else
                {
                    return View();
                }
            }
            
        #endregion
    }
}
