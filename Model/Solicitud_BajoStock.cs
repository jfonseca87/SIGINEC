namespace Model
{
    using System;
    using Model.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Solicitud_BajoStock
    {
        public Solicitud_BajoStock()
        {
            Detalle_Solicitud_BajoStock = new List<Detalle_Solicitud_BajoStock>();
            Seguimiento_BajoStock = new List<Seguimiento_BajoStock>();
        }

        [Key]
        public int Id_Solicitud { get; set; }

        public string Observaciones { get; set; }

        public int? Estado_Solicitud { get; set; }

        public int? Usuario_SolBajoStock { get; set; }

        public DateTime? Fecha_Solicitud { get; set; }

        public int? Usuario_Responsable { get; set; }

        public ICollection<Detalle_Solicitud_BajoStock> Detalle_Solicitud_BajoStock { get; set; }

        public Estados_Op Estados_Op { get; set; }

        public ICollection<Seguimiento_BajoStock> Seguimiento_BajoStock { get; set; }

        public Usuario Usuario { get; set; }

        public PagedData<ViewSolicitudBajoStock> listarSolicitudesBS(int PageSize, int CurrentPage)
        {
            var solBS = new PagedData<ViewSolicitudBajoStock>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        solBS.Data = (from s in context.Solicitud_BajoStock
                                    where s.Estado_Solicitud == 1
                                    orderby s.Id_Solicitud
                                    select new ViewSolicitudBajoStock
                                    {
                                        IdSolicitud = s.Id_Solicitud,
                                        Observaciones = s.Observaciones,
                                        Estado = s.Estados_Op.Estado_Op,
                                        UsuarioSol = s.Usuario.Persona.Nombre_1 + " " + s.Usuario.Persona.Apellido_2,
                                        FechaSol = s.Fecha_Solicitud,
                                        UsuarioResp = s.Usuario.Persona.Nombre_1 + " " + s.Usuario.Persona.Apellido_2,
                                        
                                    }).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                    }
                    else
                    {
                        solBS.Data = (from s in context.Solicitud_BajoStock
                                      where s.Estado_Solicitud == 1
                                      orderby s.Id_Solicitud
                                      select new ViewSolicitudBajoStock
                                      {
                                          IdSolicitud = s.Id_Solicitud,
                                          Observaciones = s.Observaciones,
                                          Estado = s.Estados_Op.Estado_Op,
                                          UsuarioSol = s.Usuario.Persona.Nombre_1 + " " + s.Usuario.Persona.Apellido_2,
                                          FechaSol = s.Fecha_Solicitud,
                                          UsuarioResp = s.Usuario.Persona.Nombre_1 + " " + s.Usuario.Persona.Apellido_2,

                                      }).Take(PageSize).ToList();

                    }

                    solBS.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Solicitud_BajoStock.Where(s => s.Estado_Solicitud == 1).Count() / PageSize));
                    solBS.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return solBS;
        }

        public void guardarSolicitud()
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

        public int retornaIdSol(int idUsuario)
        {
            int idSolicitud = 0;
            try 
            {
                using (var context = new SIGINECContext())
                {
                    idSolicitud = (from sol in context.Solicitud_BajoStock
                                   where sol.Usuario_SolBajoStock == idUsuario && sol.Estado_Solicitud == 1
                                   orderby sol.Id_Solicitud descending
                                   select sol.Id_Solicitud).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return idSolicitud;
        }

        public ViewSeguimientoBS detalleSolBS(int id)
        {
            ViewSeguimientoBS vSeguimientoBS = new ViewSeguimientoBS();
            List<DetalleDispositivo> lstDispositivos = new List<DetalleDispositivo>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    vSeguimientoBS = (from vs in context.Solicitud_BajoStock
                                      where vs.Id_Solicitud == id && vs.Estado_Solicitud == 1
                                      select new ViewSeguimientoBS
                                      {
                                          IdSolicitud = vs.Id_Solicitud,
                                          Observaciones = vs.Observaciones,
                                          Estado = vs.Estados_Op.Estado_Op,
                                          UsuarioSol = vs.Usuario.Persona.Nombre_1 + " " + vs.Usuario.Persona.Apellido_2,
                                          UsuarioResp = vs.Usuario.Persona.Nombre_1 + " " + vs.Usuario.Persona.Apellido_2,
                                          FechaSolicitud = vs.Fecha_Solicitud
                                      }).FirstOrDefault();

                    lstDispositivos = (from ls in context.Detalle_Solicitud_BajoStock
                                       where ls.Id_Solicitud_Stock == id
                                       select new DetalleDispositivo
                                       {
                                           Id_Dispositivo = ls.Id_Dispositivo,
                                           Dispositivo = ls.Dispositivo.Nombre,
                                           Cantidad = ls.Cantidad
                                       }).ToList();

                    vSeguimientoBS.lstDetalleSolBS = lstDispositivos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vSeguimientoBS;
        }
    }
}
