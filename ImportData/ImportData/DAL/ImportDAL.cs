using Caxa.Project.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportData.DAL
{
    public class ImportDAL
    {
        /// <summary>
        /// 导入报表中工业分类
        /// </summary>
        public bool ImportIndustryType(string SQLString) {
            if (DbHelperMySQL.ExecuteSql(SQLString) < 0) {
                return false;
            }
            return true;
        }

    }
}
