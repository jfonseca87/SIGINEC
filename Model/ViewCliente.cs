using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ViewCliente
    {
        [Required(ErrorMessage="Debe seleccionar una persona")]
        public int Persona { get; set; }

        [Required(ErrorMessage="Debe indicar una dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage="Debe indicar un teléfono")]
        public string Telefono { get; set; }
    }
}
