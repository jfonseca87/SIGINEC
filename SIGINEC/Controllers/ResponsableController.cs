using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class ResponsableController : Controller
    {
        #region Variables y Objetos
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Usuario usuario = new Usuario();
        #endregion

        public ActionResult Index()
        {
            if (Session["Usuario"] != null && Session["Perfil"].ToString() == "adm")
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(3);
                ViewBag.RespBodega = usuario.traeResponsable(1);
                ViewBag.RespBajoStock = usuario.traeResponsable(2);

                return View();
            }
            else
            {
                return RedirectToAction("Indexaf", "Home");
            }
        }

        public ActionResult respBodega()
        {
            ViewBag.UsuariosBodega = usuario.listarUsuariosBodega();
            return View();
        }

        [HttpPost]
        public ActionResult respBodega(ViewRespBodega vResp)
        {
            if (Session["Usuario"] != null)
            {
                if (ModelState.IsValid == true)
                {
                    usuario.desactivaResponsable(1);
                    usuario.activaResponsable(vResp.RespBodega, 1);
                    return RedirectToAction("Index");
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

        public ActionResult respBajoStock()
        {
            ViewBag.UsuariosBajoStock = usuario.listarUsuariosBodega();
            return View();
        }

        [HttpPost]
        public ActionResult respBajoStock(ViewRespBodega vResp)
        {
            if (Session["Usuario"] != null)
            {
                if (ModelState.IsValid == true)
                {
                    usuario.desactivaResponsable(2);
                    usuario.activaResponsable(vResp.RespBodega, 2);
                    return RedirectToAction("Index");
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
    }
}
