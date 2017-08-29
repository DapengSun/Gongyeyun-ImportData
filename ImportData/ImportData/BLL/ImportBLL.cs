using ImportData.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportData.BLL
{
    public class ImportBLL
    {
        ImportDAL _dal = new ImportDAL();
        public static string BaseConfigConnection = ConfigurationManager.ConnectionStrings["BaseConfigConnection"].ToString();


        public bool ImportIndustryType(DataTable TypeDt) {
            StringBuilder SQLString = new StringBuilder();
            string Item = string.Empty;
            string SQL = "insert into IndustryAssociated(EnterpristIndustry,CDate,Sysflag) values('{0}',Now(),0);";

            if (TypeDt.Rows.Count > 0)
            {
                foreach(DataRow Row in TypeDt.Rows) {
                    Item = string.Format(SQL, Row.ItemArray[0]);
                    SQLString.Append(Item);
                }
            }

            return _dal.ImportIndustryType(SQLString.ToString());
        }
    }
}
