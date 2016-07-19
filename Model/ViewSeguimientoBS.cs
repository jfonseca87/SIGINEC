using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewSeguimientoBS
    {
        public int IdSolicitud { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public string UsuarioSol { get; set; }
        public string UsuarioResp { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public List<DetalleDispositivo> lstDetalleSolBS { get; set; }
    }
}
