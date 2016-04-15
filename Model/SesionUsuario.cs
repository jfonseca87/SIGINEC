using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using Model.Helpers;

namespace Model
{
    public partial class SesionUsuario
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }

        public SesionUsuario LogInUsuario(string user, string password)
        {
            SesionUsuario usuario = null;
            MD5Convert convertidor = new MD5Convert();

            string MD5Password = convertidor.MD5ConvertPassword(password);

            using (var context = new SIGINECContext())
            {
                var query = from u in context.Usuario
                            where u.Nick_usuario == user && u.Password_Usuario == MD5Password && u.Activo == 1
                            select new
                            {
                                IdUsuario = u.Id_Usuario,
                                Usuario = u.Nick_usuario,
                                Nombres = u.Persona.Nombre_1 + " " + u.Persona.Apellido_1
                            };

                foreach (var item in query)
                {
                    usuario = new SesionUsuario
                    {
                        IdUsuario = item.IdUsuario,
                        Usuario = item.Usuario,
                        Nombres = item.Nombres
                    };
                }

            }

            return usuario;
        }
    }
}
