namespace Model
{
    using System;
    using Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Seguimiento_BajoStock
    {
        [Key]
        public int Id_Seguimiento { get; set; }

        [Required(ErrorMessage="Es necesario que realice un seguimiento")]
        public string Seguimiento { get; set; }

        public int Usuario_Seguimiento { get; set; }

        public DateTime? Fecha_Seguimiento { get; set; }

        public int? Id_Solicitud { get; set; }

        public Solicitud_BajoStock Solicitud_BajoStock { get; set; }

        public Usuario Usuario { get; set; }

        public void crearSeguimientoBS()
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedData<ViewDetSeguimientoBS> ListarSeguimientosBS(int id, int PageSize, int CurrentPage)
        {
            var segBS = new PagedData<ViewDetSeguimientoBS>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        segBS.Data = (from det in context.Seguimiento_BajoStock
                                    orderby det.Id_Seguimiento descending
                                    where det.Id_Solicitud == id
                                    select new ViewDetSeguimientoBS
                                    {
                                        IdSeguimiento = det.Id_Seguimiento,
                                        UsuarioSeguimiento = det.Usuario.Persona.Nombre_1 + " " + det.Usuario.Persona.Apellido_1,
                                        Seguimiento = det.Seguimiento,
                                        FechaSeguimiento = det.Fecha_Seguimiento
                                    }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
                    }
                    else
                    {
                        segBS.Data = (from det in context.Seguimiento_BajoStock
                                    orderby det.Id_Seguimiento descending
                                    where det.Id_Solicitud == id
                                    select new ViewDetSeguimientoBS
                                    {
                                        IdSeguimiento = det.Id_Seguimiento,
                                        UsuarioSeguimiento = det.Usuario.Persona.Nombre_1 +" "+ det.Usuario.Persona.Apellido_1,
                                        Seguimiento = det.Seguimiento,
                                        FechaSeguimiento = det.Fecha_Seguimiento
                                    }).Take(PageSize).ToList();
                    }

                    segBS.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Seguimiento_BajoStock.Where(det => det.Id_Solicitud == id).Count() / PageSize));
                    segBS.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return segBS;
        }
    }
}
