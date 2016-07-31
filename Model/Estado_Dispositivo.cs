namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Estado_Dispositivo
    {
        public Estado_Dispositivo()
        {
            Bitacora = new List<Bitacora>();
        }

        [Key]
        public int id_Estado { get; set; }

        [Required]
        [StringLength(15)]
        public string Estado { get; set; }

        public int? Activo { get; set; }

        public ICollection<Bitacora> Bitacora { get; set; }

        public List<Estado_Dispositivo> ListarEstadosDropDown()
        {
            List<Estado_Dispositivo> lstEstadosDispositivo = new List<Estado_Dispositivo>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstEstadosDispositivo = context.Estado_Dispositivo.Where(est => est.Activo == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstEstadosDispositivo;
        }
    }
}
