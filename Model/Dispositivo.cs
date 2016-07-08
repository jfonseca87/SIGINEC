namespace Model
{
    using System;
    using Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Dispositivo")]
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            Bitacora = new List<Bitacora>();
            Detalle_Solicitud_BajoStock = new List<Detalle_Solicitud_BajoStock>();
            Ingreso_Dispositivo = new List<Ingreso_Dispositivo>();
            Solicitud_Dispositivo = new List<Solicitud_Dispositivo>();
        }

        [Key]
        public int Id_Dispositivo { get; set; }

        [Required(ErrorMessage="El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La clase es requerida")]
        [StringLength(100)]
        public string Clase { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(100)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido")]
        [StringLength(100)]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese caracteristicas")]
        public string Caracteristicas { get; set; }

        [Required(ErrorMessage = "Es necesario que agregue almenos una configuración")]
        public string Configuracion { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int? Cantidad { get; set; }

        public ICollection<Bitacora> Bitacora { get; set; }

        public ICollection<Detalle_Solicitud_BajoStock> Detalle_Solicitud_BajoStock { get; set; }

        public ICollection<Ingreso_Dispositivo> Ingreso_Dispositivo { get; set; }

        public ICollection<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }

        public PagedData<Dispositivo> listarDispositivo(int PageSize, int CurrentPage)
        {
            var disp = new PagedData<Dispositivo>();

            using (var context = new SIGINECContext())
            {
                try
                {
                    if (CurrentPage > 1)
                    {
                        disp.Data = context.Dispositivo.OrderBy(d => d.Id_Dispositivo).Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
                    }
                    else
                    {
                        disp.Data = context.Dispositivo.OrderBy(d => d.Id_Dispositivo).Take(PageSize).ToList();
                    }
                    disp.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Dispositivo.Count() / PageSize));
                    disp.CurrentPage = CurrentPage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return disp;
        }

        public List<Dispositivo> listarDispositivoDropDown()
        {
            List<Dispositivo> lstDispositivos = new List<Dispositivo>();

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstDispositivos = context.Dispositivo.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstDispositivos;

        }

        public Dispositivo ConsultaDispositivo(int id)
        {
            Dispositivo dispositivo = new Dispositivo();

            try
            {
                using (var context = new SIGINECContext())
                {
                    dispositivo = context.Dispositivo.Find(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dispositivo;
        }

        public void guardarDispositivo(int id)
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    if (id == 0)
                    {
                        context.Entry(this).State = System.Data.Entity.EntityState.Added;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DescontarUnidades(int id, int cantDescontar)
        {
            try
            {
                using (var context = new SIGINECContext())
                {
                    var Dispositivo = (from disp in context.Dispositivo
                                       where disp.Id_Dispositivo == id
                                       select disp).First();

                    Dispositivo.Cantidad -= cantDescontar;
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
