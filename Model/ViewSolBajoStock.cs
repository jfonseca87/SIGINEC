using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ViewSolBajoStock
    {
        
        public int? Id_Dispositivo { get; set; }

        public string Dispositivo { get; set; }

        public int? CantidadDisp { get; set; }

        [Required(ErrorMessage="Debe ingresar almenos una obervación")]
        public string Observaciones { get; set; }
    }
}
