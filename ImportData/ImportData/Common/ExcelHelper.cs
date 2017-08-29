using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using NPOI.SS.Util;
using NPOI.HPSF;

namespace ExcelToDb
{
    public class ExcelHelper
    {
        private static HSSFWorkbook workbook = null;
        private static FileStream fs = null;
        private static MemoryStream _ms = null;
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="ColumnIntroduction">列头说明</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static MemoryStream DataTableToExcel(System.Data.DataTable data, Dictionary<string, string> ColumnIntroduction, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            workbook = new HSSFWorkbook();
            try
            {
                _ms = new MemoryStream();

                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                //属性信息
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;
                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "数码大方科技股份有限公司"; //填加xls文件作者信息
                si.LastAuthor = "数码大方科技股份有限公司"; //填加xls文件最后保存者信息
                si.Comments = "数码大方科技股份有限公司"; //填加xls文件作者信息
                si.Title = "产品列表"; //填加xls文件标题信息
                si.Subject = "产品列表";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;

                //设置字体样式
                HSSFCellStyle HeadercellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HeadercellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //字体
                NPOI.SS.UserModel.IFont headerfont = workbook.CreateFont();
                headerfont.Boldweight = (short)FontBoldWeight.Bold;
                HeadercellStyle.SetFont(headerfont);
                HeadercellStyle.IsLocked = true;
                //保护
                ICellStyle locked = workbook.CreateCellStyle();
                locked.IsLocked = true;
                //不设保护
                ICellStyle Unlocked = workbook.CreateCellStyle();
                Unlocked.IsLocked = false;

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        HSSFCell cell = (HSSFCell)row.CreateCell(j);
                        cell.SetCellValue(data.Columns[j].ColumnName);
                        cell.CellStyle = HeadercellStyle;
                        //sheet.AutoSizeColumn(j);
                    }
                    row.IsHidden = true;
                    //row.RowStyle = locked;
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                if (isColumnWritten == true) //写入列头说明
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        HSSFCell cell = (HSSFCell)row.CreateCell(j);
                        cell.SetCellValue(ColumnIntroduction.First(z => z.Key == data.Columns[j].ColumnName).Value.ToString());
                        cell.CellStyle = HeadercellStyle;
                    }
                    count = 2;
                }
                else
                {
                    count = 0;
                }

                HSSFCellStyle cellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                NPOI.SS.UserModel.IFont cellfont = workbook.CreateFont();
                cellfont.Boldweight = (short)FontBoldWeight.Normal;
                cellStyle.SetFont(cellfont);
                cellStyle.IsLocked = false;

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        HSSFCell cell = (HSSFCell)row.CreateCell(j);
                        cell.SetCellValue(data.Rows[i][j].ToString());
                        cell.CellStyle = cellStyle;
                    }
                    //row.RowStyle = Unlocked;
                    ++count;
                }

                //自适应列宽度
                for (i = 0; i < data.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                //合并单元格
                SetCellRangeAddress(sheet, 1, 1, 1, 2); //产品型号
                SetCellRangeAddress(sheet, 1, 1, 3, 5); //产品类型
                SetHideColumns(sheet, 2);
                SetHideColumns(sheet, 3);
                for (i = 6; i <= data.Columns.Count; i = i + 3){
                    if(i % 3 == 0) { 
                        sheet.SetColumnHidden(i, true);
                    }
                    SetCellRangeAddress(sheet, 1, 1, i, i+2); //产品类型
                }

                ///下面对CreateFreezePane的参数作一下说明：
                ///第一个参数表示要冻结的列数；
                ///第二个参数表示要冻结的行数；
                ///第三个参数表示右边区域可见的首列序号，从1开始计算；
                ///第四个参数表示下边区域可见的首行序号，从1开始计算；
                sheet.CreateFreezePane(0, 1, 0, 1);//冻结首行

                //设置只读修改密码
                //sheet.ProtectSheet("CAXA");

                workbook.Write(_ms); //写入到Excel
                return _ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        /// <summary>
        /// 隐藏列
        /// </summary>
        public static void SetHideColumns(ISheet sheet, int hidCol)
        {
            sheet.SetColumnHidden(hidCol, true);
        }

        /// <summary>
        /// 将Excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName">Excel文件</param>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static System.Data.DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            System.Data.DataTable data = new System.Data.DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一行的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        if (i != 1)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue; //没有数据的行默认是null　　　　　　　

                            DataRow dataRow = data.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                    dataRow[j] = row.GetCell(j).ToString();
                            }
                            data.Rows.Add(dataRow);
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}
