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

        /// <summary>
        /// 根据行业分类名称 通过IndustryAssociated表 查找行业分类ID及根ID
        /// </summary>
        /// <param name="IndustryName"></param>
        /// <param name="IndustryID"></param>
        /// <param name="IndustryRootID"></param>
        public void GetImportDataType(string IndustryName, out string IndustryID, out string IndustryRootID) {
            string SQL = "select * from IndustryAssociated where EnterpristIndustry like '%{0}%'";
            _dal.GetImportDataType(string.Format(SQL,IndustryName),out IndustryID, out IndustryRootID);
        }

        /// <summary> 
        /// 根据企业名称 获取企业ID
        /// </summary>
        /// <param name="IndustryName"></param>
        /// <param name="IndustryID"></param>
        /// <param name="IndustryRootID"></param>
        public string GetEnterpriseAccountID(string EnterpriseName)
        {
            return _dal.GetEnterpriseAccountID(EnterpriseName);
        }

        /// <summary> 
        /// 根据企业名称 获取企业ID
        /// </summary>
        /// <param name="IndustryName"></param>
        /// <param name="IndustryID"></param>
        /// <param name="IndustryRootID"></param>
        public string GetEnterpriseInfoID(string EnterpriseName)
        {
            return _dal.GetEnterpriseInfoID(EnterpriseName);
        }

        /// <summary> 
        /// 根据ID 获取软件ID
        /// </summary>
        public string GetSoftwareID(string Id)
        {
            return _dal.GetSoftwareID(Id);
        }

        /// <summary> 
        /// 根据ID 获取平台ID
        /// </summary>
        public string GetPlatApplyID(string Id)
        {
            return _dal.GetPlatApplyID(Id);
        }

        /// <summary> 
        /// 根据ID 获取电商ID
        /// </summary>
        public string GetBusinessID(string Id)
        {
            return _dal.GetBusinessID(Id);
        }

        /// <summary> 
        /// 根据ID 获取电商ID
        /// </summary>
        public string GetApplicationID(string Id)
        {
            return _dal.GetApplicationID(Id);
        }
    }
}
