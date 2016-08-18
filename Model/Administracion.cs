namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Administracion
    {
        [Key]
        public int Id_Admin { get; set; }
        public int? Resp_Bodega { get; set; }
        public int? Resp_BajoStock { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
