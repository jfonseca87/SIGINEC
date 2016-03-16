namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Bitacora = new List<Bitacora>();
            Ingreso_Dispositivo = new List<Ingreso_Dispositivo>();
            Solicitud_BajoStock = new List<Solicitud_BajoStock>();
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
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

        public int? Id_Persona { get; set; }

        public ICollection<Bitacora> Bitacora { get; set; }

        public ICollection<Ingreso_Dispositivo> Ingreso_Dispositivo { get; set; }

        public Persona Persona { get; set; }

        public ICollection<Solicitud_BajoStock> Solicitud_BajoStock { get; set; }

        public ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }
    }
}
