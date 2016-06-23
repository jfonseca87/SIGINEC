using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Helpers
{
    class RecortaTexto
    {
        int CantMax = 0;
        string Texto = "";

        public string retornaTexto(string texto)
        {
            if (CantMax > 227)
            {
                Texto = texto.Substring(1, 227) + "...";
            }
            else
            {
                Texto = texto;
            }
            return Texto;
        }
    }
}
