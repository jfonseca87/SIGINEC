using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    class ViewSolicitudBajoStock
    {
        [Required(ErrorMessage="Debe seleccionar un Dispositivo")]
        public int Id_Dispositivo { get; set; }

        public string Nom_Dispositivo { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Debe especificar almenos una observación")]
        public string Observaciones { get; set; }
    }
}
