using Model;
using Model.Helpers;
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
            static string mensaje = "";  
            Usuario usuario = new Usuario();
            MD5Convert convertidor = new MD5Convert();
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
                if (Session["Usuario"] != null)
                {
                    Menu1 menu = new Menu1();

                    Session["Operacion"] = "LogOut";
                    ViewBag.Menu1 = menu.listaMenu1();
                    ViewBag.Perfil = Session["Perfil"].ToString();
                    ViewBag.idUsuario = Session["IdUsuario"];
                    ViewBag.Mensaje = mensaje;
                    mensaje = "";

                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
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
                    Session["Perfil"] = User.Perfil;
                }
                else
                {
                    User = new SesionUsuario
                    {
                        IdUsuario = 0,
                        Usuario = "",
                        Nombres = "",
                        Perfil = ""
                    };
                }

                return Json(User, JsonRequestBehavior.AllowGet);
            }

            public ActionResult Salir()
            {
                Session["IdUsuario"] = null;
                Session["Usuario"] = null;
                Session["Nombres"] = null;
                Session["Perfil"] = null;

                return RedirectToAction("Index");
            }

            public ActionResult cambioPassword(int id)
            {
                return View(usuario.conUsuario(id));
            }

            [HttpPost]
            public ActionResult cambioPassword(ViewCambioPassword vCambioPassword)
            {
                string NuevoPassword = "";

                if (ModelState.IsValid == true)
                {
                    NuevoPassword = convertidor.MD5ConvertPassword(vCambioPassword.Nuevo_Password);
                    usuario.cambioPassword(vCambioPassword.Id_Usuario, NuevoPassword);
                    mensaje = "Ha cambiado exitosamente la contraseña para " + vCambioPassword.Nombres;

                    return RedirectToAction("Indexaf");
                }
                else
                {
                    return View();
                }
            }
        
        #endregion
    }  
}
