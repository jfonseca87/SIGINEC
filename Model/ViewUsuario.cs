using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ViewUsuario
    {
        [Required(ErrorMessage="Debe seleccionar una persona")]
        public int Persona { get; set; }

        [Required(ErrorMessage = "Debe ingresar un usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de usuario")]
        public string Tipo_Usuario { get; set; }
    }
}
