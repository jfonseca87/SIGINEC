using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGINEC.Controllers
{
    public class ClienteController : Controller
    {
        #region Variables y Objetos
            static string mensaje = "";
            public const int PageSize = 10;
            Menu1 menu = new Menu1();
            Menu2 menu2 = new Menu2();
            Persona persona = new Persona();
            Cliente cliente = new Cliente();
        #endregion

        public ActionResult Index()
        {
            if (Session["Usuario"] != null && Session["Perfil"].ToString() == "adm")
            {
                ViewBag.Menu1 = menu.listaMenu1();
                ViewBag.Menu2 = menu2.listarMenu2(3);
                ViewBag.Mensaje = mensaje;
                mensaje = "";

                return View(cliente.listarClientes(PageSize, 1));
            }
            else
            {
                return RedirectToAction("Indexaf", "Home");
            }
        }

        public ActionResult ClienteList(int id)
        {
            return PartialView(cliente.listarClientes(PageSize, id));
        }

        public ActionResult nuevoCliente()
        {
            ViewBag.Personas = persona.personasDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult nuevoCliente(ViewCliente vCliente)
        {
            if (Session["Usuario"] != null)
            {
                if (ModelState.IsValid == true)
                {
                    cliente.Id_Persona = vCliente.Persona;
                    cliente.Direccion = vCliente.Direccion;
                    cliente.Telefono = vCliente.Telefono;
                    cliente.Activo = 1;
                    cliente.GuardaCliente();
                    persona.cambiaAsignacion(vCliente.Persona);

                    mensaje = "Se ha creado exitosamente el cliente ";
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

        public ActionResult desactivaCliente(int id)
        {
            return View(cliente.consCliente(id));
        }

        [HttpPost]
        public ActionResult desactivaCliente(Cliente cliente)
        {
            if (Session["Usuario"] != null)
            {
                cliente.desactivaCliente(cliente.Id_Cliente);
                mensaje = "Se ha desactivado exitosamente el cliente con ID " + cliente.Id_Cliente;

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }   
        }
    }
}
