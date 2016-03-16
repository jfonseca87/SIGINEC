namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bitacora")]
    public partial class Bitacora
    {
        public Bitacora()
        {
            Detalle_Bitacora = new List<Detalle_Bitacora>();
        }

        [Key]
        public int Id_Bitacora { get; set; }

        public int? Id_Dispositivo { get; set; }

        public int? Estado_Dispositivo { get; set; }

        public string Detalles_Revision { get; set; }

        public string Observaciones { get; set; }

        public int? Usuario_Registra { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        public Estado_Dispositivo Estado_Dispositivo1 { get; set; }

        public Dispositivo Dispositivo { get; set; }

        public Usuario Usuario { get; set; }

        public virtual ICollection<Detalle_Bitacora> Detalle_Bitacora { get; set; }
    }
}
