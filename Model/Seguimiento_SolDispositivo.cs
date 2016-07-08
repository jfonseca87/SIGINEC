namespace Model
{
    using System;
    using Model.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Seguimiento_SolDispositivo
    {
        [Key]
        public int Id_Seguimiento { get; set; }

        [Required(ErrorMessage="Es necesario que realice un seguimiento")]
        public string Seguimiento { get; set; }

        public int Usuario_Seguimiento { get; set; }

        public DateTime? Fecha_Seguimiento { get; set; }

        public int? Id_SolicitudDisp { get; set; }

        public Solicitud_Dispositivo Solicitud_Dispositivo { get; set; }

        public Usuario Usuario { get; set; }

        public void creaSeguimiento()
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

        public PagedData<ViewDetSeguimiento> ListarSeguimientos(int id, int PageSize, int CurrentPage)
        {
            var seg = new PagedData<ViewDetSeguimiento>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        seg.Data = (from det in context.Seguimiento_SolDispositivo
                                    orderby det.Id_Seguimiento descending
                                    where det.Id_SolicitudDisp == id
                                    select new ViewDetSeguimiento
                                    {
                                        IdSeguimiento = det.Id_Seguimiento,
                                        UsuarioSeguimiento = det.Usuario.Persona.Nombre_1 + " " + det.Usuario.Persona.Apellido_1,
                                        Seguimiento = det.Seguimiento,
                                        FechaSeguimiento = det.Fecha_Seguimiento
                                    }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
                    }
                    else
                    {
                        seg.Data = (from det in context.Seguimiento_SolDispositivo
                                    orderby det.Id_Seguimiento descending
                                    where det.Id_SolicitudDisp == id
                                    select new ViewDetSeguimiento
                                    {
                                        IdSeguimiento = det.Id_Seguimiento,
                                        UsuarioSeguimiento = det.Usuario.Persona.Nombre_1 + " " + det.Usuario.Persona.Apellido_1,
                                        Seguimiento = det.Seguimiento,
                                        FechaSeguimiento = det.Fecha_Seguimiento
                                    }).Take(PageSize).ToList();
                    }

                    seg.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Seguimiento_SolDispositivo.Where(det => det.Id_SolicitudDisp == id).Count() / PageSize));
                    seg.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return seg;
        }
    }
}
