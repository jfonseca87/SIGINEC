namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detalle_Solicitud_BajoStock
    {
        [Key]
        public int Id_Detalle { get; set; }

        public int? Id_Dispositivo { get; set; }

        public int? Cantidad { get; set; }

        public int? Id_Solicitud_Stock { get; set; }

        public Dispositivo Dispositivo { get; set; }

        public Solicitud_BajoStock Solicitud_BajoStock { get; set; }

        public void guardaDetalle()
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
    }
}
