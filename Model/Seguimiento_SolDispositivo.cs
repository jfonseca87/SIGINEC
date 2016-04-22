namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
    }
}
