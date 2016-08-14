namespace Model
{
    using System;
    using Model.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Bitacora")]
    public partial class Bitacora
    {
        public Bitacora()
        {
            Detalle_Bitacora = new List<Detalle_Bitacora>();
        }

        [Key]
        public int Id_Bitacora { get; set; }

        [Required(ErrorMessage="Debe seleccionar un Dispositivo")]
        public int Id_Dispositivo { get; set; }

        [Required(ErrorMessage="Debe seleccionar un estado")]
        public int Id_Estado_Dispositivo { get; set; }

        [Required(ErrorMessage=("Ingrese los detalles obtenidos en la revisión"))]
        public string Detalles_Revision { get; set; }

        [Required(ErrorMessage = ("Ingrese almenos una observación"))]
        public string Observaciones { get; set; }

        public int? Usuario_Registra { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        public virtual Estado_Dispositivo Estado_Dispositivo { get; set; }

        public virtual Dispositivo Dispositivo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Detalle_Bitacora> Detalle_Bitacora { get; set; }

        public void Guardar() {
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

        public int TraeIdBitacora(int UserID)
        {
            int ID_Bitacora = 0;

            try
            {
                using (var context = new SIGINECContext())
                {
                    ID_Bitacora = (from b in context.Bitacora
                                   where b.Usuario_Registra == UserID
                                   orderby b.Id_Bitacora descending
                                   select b.Id_Bitacora).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ID_Bitacora;
        }

        public PagedData<ViewListBitacora> listarBitacora(int PageSize, int CurrentPage)
        {
            var lstBitacora = new PagedData<ViewListBitacora>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        lstBitacora.Data = (from b in context.Bitacora
                                            orderby b.Id_Bitacora 
                                            select new ViewListBitacora
                                            {
                                                Id_Bitacora = b.Id_Bitacora,
                                                Nom_Dispositivo = b.Dispositivo.Nombre,
                                                Estado_Dispositivo = b.Estado_Dispositivo.Estado,
                                                Usuario_Registra = b.Usuario.Persona.Nombre_1 + " " + b.Usuario.Persona.Apellido_2,
                                                Fecha_Registro = b.Fecha_Registro
                                            }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                    }
                    else
                    {
                        lstBitacora.Data = (from b in context.Bitacora
                                            orderby b.Id_Bitacora
                                            select new ViewListBitacora
                                            {
                                                Id_Bitacora = b.Id_Bitacora,
                                                Nom_Dispositivo = b.Dispositivo.Nombre,
                                                Estado_Dispositivo = b.Estado_Dispositivo.Estado,
                                                Usuario_Registra = b.Usuario.Persona.Nombre_1 + " " + b.Usuario.Persona.Apellido_2,
                                                Fecha_Registro = b.Fecha_Registro
                                            }).Take(PageSize).ToList();

                    }

                    lstBitacora.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Bitacora.Count() / PageSize));
                    lstBitacora.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstBitacora;
        }

        public ViewDetBitacora traeBitacora(int id)
        {
            ViewDetBitacora detBitacora = new ViewDetBitacora();

            try
            {
                using (var context = new SIGINECContext())
                {
                    detBitacora = (from b in context.Bitacora
                                   where b.Id_Bitacora == id
                                   select new ViewDetBitacora
                                   {
                                       Id_Bitacora = b.Id_Bitacora,
                                       Dispositivo = b.Dispositivo.Nombre,
                                       Estado = b.Estado_Dispositivo.Estado,
                                       Detalles_Revision = b.Detalles_Revision,
                                       Observaciones = b.Observaciones,
                                       Usuario_Registra = b.Usuario.Persona.Nombre_1 + " " + b.Usuario.Persona.Apellido_1,
                                       Fecha_Registra = b.Fecha_Registro
                                   }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return detBitacora;
        }
    }
}
