using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class BitacoraController : Controller
    {
        #region Variables y Objetos
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Dispositivo dispositivo = new Dispositivo();
            Estado_Dispositivo estDispositivo = new Estado_Dispositivo();
            static List<Detalle_Bitacora> Adjuntos = new List<Detalle_Bitacora>();
        #endregion

        //Index Pagina Bitacoras Principal
        public ActionResult Index()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(2);
            return View();
        }

        #region Acciones
            public ActionResult detBitacora()
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(2);
                return View();
            }

            public ActionResult nuevaBitacora()
            {
                ViewBag.Dispositivos = dispositivo.listarDispositivoDropDown();
                ViewBag.EstadosDisp = estDispositivo.ListarEstadosDropDown();
                Adjuntos.Clear();
                return View();
            }

            [HttpPost]
            public ActionResult nuevaBitacora(Bitacora bitacora)
            {
                if (ModelState.IsValid == true)
                {
                    return RedirectToAction("detBitacora");
                }
                else
                {
                    return View();
                }
            }

            [HttpPost]
            public JsonResult listAdjuntos(Detalle_Bitacora detBitacora)
            {
                Adjuntos.Add(detBitacora);

                return Json(Adjuntos, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            public JsonResult eliminaRegistro(string adjunto)
            {
                Adjuntos.RemoveAll(a => a.Fotografia == adjunto);
                return Json(Adjuntos, JsonRequestBehavior.AllowGet);
            }

        #endregion

    }
}
