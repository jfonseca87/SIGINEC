using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewListBitacora
    {
        public int Id_Bitacora { get; set; }
        public string Nom_Dispositivo { get; set; }
        public string Estado_Dispositivo { get; set; }
        public string Usuario_Registra { get; set; }
        public DateTime? Fecha_Registro { get; set; }
    }
}
