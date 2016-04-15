namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Menu1
    {
        public Menu1()
        {
            Menu2 = new List<Menu2>();
        }

        [Key]
        public int id_Menu1 { get; set; }

        [Required]
        [StringLength(50)]
        public string pagina { get; set; }

        public int? activa { get; set; }

        public virtual ICollection<Menu2> Menu2 { get; set; }

        public List<Menu1> listaMenu1()
        {
            List<Menu1> lstMenu1 = new List<Menu1>();

            try 
            {
                using (var context = new SIGINECContext())
                {
                    lstMenu1 = context.Menu1.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstMenu1;
        }
    }
}
