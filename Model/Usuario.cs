namespace Model
{
    using System;
    using Model.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Bitacora = new List<Bitacora>();
            Ingreso_Dispositivo = new List<Ingreso_Dispositivo>();
            Solicitud_BajoStock = new List<Solicitud_BajoStock>();
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
            Seguimiento_SolDispositivo = new List<Seguimiento_SolDispositivo>();
            Seguimiento_BajoStock = new List<Seguimiento_BajoStock>();
            Administracion = new List<Administracion>();
        }

        [Key]
        public int Id_Usuario { get; set; }

        [Required(ErrorMessage="Debe ingresar el Nick del usuario")]
        [StringLength(50)]
        public string Nick_usuario { get; set; }

        [Required(ErrorMessage="Debe ingresar una contraseña")]
        [StringLength(32)]
        public string Password_Usuario { get; set; }

        public int? Activo { get; set; }

        public string Tipo_Usuario { get; set; }

        public int Responsable { get; set; }

        public int? Id_Persona { get; set; }

        public ICollection<Bitacora> Bitacora { get; set; }

        public ICollection<Ingreso_Dispositivo> Ingreso_Dispositivo { get; set; }

        public Persona Persona { get; set; }

        public ICollection<Solicitud_BajoStock> Solicitud_BajoStock { get; set; }

        public ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }

        public ICollection<Seguimiento_SolDispositivo> Seguimiento_SolDispositivo { get; set; }

        public ICollection<Seguimiento_BajoStock> Seguimiento_BajoStock { get; set; }

        public ICollection<Administracion> Administracion { get; set; }

        public SesionUsuario LogInUsuario(string user, string password)
        {
            SesionUsuario sUsuario = new SesionUsuario();
            MD5Convert convertidor = new MD5Convert();

            string MD5Password = convertidor.MD5ConvertPassword(password);

            using (var context = new SIGINECContext())
            {
                sUsuario = (from u in context.Usuario
                            where u.Nick_usuario == user && u.Password_Usuario == MD5Password && u.Activo == 1
                            select new SesionUsuario
                            {
                                IdUsuario = u.Id_Usuario,
                                Usuario = u.Nick_usuario,
                                Nombres = u.Persona.Nombre_1 +" "+ u.Persona.Apellido_1,
                                Perfil = u.Tipo_Usuario
                            }).FirstOrDefault();
            }

            return sUsuario;
        }

        public void GuardarUsuario()
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DesactivaUsuario(int id)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = (from u in context.Usuario
                               where u.Id_Usuario == id
                               select u).FirstOrDefault();

                    usuario.Activo = 0;
                    context.SaveChanges();
                }
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedData<ViewListUsuario> listarUsuarios(int PageSize, int CurrentPage)
        {
            var lstUsuario = new PagedData<ViewListUsuario>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        lstUsuario.Data = (from u in context.Usuario
                                           where u.Activo == 1
                                           orderby u.Id_Usuario
                                           select new ViewListUsuario 
                                           {
                                               Id_Usuario = u.Id_Usuario,
                                               Num_Documento = u.Persona.Numero_Documento,
                                               Nombres = u.Persona.Nombre_1 +" "+ u.Persona.Apellido_1,
                                               NickName = u.Nick_usuario,
                                               Tipo_Usuario = u.Tipo_Usuario
                                           }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                    }
                    else
                    {
                        lstUsuario.Data = (from u in context.Usuario
                                           where u.Activo == 1
                                           orderby u.Id_Usuario
                                           select new ViewListUsuario
                                           {
                                               Id_Usuario = u.Id_Usuario,
                                               Num_Documento = u.Persona.Numero_Documento,
                                               Nombres = u.Persona.Nombre_1 + " " + u.Persona.Apellido_1,
                                               NickName = u.Nick_usuario,
                                               Tipo_Usuario = u.Tipo_Usuario
                                           }).Take(PageSize).ToList();

                    }

                    lstUsuario.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Usuario.Where(u => u.Activo == 1).Count() / PageSize));
                    lstUsuario.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstUsuario;
        }

        public Usuario consUsuario(int id)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = context.Usuario.Where(u => u.Id_Usuario == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return usuario;
        }

        public List<Usuario> consUsuario(string user)
        {
            List<Usuario> usuario = new List<Usuario>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = context.Usuario.Where(u => u.Nick_usuario == user.Trim()).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return usuario;
        }

        public string traeResponsable(int id)
        {
            string persona = "";

            try
            {
                using (var context = new SIGINECContext())
                {
                    persona = (from u in context.Usuario
                               where u.Responsable == id
                               select u.Persona.Nombres_Mostrar).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return persona;
        }

        public Usuario traeUsuarioResp(int id)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = (from u in context.Usuario
                               where u.Responsable == id
                               select u).FirstOrDefault();
                }
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }

            return usuario;
        }


        public List<UsuariosBodega> listarUsuariosBodega()
        {
            List<UsuariosBodega> lstUsuariosb = new List<UsuariosBodega>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstUsuariosb = (from u in context.Usuario
                                    where u.Activo == 1 && u.Responsable == 0
                                    select new UsuariosBodega
                                    {
                                        Id_Usuario = u.Id_Usuario,
                                        Nom_Usuario = u.Persona.Nombres_Mostrar
                                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstUsuariosb;
        }

        public void activaResponsable(int id, int valor)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = context.Usuario.Where(u => u.Id_Usuario == id).FirstOrDefault();
                    usuario.Responsable = valor;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void desactivaResponsable(int valor)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = (from u in context.Usuario
                               where u.Responsable == valor
                               select u).FirstOrDefault();
                    if (usuario != null)
                    {
                        usuario.Responsable = 0;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ViewCambioPassword conUsuario(int id)
        {
            ViewCambioPassword vCambioPasswordInfo = new ViewCambioPassword();

            try
            {
                using (var context = new SIGINECContext())
                {
                    vCambioPasswordInfo = (from vcp in context.Usuario
                                           where vcp.Id_Usuario == id
                                           select new ViewCambioPassword
                                           {
                                               Id_Usuario = vcp.Id_Usuario,
                                               Nombres = vcp.Persona.Nombres_Mostrar
                                           }).FirstOrDefault();
                }
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }

            return vCambioPasswordInfo;
        }

        public void cambioPassword(int id, string password)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (var context = new SIGINECContext())
                {
                    usuario = (from u in context.Usuario
                               where u.Id_Usuario == id
                               select u).FirstOrDefault();
                    usuario.Password_Usuario = password;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
