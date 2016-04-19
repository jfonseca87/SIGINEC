namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ingreso_Dispositivo
    {
        [Key]
        public int Id_Ingreso { get; set; }

        public string Observaciones { get; set; }

        public int? Id_Dispositivo { get; set; }

        public int? Usuario_Registra { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        public Dispositivo Dispositivo { get; set; }

        public Usuario Usuario { get; set; }

        public void guardaRegistro(string observaciones, int idDispositivo, int Usuario)
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    context.Ingreso_Dispositivo.Add(new Ingreso_Dispositivo { 
                        Observaciones = observaciones,
                        Id_Dispositivo = idDispositivo,
                        Usuario_Registra = Usuario,
                        Fecha_Registro = System.DateTime.Now
                    });
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
