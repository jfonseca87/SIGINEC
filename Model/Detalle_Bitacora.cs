namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Detalle_Bitacora
    {
        [Key]
        public int Id_Detalle { get; set; }

        public string Fotografia { get; set; }

        public string Observacion_Foto { get; set; }

        public int? Id_Bitacora { get; set; }

        public Bitacora Bitacora { get; set; }

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

        public List<Detalle_Bitacora> traeImagenes(int id)
        {
            List<Detalle_Bitacora> lstImagenes = new List<Detalle_Bitacora>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstImagenes = context.Detalle_Bitacora.Where(det => det.Id_Bitacora == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstImagenes;
        }
    }
}
