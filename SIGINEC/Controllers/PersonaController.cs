using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class PersonaController : Controller
    {

        #region Variables y Objetos
            static string mensaje = "";
            public const int PageSize = 2;
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Persona persona = new Persona();
        #endregion

        public ActionResult Index()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(3);
            ViewBag.Mensaje = mensaje;
            mensaje = "";
            return View( persona.listarPersonas(PageSize, 1) );
        }

        public ActionResult PersonasList(int id)
        {
            return PartialView(persona.listarPersonas(PageSize, id));
        }

        public ActionResult nuevaPersona()
        {
            return View();
        }

        [HttpPost]
        public ActionResult nuevaPersona(Persona persona)
        {
            if (ModelState.IsValid == true)
            {
                persona.Nombres_Mostrar = persona.Nombre_1.Trim() + " " + persona.Apellido_1.Trim();
                persona.GuardaPersona();

                mensaje = "Se ha creado exitosamente la persona " + persona.Nombres_Mostrar;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult editaPersona(int id)
        {
            return View(persona.consPersona(id));
        }

        [HttpPost]
        public ActionResult editaPersona(Persona persona) 
        {
            if (ModelState.IsValid == true)
            {
                persona.Nombres_Mostrar = persona.Nombre_1.Trim() + " " + persona.Apellido_1.Trim();
                persona.EditaPersona();

                mensaje = "Se ha editado exitosamente la persona " + persona.Nombres_Mostrar;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult consPersona(string documento)
        {
            return Json(persona.consPersona(documento), JsonRequestBehavior.AllowGet);
        }

    }
}
