namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Seguimiento_SolDispositivo
    {
        [Key]
        public int Id_Seguimiento { get; set; }

        [Required]
        public string Seguimiento { get; set; }

        public int Usuario_Seguimiento { get; set; }

        public DateTime? Fecha_Seguimiento { get; set; }

        public int? Id_SolicitudDisp { get; set; }

        public Solicitud_Dispositivo Solicitud_Dispositivo { get; set; }
    }
}
