using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class AdministracionController : Controller
    {

        #region Variables y Objetos
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
        #endregion
        //
        // GET: /Administracion/

        public ActionResult Index()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(3);
            return View();
        }

    }
}
