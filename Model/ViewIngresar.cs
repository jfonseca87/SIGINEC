using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Model.Helpers;

namespace Model
{
    public partial class ViewIngresar
    {
        [Required(ErrorMessage="El usuario es requerido")]
        public string User { get; set; }

        [Required(ErrorMessage="La contraseña es requerida")]
        public string Password { get; set; }
    }
}
