using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ViewCambioPassword
    {
        public int Id_Usuario { get; set; }
        public string Nombres { get; set; }

        [Required(ErrorMessage="Debe ingresar una contraseña")]
        public string Nuevo_Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [Compare("Nuevo_Password", ErrorMessage="No concuerdan las contraseñas")]
        public string Confirma_Password { get; set; }
    }
}
