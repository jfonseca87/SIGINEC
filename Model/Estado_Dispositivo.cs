namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Estado_Dispositivo
    {
        public Estado_Dispositivo()
        {
            Bitacora = new List<Bitacora>();
        }

        [Key]
        public int id_Estado { get; set; }

        [Required]
        [StringLength(15)]
        public string Estado { get; set; }

        public int? Activo { get; set; }

        public ICollection<Bitacora> Bitacora { get; set; }
    }
}
