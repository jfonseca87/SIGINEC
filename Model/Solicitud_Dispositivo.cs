namespace Model
{
    using System;
    using Model.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Solicitud_Dispositivo
    {
        public Solicitud_Dispositivo()
        {
            Seguimiento_SolDispositivo = new List<Seguimiento_SolDispositivo>();
        }

        [Key]
        public int Id_Solicitud { get; set; }

        public string Observaciones { get; set; }

        public int? Estado_Solicitud { get; set; }

        public int? Id_Dispositivo { get; set; }

        public int? Cantidad { get; set; }

        public int? Id_Cliente { get; set; }

        public int? Usuario_SolDispositivo { get; set; }

        public DateTime? Fecha_Solicitud { get; set; }

        public Cliente Cliente { get; set; }

        public Dispositivo Dispositivo { get; set; }

        public Estados_Op Estados_Op { get; set; }

        public ICollection<Seguimiento_SolDispositivo> Seguimiento_SolDispositivo { get; set; }

        public Usuario Usuario { get; set; }

        public PagedData<ViewSolicitudDispositivo> listarSolicitudes(int PageSize, int CurrentPage)
        {
            var sol = new PagedData<ViewSolicitudDispositivo>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        sol.Data = (from s in context.Solicitud_Dispositivo
                                    orderby s.Id_Solicitud
                                    select new ViewSolicitudDispositivo
                                    {
                                        IdSolicitud = s.Id_Solicitud,
                                        Estado = s.Estados_Op.Estado_Op,
                                        Dispositivo = s.Dispositivo.Nombre,
                                        Cantidad = s.Cantidad,
                                        Cliente = s.Cliente.Persona.Nombre_1 + ' ' + s.Cliente.Persona.Apellido_1,
                                        FSolicitud = s.Fecha_Solicitud
                                    }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                    }
                    else 
                    {
                        sol.Data = (from s in context.Solicitud_Dispositivo
                                    orderby s.Id_Solicitud
                                    select new ViewSolicitudDispositivo
                                    {
                                        IdSolicitud = s.Id_Solicitud,
                                        Estado = s.Estados_Op.Estado_Op,
                                        Dispositivo = s.Dispositivo.Nombre,
                                        Cantidad = s.Cantidad,
                                        Cliente = s.Cliente.Persona.Nombre_1 + " " + s.Cliente.Persona.Apellido_1,
                                        FSolicitud = s.Fecha_Solicitud
                                    }).Take(PageSize).ToList();

                        //List<ViewSolicitudDispositivo> lstSolicitudes = new List<ViewSolicitudDispositivo>();
                        
                        //foreach( var item in query)

                    }

                    sol.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Solicitud_Dispositivo.Count() / PageSize));
                    sol.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return sol;
        }
    }
}
