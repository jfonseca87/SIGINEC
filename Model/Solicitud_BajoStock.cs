namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public void guardarSolicitud()
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    context.Entry(this).State = System.Data.Entity.EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
