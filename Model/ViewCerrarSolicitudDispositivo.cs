using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public partial class ViewCerrarSolicitudDispositivo
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Defina si se soluciona o no la solicitud")]
        public int ResuelveSolicitud { get; set; }

        [Required(ErrorMessage="Debe ingresar las observaciones para cerrar la solicitud")]
        public string Observaciones { get; set; }
    }
}
