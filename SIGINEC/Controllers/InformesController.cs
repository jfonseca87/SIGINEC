using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class InformesController : Controller
    {
        #region Varioables y Objetos
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            SolDispMes solDisp = new SolDispMes();
        #endregion 

        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                ViewBag.Menu1 = menu.listaMenu1();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult InfSolDispositivos()
        {
            if (Session["Usuario"] != null)
            {
                ViewBag.Menu1 = menu.listaMenu1();

                return View();
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public JsonResult traeInformeSolDisp()
        {
            var chartsdata = solDisp.infSolDispMes();
            return Json(chartsdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InfSolBajoStock()
        {
            if (Session["Usuario"] != null)
            {
                ViewBag.Menu1 = menu.listaMenu1();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public JsonResult traeInformeSolBajoStock()
        {
            var chartsdata = solDisp.infSolBajoStockMes();
            return Json(chartsdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InfBitacoras()
        {
            if (Session["Usuario"] != null)
            {
                ViewBag.Menu1 = menu.listaMenu1();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public JsonResult traeInformeBitacoras()
        {
            var chartsdata = solDisp.infBitacoras();
            return Json(chartsdata, JsonRequestBehavior.AllowGet);
        }
    }
}
