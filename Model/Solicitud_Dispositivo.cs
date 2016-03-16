namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Solicitud_Dispositivo
    {
        public Solicitud_Dispositivo()
        {
            Seguimiento_SolDispositivo = new List<Seguimiento_SolDispositivo>();
        }

        [Key]
        public int Id_Solicitud { get; set; }

        public string Observaciones { get; set; }

        public int? Estado_Solicitud { get; set; }

        public int? Id_Dispositivo { get; set; }

        public int? Id_Cliente { get; set; }

        public int? Usuario_SolDispositivo { get; set; }

        public DateTime? Fecha_Solicitud { get; set; }

        public Cliente Cliente { get; set; }

        public Dispositivo Dispositivo { get; set; }

        public Estados_Op Estados_Op { get; set; }

        public ICollection<Seguimiento_SolDispositivo> Seguimiento_SolDispositivo { get; set; }

        public Usuario Usuario { get; set; }
    }
}
