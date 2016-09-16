namespace Model
{
    using System;
    using Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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

        [Required(ErrorMessage="Debe seleccionar una opción")]
        [StringLength(3)]
        public string Tipo_Documento { get; set; }

        [Required(ErrorMessage = "Debe ingresar su número de documento")]
        [StringLength(50)]
        public string Numero_Documento { get; set; }

        [Required(ErrorMessage = "Ingrese su primer Nombre")]
        [StringLength(50)]
        public string Nombre_1 { get; set; }

        [StringLength(50)]
        public string Nombre_2 { get; set; }

        [Required(ErrorMessage = "Ingrese su primer Apellido")]
        [StringLength(50)]
        public string Apellido_1 { get; set; }

        [StringLength(50)]
        public string Apellido_2 { get; set; }

        public string Nombres_Mostrar { get; set; }

        [Required(ErrorMessage="El email es requerido")]
        [EmailAddress(ErrorMessage = "Correo incorrecto")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Cargo { get; set; }

        public int Asignado { get; set; }

        public ICollection<Cliente> Cliente { get; set; }

        public ICollection<Usuario> Usuario { get; set; }

        public void GuardaPersona()
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

        public void EditaPersona()
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void cambiaAsignacion(int id)
        {
            Persona persona = new Persona();

            try
            {
                using (var context = new SIGINECContext())
                {
                    persona = (from p in context.Persona
                               where p.Id_Persona == id && p.Asignado == 0
                               select p).FirstOrDefault();
                    persona.Asignado = 1;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedData<Persona> listarPersonas(int PageSize, int CurrentPage)
        {
            var lstPersona = new PagedData<Persona>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        lstPersona.Data = (from p in context.Persona
                                           where p.Asignado == 0 
                                           orderby p.Id_Persona
                                           select p
                                           ).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                    }
                    else
                    {
                        lstPersona.Data = (from p in context.Persona
                                           where p.Asignado == 0
                                           orderby p.Id_Persona
                                           select p
                                           ).Take(PageSize).ToList();

                    }

                    lstPersona.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Persona.Where(p => p.Asignado == 0).Count() / PageSize));
                    lstPersona.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstPersona;
        }

        public List<Persona> personasDropDownList()
        {
            List<Persona> lstPersonas = new List<Persona>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstPersonas = (from p in context.Persona
                                   where p.Asignado == 0
                                   orderby p.Nombre_1
                                   select p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstPersonas;
        }

        public Persona consPersona(int id)
        {
            Persona persona = new Persona();

            try
            {
                using (var context = new SIGINECContext())
                {
                    persona = (from p in context.Persona
                               where p.Id_Persona == id
                               select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return persona;
        }

        public List<Persona> consPersona(string documento)
        {
            List<Persona> persona = new List<Persona>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    persona = (from p in context.Persona
                               where p.Numero_Documento == documento
                               select p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return persona;
        }

        public string traeCorreoResp(int id)
        {
            string correo = "";

            try
            {
                using (var context = new SIGINECContext())
                {
                    correo = (from p in context.Persona
                              where p.Id_Persona == id
                              select p.Email).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return correo;
        }
    }
}
