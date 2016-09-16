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
                        string q = "select s.Id_Solicitud as IdSolicitud, s.Observaciones, (select estado_op from Estados_Op where id_Estado = s.Estado_Solicitud) as Estado, " +
                                   "(select nombres_mostrar from persona where id_persona = (select Id_Persona from usuario where Id_Usuario = s.Usuario_SolBajoStock)) as UsuarioSol, " +
                                   "s.Fecha_Solicitud as FechaSol, (select nombres_mostrar from persona where id_persona = (select Id_Persona from usuario where Id_Usuario = s.Usuario_Responsable)) as UsuarioResp " +
                                   "from Solicitud_BajoStock as s " +
                                   "where s.Estado_Solicitud = 1 ";

                        var query = context.Database.SqlQuery<ViewSolicitudBajoStock>(q).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();

                        solBS.Data = query;

                    }
                    else
                    {
                        string q = "select s.Id_Solicitud as IdSolicitud, s.Observaciones, (select estado_op from Estados_Op where id_Estado = s.Estado_Solicitud) as Estado, " +
                                   "(select nombres_mostrar from persona where id_persona = (select Id_Persona from usuario where Id_Usuario = s.Usuario_SolBajoStock)) as UsuarioSol, " +
                                   "s.Fecha_Solicitud as FechaSol, (select nombres_mostrar from persona where id_persona = (select Id_Persona from usuario where Id_Usuario = s.Usuario_Responsable)) as UsuarioResp " +
                                   "from Solicitud_BajoStock as s " +
                                   "where s.Estado_Solicitud = 1 ";

                        var query = context.Database.SqlQuery<ViewSolicitudBajoStock>(q).Take(PageSize).ToList();

                        solBS.Data = query;

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
                    string q = "select s.Id_Solicitud as IdSolicitud, s.Observaciones, (select estado_op from Estados_Op where id_Estado = s.Estado_Solicitud) as Estado, " +
                                   "(select nombres_mostrar from persona where id_persona = (select Id_Persona from usuario where Id_Usuario = s.Usuario_SolBajoStock)) as UsuarioSol, " +
                                   "(select nombres_mostrar from persona where id_persona = (select Id_Persona from usuario where Id_Usuario = s.Usuario_Responsable)) as UsuarioResp, s.Fecha_Solicitud as FechaSolicitud " +
                                   "from Solicitud_BajoStock as s " +
                                   "where s.Estado_Solicitud = 1 and s.Id_Solicitud = "+ id;

                    vSeguimientoBS = (context.Database.SqlQuery<ViewSeguimientoBS>(q)).FirstOrDefault();

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

        public void CerrarSolicitudDispositivoBS(int id)
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    var Solicitud = (from sol in context.Solicitud_BajoStock
                                     where sol.Id_Solicitud == id
                                     select sol).First();

                    Solicitud.Estado_Solicitud = 2;
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
