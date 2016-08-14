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
        public string saveFile(int id, HttpPostedFileBase file)
        {
            string dirRelativo = HttpContext.Current.Server.MapPath("~/BitacoraImagenes");
            string dirEspecifico = id.ToString();
            string ruta = "";
            string archivo = "";

            //Crea el directorio relativo donde se almacenaran todas las imagenes
            if (!Directory.Exists(dirRelativo))
            {
                Directory.CreateDirectory(dirRelativo);
            }

            //Crea el directorio especifico donde se almancenaran las imagenes
            if (!Directory.Exists(dirRelativo +"/"+ dirEspecifico))
            {
                Directory.CreateDirectory(dirRelativo + "/" + dirEspecifico);
            }

            ruta = dirRelativo + "/" + dirEspecifico;
            archivo = ruta + "/" + file.FileName;

            if (!Directory.Exists(archivo))
            {
                file.SaveAs(archivo);
            }

            return archivo;
        }
    }
}
