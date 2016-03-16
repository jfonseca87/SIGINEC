namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dispositivo")]
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            Bitacora = new List<Bitacora>();
            Detalle_Solicitud_BajoStock = new List<Detalle_Solicitud_BajoStock>();
            Ingreso_Dispositivo = new List<Ingreso_Dispositivo>();
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
        }

        [Key]
        public int Id_Dispositivo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Clase { get; set; }

        [Required]
        [StringLength(100)]
        public string Marca { get; set; }

        [Required]
        [StringLength(100)]
        public string Modelo { get; set; }

        public string Caracteristicas { get; set; }

        public string Configuracion { get; set; }

        public int? Cantidad { get; set; }

        public ICollection<Bitacora> Bitacora { get; set; }

        public ICollection<Detalle_Solicitud_BajoStock> Detalle_Solicitud_BajoStock { get; set; }

        public ICollection<Ingreso_Dispositivo> Ingreso_Dispositivo { get; set; }

        public ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }
    }
}
