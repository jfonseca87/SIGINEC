using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using Model.Helpers;

namespace Model
{
    public partial class SesionUsuario
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string Perfil { get; set; }
    }
}
