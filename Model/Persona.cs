namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persona")]
    public partial class Persona
    {
        public Persona()
        {
            Cliente = new List<Cliente>();
            Usuario = new List<Usuario>();
        }

        [Key]
        public int Id_Persona { get; set; }

        [Required]
        [StringLength(2)]
        public string Tipo_Documento { get; set; }

        [Required]
        [StringLength(50)]
        public string Numero_Documento { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre_1 { get; set; }

        [StringLength(50)]
        public string Nombre_2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido_1 { get; set; }

        [StringLength(50)]
        public string Apellido_2 { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Cargo { get; set; }

        public ICollection<Cliente> Cliente { get; set; }

        public ICollection<Usuario> Usuario { get; set; }
    }
}
