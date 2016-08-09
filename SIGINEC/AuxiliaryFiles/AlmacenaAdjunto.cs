using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace SIGINEC.AuxiliaryFiles
{
    public class AlmacenaAdjunto
    {
        public void saveFile(int id, HttpPostedFileBase file)
        {

            string dirEspecifico = id.ToString();
            string ruta = "";
            string archivo = "";

            //Crea el directorio relativo donde se almacenaran todas las imagenes
            if (!Directory.Exists("E:/SIGINEC"))
            {
                Directory.CreateDirectory("E:/SIGINEC");
            }

            //Crea el directorio especifico donde se almancenaran las imagenes
            if (!Directory.Exists("E:/SIGINEC/" + dirEspecifico))
            {
                Directory.CreateDirectory("E:/SIGINEC/" + dirEspecifico);
            }

            ruta = "E:/SIGINEC/" + dirEspecifico;
            archivo = ruta + "/" + file.FileName;

            if (!Directory.Exists(archivo))
            {
                file.SaveAs(archivo);
            }
        }
    }
}
