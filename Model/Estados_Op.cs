namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Estados_Op
    {
        public Estados_Op()
        {
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
            Solicitud_BajoStock = new List<Solicitud_BajoStock>();
        }

        [Key]
        public int id_Estado { get; set; }

        [StringLength(15)]
        public string Estado_Op { get; set; }

        public int? Activo { get; set; }

        public ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }

        public ICollection<Solicitud_BajoStock> Solicitud_BajoStock { get; set; }
    }
}
