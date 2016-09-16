using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SolDispMes
    {
        public string Mes { get; set; }
        public int cantSolDisp { get; set; }
        public int solBajoStock { get; set; }
        public int cantBitacoras { get; set; }

        public List<SolDispMes> infSolDispMes()
        {
            List<SolDispMes> lstSolicitudes = new List<SolDispMes>();
            string query = "select DATEname(MONTH,Fecha_Solicitud) as Mes, count(*) as cantSolDisp from Solicitud_Dispositivo " +
                       "where DATEPART(YEAR, Fecha_Solicitud) = 2016 " +
                       "group by DATEname(MONTH,Fecha_Solicitud), DATEPART(MONTH,Fecha_Solicitud)";

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstSolicitudes = context.Database.SqlQuery<SolDispMes>(query).ToList();
                }
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }

            return lstSolicitudes;
        }

        public List<SolDispMes> infSolBajoStockMes()
        {
            List<SolDispMes> lstBajoStock = new List<SolDispMes>();
            string query = "select DATEname(MONTH,Fecha_Solicitud) as Mes, count(*) as solBajoStock " +
                           "from Solicitud_BajoStock where DATEPART(YEAR, Fecha_Solicitud) = 2016 group by DATEname(MONTH,Fecha_Solicitud), " +
                           "DATEPART(MONTH,Fecha_Solicitud)";

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstBajoStock = context.Database.SqlQuery<SolDispMes>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstBajoStock;
        }

        public List<SolDispMes> infBitacoras()
        {
            List<SolDispMes> lstBitacoras = new List<SolDispMes>();
            string query = "select DATEname(MONTH,Fecha_Registro) as Mes, count(*) as cantBitacoras " +
                           "from Bitacora where DATEPART(YEAR, Fecha_Registro) = 2016 " +
                           "group by DATEname(MONTH,Fecha_Registro), DATEPART(MONTH,Fecha_Registro)";

            try
            {
                using (var context = new SIGINECContext())
                {
                    lstBitacoras = context.Database.SqlQuery<SolDispMes>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstBitacoras;
        }
    }
}
