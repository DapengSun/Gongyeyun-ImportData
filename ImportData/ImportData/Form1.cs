using ExcelToDb;
using ImportData.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportData
{
    public partial class Form1 : Form
    {
        ImportBLL _bll = new ImportBLL();
        DataTable ImportDt = new DataTable();
        DataTable InsturyTypeDt = new DataTable();


        public Form1()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 将SQL写入TextBox
        /// </summary>
        public void WriteSQL(string SQLString) {
            if (InvokeRequired) {
                this.ShowSQL.BeginInvoke(new MethodInvoker(delegate(){
                    this.ShowSQL.AppendText(SQLString);
                    this.ShowSQL.AppendText("\r\n");
                }));
            }
            this.ShowSQL.AppendText(SQLString);
            this.ShowSQL.AppendText("\r\n");
        }

        public void ClearSQLTextBox() {
            if (InvokeRequired)
            {
                this.ShowSQL.BeginInvoke(new MethodInvoker(delegate () {
                    this.ShowSQL.Clear();
                }));
            }
            this.ShowSQL.Clear();
        }

        /// <summary>
        /// 导入报表分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportIndustryType_Click(object sender, EventArgs e)
        {
            try
            {
                GetIndustryType(5);
                _bll.ImportIndustryType(InsturyTypeDt);
                WriteSQL("报表行业分类已导入完成！ \r\n");
            }
            catch (Exception ex)
            {
                WriteSQL("报表行业分类导入异常！ \r\n");
                throw ex;
            }
        }

        public void GetIndustryType(int index = 5) {
            InsturyTypeDt = ImportDt.DefaultView.ToTable(true,ImportDt.Columns[index].ColumnName.ToString());
        }

        public void GetImportData(object sender, EventArgs e) {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "所有文件|*.*";
                fileDialog.Multiselect = true;
                fileDialog.FilterIndex = 1;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportDt = ExcelHelper.ExcelToDataTable(fileDialog.FileName, "0", true);
                    WriteSQL("【" + fileDialog.FileName + "】已导入完成！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生成企业账户SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterpriseAccountData_Click(object sender, EventArgs e)
        {
            int EnterpriseNameIndex = 1;
            int StartIndex = 865;
            int PlatCode = 47;
            string SQL = string.Empty;
            string LoginName = string.Empty;

            ClearSQLTextBox();
            for (int i = 0; i < ImportDt.Rows.Count;i++ )
            {
                LoginName = "cz" + (StartIndex + i).ToString();
                SQL = EnterpriseAccountDataSQL(LoginName, ImportDt.Rows[i][EnterpriseNameIndex].ToString().Trim(), PlatCode);
                WriteSQL(SQL);
            }
        }

        /// <summary>
        /// 生成企业账户SQL
        /// </summary>
        private string EnterpriseAccountDataSQL(string LoginName, string EnterpriseName,int PlatCode = 47,string Password = "F379EAF3C831B04DE153469D1BEC345E",string ProviceName = "江苏省",string City= "常州市",string Town = "武进区")
        {
            string SQL = "INSERT INTO `gyy_account`.`accountinfoes`(`Id`,`OldCid`,`Role`,`Type`,`LoginName`,`Password`,`NickName`,`Phone`,`Name`,`Sex`,`Email`,`Bday`,`Province`,`City`,`Town`,`Address`, `PostCode`,`PlatCode`,`CDate`,`UpdateDate` ,`Perfinish`,`DeviceFlag`,`LastLoginDate`,`Editnum`,`SysStatus`,`Like`,`Coins`,`LoginNum`,`AllLoginNum`,`AllLastLoginDate`)";
                   SQL += " VALUES(REPLACE(UUID() , '-', ''),0,0,0,'{0}','{1}','{2}','','{2}',1,'','','{3}','{4}','{5}','','',{6},NOW(),NOW(),0,0,NOW(),0,0,0,0,0,0,NOW());";

            return string.Format(SQL, new object[] { LoginName, Password, EnterpriseName,ProviceName, City, Town, PlatCode });
        }

        /// <summary>
        /// 生成企业数据SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterpriseData_Click(object sender, EventArgs e)
        {
            try
            {
                int Platform = 3;
                int PlatCode = 47;
                int IndustryTypeIndex = 5;
                int EnterpriseNameIndex = 1;
                string ImgPath = @"http://deyang.gongyeyun.com/CloudFile/Resouce/2014/1-16//gongfang1.png";
                string ProviceName = "江苏省";
                string City = "常州市";
                string Town = "武进区";

                string IndustryID = string.Empty;
                string IndustryRootID = string.Empty;
                string EnterpriseAccountID = string.Empty;
                string EnterpriseName = string.Empty;
                string SQL = string.Empty;

                ClearSQLTextBox();
                foreach (DataRow item in ImportDt.Rows)
                {
                    //根据行业分类名称 通过IndustryAssociated表 查找行业分类ID及根ID
                    GetImportDataType(item[IndustryTypeIndex].ToString(), out IndustryID, out IndustryRootID);

                    EnterpriseName = item[EnterpriseNameIndex].ToString();
                    //根据企业名称 获取企业账号ID
                    EnterpriseAccountID = _bll.GetEnterpriseAccountID(EnterpriseName);

                    SQL = EnterpriseDataSQL(EnterpriseAccountID, EnterpriseName.Trim(), ProviceName, City, Town, IndustryRootID, string.IsNullOrEmpty(IndustryID) == true ? 0 : int.Parse(IndustryID), Platform, PlatCode, ImgPath);
                    WriteSQL(SQL);
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        /// <summary>
        /// 生成企业数据SQL
        /// </summary>
        private string EnterpriseDataSQL(string EnterpriseAccountID, string EnterpriseName, string ProviceName, string City, string Town, string IndustryRootID, int IndustryID, int Platform, int PlatCode,string ImgPath) {
            string SQL = "INSERT INTO `gyy_company`.`companyinfoes`(`Id`,`OldSid`,`Cid`,`RzType`,`RzCheck`,`Name`,`Province`,`City`,`Town`,`Address`,`Lng`,`Lat`,";
                   SQL+= "`Industry`,`IndustryId`,`IndustryRoot`,`UpdateDate`,`PlatCode`,`CDate`,`SysStatus`,`ScaleType`,`Platform`,`imgpath`,Probar,`MainBusiness`,`CasesNum`,`CollsNum`,`ViewNum`,`LiuyanNum`,`PhotoNum`,`DynamicNum`,`PjNum`,`Sdid`,`ScaleNum`,`TurnOver`,`ScaleSite`,`EmailFlag`,`PhoneFlag`,`IdenFlag`)";
                   SQL+= " VALUES(REPLACE(UUID() , '-', ''),0,'{0}',0,0,'{1}','{2}','{3}','{4}','','','','{5}',{6},'{5}',NOW(),{7},NOW(),0,0,{8},'{9}',0,'',0,0,0,0,0,0,0,0,0,0,0,0,0,0);";

            SQL  = string.Format(SQL, new object[] { EnterpriseAccountID, EnterpriseName, ProviceName, City, Town, IndustryRootID, IndustryID , PlatCode , Platform , ImgPath });
            return SQL;
        }

        /// <summary>
        /// 根据行业分类名称 通过IndustryAssociated表 查找行业分类ID及根ID
        /// </summary>
        private void GetImportDataType(string IndustryName, out string IndustryID,out string IndustryRootID) {
            _bll.GetImportDataType(IndustryName, out IndustryID, out IndustryRootID);
        }

        /// <summary>
        /// Ctrl + A 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        /// <summary>
        /// 生成企业软件应用SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterpriseSoftware_Click(object sender, EventArgs e)
        {
            int EnterpriseNameIndex = 1;
            int SoftwareIndex = 24;
            string SQL = string.Empty;
            string EnterpriseID = string.Empty;
            string SoftwareString = string.Empty;
            string EnterpriseName = string.Empty;

            ClearSQLTextBox();
            foreach (DataRow item in ImportDt.Rows)
            {
                EnterpriseName = item[EnterpriseNameIndex].ToString();
                SoftwareString = item[SoftwareIndex].ToString();
                //根据企业名称 获取企业账号ID
                EnterpriseID = _bll.GetEnterpriseInfoID(EnterpriseName);
                SoftwareRange(EnumType.CharactersEnum.A,EnumType.CharactersEnum.U, EnterpriseID, SoftwareString,"Software");
            }
        }

        //根据列头起始 终止字符区间 及 单元格中字符 生成SQL
        private void SoftwareRange(EnumType.CharactersEnum StartChar, EnumType.CharactersEnum EndChar,string EnterpriseID,string ContentString,string Type) {
            for (int i = (int)StartChar; i <= (int)EndChar; i++) {
                EnumType.CharactersEnum _enum = (EnumType.CharactersEnum)i;
                if (ContentString.Contains(_enum.ToString())) {
                    switch (Type)
                    {
                        case "Software":
                            WriteSQL(SoftwareSQL(EnterpriseID,GetSoftwareID(i.ToString())));
                            continue;
                        case "PlatApply":
                            WriteSQL(PlatApplySQL(EnterpriseID, GetPlatApplyID(i.ToString())));
                            continue; 
                        case "Business":
                            WriteSQL(BusinessSQL(EnterpriseID, GetBusinessID(i.ToString())));
                            continue;
                        case "Application":
                            WriteSQL(ApplicationSQL(EnterpriseID, GetApplicationID(i.ToString())));
                            continue;
                        default: continue;
                    }
                }
            }
        }

        /// 获取企业软件主键ID
        /// </summary>
        private string GetSoftwareID(string Id)
        {
            return _bll.GetSoftwareID(Id);
        }

        /// 生成企业软件应用SQL
        /// </summary>
        private string SoftwareSQL(string EnterpriseID,string SoftwareID)
        {
            string SQL = "INSERT INTO `gyy_changzhou`.`companysoftres`(`Id`,`Sid`,`SoftwareId`,`CDate`,`SysStatus`,`Memo`)VALUES(REPLACE(UUID() , '-', ''),'{0}','{1}',NOW(),0,'');";
            SQL = string.Format(SQL, new object[] { EnterpriseID, SoftwareID });
            return SQL;
        }

        /// <summary>
        /// 生成企业营业额SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void EnterpriseTurnOver_Click(object sender, EventArgs e)
        {
            int EnterpriseNameIndex = 1;
            int TurnOverIndex = 10;
            float TurnOver = 0;
            string SQL = string.Empty;
            string EnterpriseID = string.Empty;
            string EnterpriseName = string.Empty;

            ClearSQLTextBox();
            foreach (DataRow item in ImportDt.Rows)
            {
                EnterpriseName = item[EnterpriseNameIndex].ToString();
                TurnOver = OperTurnOver(item[TurnOverIndex].ToString());
                SQL = TurnOverSQL(EnterpriseName.Trim(), TurnOver);
                WriteSQL(SQL);
            }
        }

        /// <summary>
        /// 处理企业营业额数据
        /// </summary>
        /// <returns></returns>
        private float OperTurnOver(string TurnOverString) {
            float TurnOver = 0;
            TurnOverString = TurnOverString.Replace("万元", "").Trim();
            TurnOver = string.IsNullOrEmpty(TurnOverString) == true ? 0 : float.Parse(TurnOverString);
            return TurnOver;
        }

        /// <summary>
        /// 生成软件营业额SQL
        /// </summary>
        private string TurnOverSQL(string EnterpriseName,float TurnOver)
        {
            string SQL = "Update `gyy_company`.`companyinfoes` Set TurnOver = {0} where Name = '{1}';";
            SQL = string.Format(SQL, new object[] { TurnOver, EnterpriseName });
            return SQL;
        }

        /// <summary>
        /// 生成平台应用SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlatApply_Click(object sender, EventArgs e)
        {
            int EnterpriseNameIndex = 1;
            int PlatApplyIndex = 25;
            string SQL = string.Empty;
            string EnterpriseID = string.Empty;
            string PlatApplyString = string.Empty;
            string EnterpriseName = string.Empty;

            ClearSQLTextBox();
            foreach (DataRow item in ImportDt.Rows)
            {
                EnterpriseName = item[EnterpriseNameIndex].ToString();
                PlatApplyString = item[PlatApplyIndex].ToString();
                //根据企业名称 获取企业账号ID
                EnterpriseID = _bll.GetEnterpriseInfoID(EnterpriseName);
                SoftwareRange(EnumType.CharactersEnum.A, EnumType.CharactersEnum.C, EnterpriseID, PlatApplyString, "PlatApply");
            }
        }

        /// 获取平台应用主键ID
        /// </summary>
        private string GetPlatApplyID(string Id)
        {
            return _bll.GetPlatApplyID(Id);
        }

        /// <summary>
        /// 生成平台应用SQL
        /// </summary>
        private string PlatApplySQL(string EnterpriseID, string PlatApplyID)
        {
            string SQL = "INSERT INTO `gyy_changzhou`.`companycloudres`(`Id`,`Sid`,`CloudplatId`,`CDate`,`SysStatus`,`Memo`)VALUES(REPLACE(UUID() , '-', ''),'{0}','{1}',NOW(),0,'');";
            SQL = string.Format(SQL, new object[] { EnterpriseID, PlatApplyID });
            return SQL;
        }

        /// <summary>
        /// 生成工业电商SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterpriseBusiness_Click(object sender, EventArgs e)
        {
            int EnterpriseNameIndex = 1;
            int PlatApplyIndex = 64;
            string SQL = string.Empty;
            string EnterpriseID = string.Empty;
            string BusinessString = string.Empty;
            string EnterpriseName = string.Empty;

            ClearSQLTextBox();
            foreach (DataRow item in ImportDt.Rows)
            {
                EnterpriseName = item[EnterpriseNameIndex].ToString();
                BusinessString = item[PlatApplyIndex].ToString();
                //根据企业名称 获取企业账号ID
                EnterpriseID = _bll.GetEnterpriseInfoID(EnterpriseName);
                SoftwareRange(EnumType.CharactersEnum.A, EnumType.CharactersEnum.H, EnterpriseID, BusinessString, "Business");
            }
        }

        /// <summary>
        /// 获取电商主键ID
        /// </summary>
        private string GetBusinessID(string Id)
        {
            return _bll.GetBusinessID(Id);
        }

        /// 生成工业电商SQL
        /// </summary>
        private string BusinessSQL(string EnterpriseID, string EBusinessId)
        {
            string SQL = "INSERT INTO `gyy_changzhou`.`companyebusinessres`(`Id`,`Sid`,`EBusinessId`,`CDate`,`SysStatus`,`Memo`)VALUES(REPLACE(UUID() , '-', ''),'{0}','{1}',NOW(),0,'');";
            SQL = string.Format(SQL, new object[] { EnterpriseID, EBusinessId });
            return SQL;
        }

        /// <summary>
        /// 生成单项SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterpriseItem_Click(object sender, EventArgs e)
        {
            int EnterpriseNameIndex = 1;
            int ApplicationIndex = 22;
            string SQL = string.Empty;
            string EnterpriseID = string.Empty;
            string ApplicationString = string.Empty;
            string EnterpriseName = string.Empty;

            ClearSQLTextBox();
            foreach (DataRow item in ImportDt.Rows)
            {
                EnterpriseName = item[EnterpriseNameIndex].ToString();
                ApplicationString = item[ApplicationIndex].ToString();
                //根据企业名称 获取企业账号ID
                EnterpriseID = _bll.GetEnterpriseInfoID(EnterpriseName);
                SoftwareRange(EnumType.CharactersEnum.A, EnumType.CharactersEnum.M, EnterpriseID, ApplicationString, "Application");
            }
        }

        /// <summary>
        /// 获取单项应用主键ID
        /// </summary>
        private string GetApplicationID(string Id)
        {
            return _bll.GetApplicationID(Id);
        }

        /// <summary>
        /// 生成单项应用SQL
        /// </summary>
        private string ApplicationSQL(string EnterpriseID, string AppId)
        {
            string SQL = "INSERT INTO `gyy_changzhou`.`companyappres`(`Id`,`Sid`,`AppId`,`CDate`,`SysStatus`,`Memo`)VALUES(REPLACE(UUID() , '-', ''),'{0}','{1}',NOW(),0,'');";
            SQL = string.Format(SQL, new object[] { EnterpriseID, AppId });
            return SQL;
        }
    }
}
