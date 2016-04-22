namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Seguimiento_BajoStock
    {
        [Key]
        public int Id_Seguimiento { get; set; }

        [Required]
        public string Seguimiento { get; set; }

        public int Usuario_Seguimiento { get; set; }

        public DateTime? Fecha_Seguimiento { get; set; }

        public int? Id_Solicitud { get; set; }

        public Solicitud_BajoStock Solicitud_BajoStock { get; set; }

        public Usuario Usuario { get; set; }
    }
}
