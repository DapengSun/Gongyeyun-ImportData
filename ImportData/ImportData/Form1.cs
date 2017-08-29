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
                }));
            }
            this.ShowSQL.AppendText(SQLString);
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
                    WriteSQL("【" + fileDialog.FileName + "】已导入完成！ \r\n");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
