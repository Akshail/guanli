using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace XF
{
    public class PageBase : System.Web.UI.Page
    {
        public string log_userid
        {
            get
            {
                try
                {
                    return Session["UserID"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }

        public string log_usertype
        {
            get
            {
                try
                {
                    return Session["UserType"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }

        public string log_username
        {
            get
            {
                try
                {
                    return Session["UserName"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }

        public string log_ip
        {
            get
            {
                try
                {
                    return Session["IP"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }


        public string log_account
        {
            get
            {
                try
                {
                    return Session["Account"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }

        public void text(string value)
        {
            string path = Server.MapPath("~/log.txt");
            //string path = @"D:\Webs\ReptileManagementPlatform\log.txt";
            FileStream f = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine("--------" + DateTime.Now.ToString() + "---------");
            sw.WriteLine(value);
            sw.Flush();
            sw.Close();
            f.Close();
        }

        public void text_importLog(string value)
        {
            string path = Server.MapPath("~/import_log.txt");
            //string path = @"D:\Webs\ReptileManagementPlatform\log.txt";
            FileStream f = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine("--------" + DateTime.Now.ToString() + "---------");
            sw.WriteLine(value);
            sw.Flush();
            sw.Close();
            f.Close();
        }

        public DataTable ReadExcelToTable(string path)
        {
            //连接字符串
            string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意
            //string connstring = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; //Office 07以下版本 因为本人用Office2010 所以没有用到这个连接字符串 可根据自己的情况选择 或者程序判断要用哪一个连接字符串
            using (OleDbConnection conn = new OleDbConnection(connstring))
            {

                conn.Open();
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                DataSet set = new DataSet();
                ada.Fill(set);
                return set.Tables[0];
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(DataTable data, string sheetName, string fileName, bool isFirstRowColumn)
        {
            //string fileName = null; //文件名
            IWorkbook workbook = null;
            FileStream fs = null;

            ISheet sheet = null;
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
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
                    IRow firstRow = sheet.GetRow(1);
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
                        startRow = 2;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    if (startRow <= rowCount)
                    {
                        for (int i = startRow; i <= rowCount; ++i)
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

        public static Stream RenderDataTableToExcel(string[] headers, DataTable SourceTable)
        {
            MemoryStream ms = new MemoryStream();
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowIndex = 1;
            foreach (DataRow row in SourceTable.Rows)
            {
                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(row["ItemName"].ToString());
                dataRow.CreateCell(1).SetCellValue(row["Count"].ToString());
                dataRow.CreateCell(2).SetCellValue(row["Percent"].ToString());
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;
            return ms;
        }
    }
}