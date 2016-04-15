namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Menu2
    {
        [Key]
        public int id_Menu2 { get; set; }

        [Required]
        [StringLength(50)]
        public string subPagina { get; set; }

        public int? activa { get; set; }

        public int? id_Menu1 { get; set; }

        public virtual Menu1 Menu1 { get; set; }

        public List<Menu2> listarMenu2(int id)
        {
            List<Menu2> lstMenu2 = new List<Menu2>();

            using (var context = new SIGINECContext())
            {
                var query = (from m in context.Menu2
                             where m.activa == 1 && m.id_Menu1 == id
                             select m).ToList();

                foreach (var item in query)
                {
                    lstMenu2.Add(new Menu2 {
                        id_Menu2 = item.id_Menu2,
                        subPagina = item.subPagina,
                        activa = item.activa,
                        id_Menu1 = item.id_Menu1
                    });
                }
            }

            return lstMenu2;
        }
    }
}
