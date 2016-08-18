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

        [Required]
        [StringLength(50)]
        public string Nick_usuario { get; set; }

        [Required]
        [StringLength(32)]
        public string Password_Usuario { get; set; }

        public int? Activo { get; set; }

        public string Tipo_Usuario { get; set; }

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
                                Nombres = u.Persona.Nombre_1 +" "+ u.Persona.Apellido_1 
                            }).FirstOrDefault();
            }

            return sUsuario;
        }
        
    }
}
