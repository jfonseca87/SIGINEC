using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ViewSumarCantidades
    {
        public int Id_Dispositvo { get; set; }
        public string Nom_Dispositivo { get; set; }

        [Required(ErrorMessage="Es necesario ingresar una cantidad")]
        public int cantSumar { get; set; }
    }
}
