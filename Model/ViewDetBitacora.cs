using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewDetBitacora
    {
        public int Id_Bitacora { get; set; }
        public string Dispositivo { get; set; }
        public string Estado { get; set; }
        public string Detalles_Revision { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Registra { get; set; }
        public DateTime? Fecha_Registra { get; set; }
    }
}
