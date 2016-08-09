namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bitacora")]
    public partial class Bitacora
    {
        public Bitacora()
        {
            Detalle_Bitacora = new List<Detalle_Bitacora>();
        }

        [Key]
        public int Id_Bitacora { get; set; }

        [Required(ErrorMessage="Debe seleccionar un Dispositivo")]
        public int Id_Dispositivo { get; set; }

        [Required(ErrorMessage="Debe seleccionar un estado")]
        public int Id_Estado_Dispositivo { get; set; }

        [Required(ErrorMessage=("Ingrese los detalles obtenidos en la revisión"))]
        public string Detalles_Revision { get; set; }

        [Required(ErrorMessage = ("Ingrese almenos una observación"))]
        public string Observaciones { get; set; }

        public int? Usuario_Registra { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        public virtual Estado_Dispositivo Estado_Dispositivo { get; set; }

        public virtual Dispositivo Dispositivo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Detalle_Bitacora> Detalle_Bitacora { get; set; }

        public void Guardar() {
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
