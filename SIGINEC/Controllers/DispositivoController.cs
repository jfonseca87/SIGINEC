using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class DispositivoController : Controller
    {
        Menu1 menu = new Menu1();
        Menu2 menu2 = new Menu2();
        //
        // GET: /Dispositivo/

        public ActionResult Index()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(1);
            return View();
        }

        public ActionResult IngDispositivo()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(1);
            return View();
        }

    }
}
