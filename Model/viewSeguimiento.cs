using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class viewSeguimiento
    {
        public int IdSolicitud { get; set; }
        public string Cliente { get; set; }
        public string Dispositivo { get; set; }
        public int? Cantidad { get; set; }
        public string Estado { get; set; }
        public string Solicita { get; set; }
        public DateTime? FSolicitud { get; set; }
    }
}
