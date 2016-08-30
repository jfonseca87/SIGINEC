namespace Model
{
    using Model.Helpers;
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

        public PagedData<ViewListCliente> listarClientes(int PageSize, int CurrentPage)
        {
            var lstCliente = new PagedData<ViewListCliente>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        lstCliente.Data = (from c in context.Cliente
                                           where c.Activo == 1
                                           orderby c.Id_Cliente
                                           select new ViewListCliente
                                           {
                                               Id_Cliente = c.Id_Cliente,
                                               Identificacion = c.Persona.Numero_Documento,
                                               Nombres = c.Persona.Nombres_Mostrar,
                                               Direccion = c.Direccion,
                                               Telefono = c.Telefono
                                           }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                    }
                    else
                    {
                        lstCliente.Data = (from c in context.Cliente
                                           where c.Activo == 1
                                           orderby c.Id_Cliente
                                           select new ViewListCliente
                                           {
                                               Id_Cliente = c.Id_Cliente,
                                               Identificacion = c.Persona.Numero_Documento,
                                               Nombres = c.Persona.Nombres_Mostrar,
                                               Direccion = c.Direccion,
                                               Telefono = c.Telefono
                                           }).Take(PageSize).ToList();

                    }

                    lstCliente.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Cliente.Where(c => c.Activo == 1).Count() / PageSize));
                    lstCliente.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstCliente;
        }

        public void GuardaCliente()
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Cliente consCliente(int id)
        {
            Cliente cliente = new Cliente();

            try
            {
                using (var context = new SIGINECContext())
                {
                    cliente = context.Cliente.Where(c => c.Id_Cliente == id).First();
                }
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }

            return cliente;
        }

        public void desactivaCliente(int id)
        {
            Cliente cliente = new Cliente();

            try
            {
                using (var context = new SIGINECContext())
                {
                    cliente = context.Cliente.Where(c => c.Id_Cliente == id).First();
                    cliente.Activo = 0;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
