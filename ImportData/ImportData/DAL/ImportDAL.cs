using Caxa.Project.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportData.DAL
{
    public class ImportDAL
    {
        //配置连接串
        public static string BaseConfigConnection = ConfigurationManager.ConnectionStrings["BaseConfigConnection"].ToString();
        public static string AccountConnection = ConfigurationManager.ConnectionStrings["AccountConnection"].ToString();
        public static string ChangzhouConnection = ConfigurationManager.ConnectionStrings["ChangzhouConnection"].ToString();
        public static string CompanyConnection = ConfigurationManager.ConnectionStrings["CompanyConnection"].ToString();
        
        /// <summary>
        /// 导入报表中工业分类
        /// </summary>
        public bool ImportIndustryType(string SQLString) {
            if (DbHelperMySQL.ExecuteSql(BaseConfigConnection,SQLString) < 0) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据行业分类名称 通过IndustryAssociated表 查找行业分类ID及根ID
        /// </summary>
        /// <param name="IndustryName"></param>
        /// <param name="IndustryID"></param>
        /// <param name="IndustryRootID"></param>
        public void GetImportDataType(string SQLString, out string IndustryID, out string IndustryRootID)
        {
            IndustryID = string.Empty;
            IndustryRootID = string.Empty;

            DataSet Ds = DbHelperMySQL.Query(BaseConfigConnection, SQLString);
            if (Ds.Tables[0].Rows.Count > 0) {
                IndustryID = Ds.Tables[0].Rows[0]["GongyeyunIndustryId"].ToString();
                IndustryRootID = Ds.Tables[0].Rows[0]["GongyeyunIndustryRootId"].ToString();
            }
        }

        /// <summary> 
        /// 根据企业名称 获取企业ID
        /// </summary>
        /// <param name="IndustryName"></param>
        /// <param name="IndustryID"></param>
        /// <param name="IndustryRootID"></param>
        public string GetEnterpriseAccountID(string EnterpriseName)
        {
            string EnterpriseID = string.Empty;
            string SQL = "select * from accountinfoes where NickName like '%{0}%'";
            SQL = string.Format(SQL, EnterpriseName.Trim());
            return DbHelperMySQL.GetSingle(AccountConnection, SQL).ToString();
        }

        /// <summary> 
        /// 根据企业名称 获取企业ID
        /// </summary>
        /// <param name="IndustryName"></param>
        /// <param name="IndustryID"></param>
        /// <param name="IndustryRootID"></param>
        public string GetEnterpriseInfoID(string EnterpriseName)
        {
            string EnterpriseID = string.Empty;
            string SQL = "select * from companyinfoes where Name = '{0}'";
            SQL = string.Format(SQL, EnterpriseName.Trim());
            return DbHelperMySQL.GetSingle(CompanyConnection, SQL).ToString();
        }

        /// <summary> 
        /// 根据ID 获取软件ID
        /// </summary>
        public string GetSoftwareID(string Id)
        {
            string EnterpriseID = string.Empty;
            string SQL = "select Id from dicsoftwares where _id = '{0}'";
            SQL = string.Format(SQL, Id);
            return DbHelperMySQL.GetSingle(ChangzhouConnection, SQL).ToString();
        }

        /// <summary> 
        /// 根据ID 获取平台ID
        /// </summary>
        public string GetPlatApplyID(string Id)
        {
            string SQL = "select Id from dictcloudplats where _id = '{0}'";
            SQL = string.Format(SQL, Id);
            return DbHelperMySQL.GetSingle(ChangzhouConnection, SQL).ToString();
        }

        /// <summary> 
        /// 根据ID 获取电商ID
        /// </summary>
        public string GetBusinessID(string Id)
        {
            string SQL = "select Id from dictebusinesses where _id = '{0}'";
            SQL = string.Format(SQL, Id);
            return DbHelperMySQL.GetSingle(ChangzhouConnection, SQL).ToString();
        }

        /// <summary> 
        /// 根据ID 获取单项应用ID
        /// </summary>
        public string GetApplicationID(string Id)
        {
            string SQL = "select Id from dictapplications where _id = '{0}'";
            SQL = string.Format(SQL, Id);
            return DbHelperMySQL.GetSingle(ChangzhouConnection, SQL).ToString();
        }
    }
}
