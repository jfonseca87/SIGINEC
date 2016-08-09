using Model;
using SIGINEC.AuxiliaryFiles;
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
            Bitacora bitacora = new Bitacora();
            Detalle_Bitacora detaBitacora = new Detalle_Bitacora();
            AlmacenaAdjunto almAdjunto = new AlmacenaAdjunto();
            static List<HttpPostedFileBase> Adjuntos = new List<HttpPostedFileBase>();
            static List<infAdjunto> AdjuntosString = new List<infAdjunto>();
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
                AdjuntosString.Clear();
                return View();
            }

            [HttpPost]
            public ActionResult nuevaBitacora(Bitacora bitacora)
            {
                if (ModelState.IsValid == true)
                {

                    foreach (var item in Adjuntos)
                    {
                        almAdjunto.saveFile(1, item);
                    }

                    return RedirectToAction("detBitacora");
                }
                else
                {
                    return View();
                }
            }

            [HttpPost]
            public void listAdjuntos(HttpPostedFileBase adjunto)
            {
                Adjuntos.Add(adjunto);
                AdjuntosString.Add(new infAdjunto { NomArchivo = adjunto.FileName, PesoArchivo = (adjunto.ContentLength / 1000).ToString()});
            }

            public JsonResult listarAdjuntos()
            {
                return Json(AdjuntosString, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            public JsonResult eliminaRegistro(string adjunto)
            {
                Adjuntos.RemoveAll(a => a.FileName == adjunto);
                AdjuntosString.RemoveAll(a => a.NomArchivo == adjunto);

                return Json(AdjuntosString, JsonRequestBehavior.AllowGet);
            }

        #endregion

    }
}
