namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
        }

        [Key]
        public int Id_Cliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(10)]
        public string Telefono { get; set; }

        public int? Activo { get; set; }

        public int? Id_Persona { get; set; }

        public Persona Persona { get; set; }

        public virtual ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }

        public List<ClienteMostrar> listarCliente()
        {
            List<ClienteMostrar> cliente = new List<ClienteMostrar>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    cliente = (from c in context.Cliente
                               where c.Activo == 1
                               select new ClienteMostrar
                               {
                                   IdCliente = c.Id_Cliente,
                                   Nombre = c.Persona.Nombre_1 + " " + c.Persona.Apellido_1
                               }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cliente;
        }
    }
}
