using System;
using Model;
using Model.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class UsuarioController : Controller
    {
        #region Variables y Objetos
            static string mensaje = "";
            public const int PageSize = 2;
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Persona persona = new Persona();
            Usuario usuario = new Usuario();
            MD5Convert convertidor = new MD5Convert();
        #endregion

        public ActionResult Index()
        {
            ViewBag.Menu1 = menu.listaMenu1();
            ViewBag.Menu2 = menu2.listarMenu2(3);
            ViewBag.Mensaje = mensaje;
            mensaje = "";
            return View( usuario.listarUsuarios(PageSize, 1) );
        }

        public ActionResult UsuarioList(int id)
        {
            return PartialView( usuario.listarUsuarios(PageSize, id) );
        }

        public ActionResult nuevoUsuario()
        {
            ViewBag.Personas = persona.personasDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult nuevoUsuario(ViewUsuario vUsuario)
        {
            if (ModelState.IsValid == true)
            {
                usuario.Id_Persona = vUsuario.Persona;
                usuario.Nick_usuario = vUsuario.Usuario;
                usuario.Password_Usuario = convertidor.MD5ConvertPassword(vUsuario.Contrasena.Trim());
                usuario.Tipo_Usuario = vUsuario.Tipo_Usuario;
                usuario.Activo = 1;
                usuario.GuardarUsuario();

                persona.cambiaAsignacion(vUsuario.Persona);

                mensaje = "Se ha creado exitosamente el usuario " + vUsuario.Usuario;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult consUsuario(string user)
        {
            return Json(usuario.consUsuario(user), JsonRequestBehavior.AllowGet);
        }

        public ActionResult desactivaUsuario(int id)
        {
            return View(usuario.consUsuario(id));
        }

        [HttpPost]
        public ActionResult desactivaUsuario(Usuario usuario)
        {
            usuario.DesactivaUsuario(usuario.Id_Usuario);
            mensaje = "Se ha desactivado exitosamente el usuario " + usuario.Nick_usuario;
            return RedirectToAction("Index");
        }
    }
}
