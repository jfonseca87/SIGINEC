using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewDetSeguimiento
    {
        public int IdSeguimiento { get; set; }
        public string UsuarioSeguimiento { get; set; }
        public string Seguimiento { get; set; }
        public DateTime? FechaSeguimiento { get; set; }
    }
}
