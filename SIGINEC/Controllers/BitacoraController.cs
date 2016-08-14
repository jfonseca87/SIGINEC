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
            public const int PageSize = 2;
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
                return View(bitacora.listarBitacora(PageSize, 1));
            }

            public ActionResult BitacoraList(int id)
            {
                return PartialView(bitacora.listarBitacora(PageSize, id));
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
                int Id_Bitacora = 0;

                if (ModelState.IsValid == true)
                {
                    bitacora.Usuario_Registra = Convert.ToInt16(Session["IdUsuario"]);
                    bitacora.Fecha_Registro = System.DateTime.Now;
                    bitacora.Guardar();
                    Id_Bitacora = bitacora.TraeIdBitacora(Convert.ToInt16(Session["IdUsuario"]));

                    if (bitacora.Id_Estado_Dispositivo == 1)
                    {
                        dispositivo.SumarUnidades(bitacora.Id_Dispositivo, 1);
                    }

                    if (Adjuntos.Count != 0)
                    {
                        foreach (var item in Adjuntos)
                        {
                            detaBitacora.Fotografia = item.FileName;
                            detaBitacora.Id_Bitacora = Id_Bitacora;
                            detaBitacora.Guardar();
                            almAdjunto.saveFile(Id_Bitacora, item);
                        }
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

            public ActionResult detalleBitacora(int id)
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(2);
                return View( bitacora.traeBitacora(id) );
            }

            [HttpPost]
            public JsonResult conImagenes(int id)
            {
                List<Detalle_Bitacora> lstImagenes = detaBitacora.traeImagenes(id);
                return Json(lstImagenes, JsonRequestBehavior.AllowGet);
            }

            public ActionResult traeImagenes(int id)
            {
                ViewBag.Id = id;
                return View(detaBitacora.traeImagenes(id));
            }

        #endregion

    }
}
