namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Estados_Op
    {
        public Estados_Op()
        {
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
            Solicitud_BajoStock = new List<Solicitud_BajoStock>();
        }

        [Key]
        public int id_Estado { get; set; }

        [StringLength(15)]
        public string Estado_Op { get; set; }

        public int? Activo { get; set; }

        public ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }

        public ICollection<Solicitud_BajoStock> Solicitud_BajoStock { get; set; }

        public List<Estados_Op> listarEstados()
        {
            List<Estados_Op> lstEstados = new List<Estados_Op>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstEstados = context.Estados_Op.Where(e => e.Activo == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstEstados;
        }
    }
}
