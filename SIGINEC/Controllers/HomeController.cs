using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class HomeController : Controller
    {
        #region Variables y Objetos
            Usuario usuario = new Usuario();
        #endregion

        //Index Pagina Home Principal
        public ActionResult Index()
        {
            Session["Operacion"] = "LogIn";
            return View();
        }

        //Lógica para entrar o salir de la aplicación
        #region Acciones
            public ActionResult Indexaf()
            {
                Menu1 menu = new Menu1();

                Session["Operacion"] = "LogOut";
                ViewBag.Menu1 = menu.listaMenu1();
                return View();
            }

            public ActionResult Ingresar()
            {
                return View();
            }

            [HttpPost]
            public JsonResult Ingresar(ViewIngresar ingresar)
            {
                string defaultPassword = "1234";
                SesionUsuario User = new SesionUsuario();

                if (ingresar.Password == null)
                {
                    User = usuario.LogInUsuario(ingresar.User, defaultPassword);
                }
                else
                {
                    User = usuario.LogInUsuario(ingresar.User, ingresar.Password);
                }


                if (User != null)
                {
                    Session["IdUsuario"] = User.IdUsuario;
                    Session["Usuario"] = User.Usuario;
                    Session["Nombres"] = User.Nombres;
                }
                else
                {
                    User = new SesionUsuario
                    {
                        IdUsuario = 0,
                        Usuario = "",
                        Nombres = ""
                    };
                }

                return Json(User, JsonRequestBehavior.AllowGet);
            }

            public ActionResult Salir()
            {
                Session["IdUsuario"] = "";
                Session["Usuario"] = "";
                Session["Nombres"] = "";

                return RedirectToAction("Index");
            }
        
        #endregion
    }  
}
