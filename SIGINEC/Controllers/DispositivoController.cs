using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class DispositivoController : Controller
    {
        //
        // GET: /Dispositivo/

        public ActionResult Index()
        {
            return View();
        }

        //Metodos para el ingreso de Dispositivos al Sistema
        public ActionResult Nuevo_Dispositivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo_Dispositivo(int id)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
            
        }


    }
}
