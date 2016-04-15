namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu2
    {
        [Key]
        public int id_Menu2 { get; set; }

        [Required]
        [StringLength(50)]
        public string subPagina { get; set; }

        public int? activa { get; set; }

        public int? id_Menu1 { get; set; }

        public virtual Menu1 Menu1 { get; set; }
    }
}
