using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace XF
{
    public class ExcelHelper1
    {
        #region Report_jszc
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_jszc(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 6;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 1;        // 开始列（0为标题列，1为行标题，数据从2开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_gjfl
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI2_gjfl(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI2(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "UTF-8";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识--sheet名</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI2(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            //int rowIndex1 = rowIndex;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);
                if (dataRow == null)
                    dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列（0为标题列，从1开始）
                //int columnIndex1 = 5;
                //int columnIndex2 = 6;
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)
                        break;
                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);


                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                        //int intV = 0;
                        //int.TryParse(drValue, out intV);
                        //if (column.ColumnName == "shcqz")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex1);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "wdcqz")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex2);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "shxz")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex1);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "wdxz")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex2);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "shdz")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex1);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "wddz")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex2);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "shydf")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex1);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "wdydf")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex2);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "shqt")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex1);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "wdqt")
                        //{
                        //    IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                        //    ICell newCell1 = dataRow1.GetCell(columnIndex2);
                        //    newCell1.SetCellValue(intV);
                        //}
                        //else if (column.ColumnName == "sh" || column.ColumnName == "wd")
                        //{
                        //    newCell.SetCellValue(intV);
                        //}
                        //break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex = rowIndex + 1;
                //rowIndex1 = rowIndex1 + 1;
            }

            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_mzfl
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_mzfl(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_mzfl(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_mzfl(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);
                if (dataRow == null)
                    dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)

                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_nljg
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_nljg(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_nljg(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_nljg(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 6;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 1;        // 开始列（0为标题列，1为行标题，数据从2开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_mzfl
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_rsfl(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_rsfl(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_rsfl(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);
                if (dataRow == null)
                    dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)

                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_xlfl
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_xlfl(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_xlfl(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_xlfl(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 5;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 1;        // 开始列（0为标题列，1为行标题，数据从2开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_zzmm
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_zzmm(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_zzmm(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_zzmm(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 12;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 3;        // 开始列（0为标题列，1为行标题，数据从2开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 3)
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_xxhz
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_xxhz(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_xxhz(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_xxhz(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 25;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 3;        // 开始列（0为标题列，1为行标题，数据从2开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 3)
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion


        #region Report_envelope
        /// <summary>
        /// 利用模板，DataTable导出到Excel（信封贴）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_envelope(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_envelope(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_envelope(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 0;       // Excel中起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "杨浦区台办";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "杨浦区台办"; //填加xls文件作者信息
                si.ApplicationName = "杨浦区台办"; //填加xls文件创建程序信息
                si.LastAuthor = "杨浦区台办"; //填加xls文件最后保存者信息
                si.Comments = "杨浦区台办"; //填加xls文件作者信息
                si.Title = "杨浦区台办"; //填加xls文件标题信息
                si.Subject = "杨浦区台办";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            //IRow headerRow = sheet.GetRow(0);
            //ICell headerCell = headerRow.GetCell(0);
            //headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }



            ////设置列宽
            //sheet.DefaultColumnWidth = 29.75 * 256;
            ////每页纸打印24行*3列个信封贴
            //for (int e_rowindex = 0; e_rowindex < 24; e_rowindex += 3)
            for (int e_rowindex = 0; e_rowindex < dtSource.Rows.Count; e_rowindex += 3)
            {
                for (int p_rowindex = 0; p_rowindex < 3; p_rowindex++)
                {

                    IRow dataRow = sheet.GetRow(e_rowindex + p_rowindex);
                    if (dataRow == null)
                        dataRow = sheet.CreateRow(e_rowindex + p_rowindex);
                    switch (p_rowindex)
                    {
                        case 0: dataRow.HeightInPoints = (float)22.5;
                            break;
                        case 1: dataRow.HeightInPoints = (float)45.5;
                            break;
                        case 2: dataRow.HeightInPoints = (float)25;
                            break;
                        default: break;
                    }



                    for (int e_columnindex = 0; e_columnindex < 3 && e_columnindex < dtSource.Rows.Count; e_columnindex++)
                    {


                        ICell newCell = dataRow.GetCell(e_columnindex);
                        if (newCell == null)
                            newCell = dataRow.CreateCell(e_columnindex);

                        //单元格格式

                        if (e_rowindex + e_columnindex < dtSource.Rows.Count)
                        {
                            string drValue = dtSource.Rows[e_rowindex + e_columnindex][p_rowindex + 1].ToString();

                            Type t = dtSource.Rows[e_rowindex + e_columnindex][p_rowindex].GetType();
                            switch (t.ToString())
                            {
                                case "System.String"://字符串类型
                                    newCell.SetCellValue(drValue);
                                    break;
                                case "System.DateTime"://日期类型
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    newCell.SetCellValue(dateV);

                                    break;
                                case "System.Boolean"://布尔型
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(drValue, out doubV);
                                    newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                        }
                        switch (p_rowindex)
                        {
                            case 0:

                                ICellStyle style_ZipCode = workbook.CreateCellStyle();
                                style_ZipCode.Alignment = HorizontalAlignment.Left;
                                style_ZipCode.VerticalAlignment = VerticalAlignment.Center;
                                style_ZipCode.WrapText = true;
                                //style_ZipCode.IsLocked = true;
                                IFont font_ZipCode = workbook.CreateFont();
                                font_ZipCode.FontHeightInPoints = 11;
                                //font_ZipCode.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                                font_ZipCode.FontName = "Arial";
                                style_ZipCode.SetFont(font_ZipCode);
                                newCell.CellStyle = style_ZipCode;
                                break;
                            case 1:
                                ICellStyle style_Address = workbook.CreateCellStyle();
                                style_Address.Alignment = HorizontalAlignment.Center;
                                style_Address.VerticalAlignment = VerticalAlignment.Top;
                                style_Address.WrapText = true;
                                //style_Address.IsLocked = true;
                                IFont font_Address = workbook.CreateFont();
                                font_Address.FontHeightInPoints = 11;
                                //font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                                font_Address.FontName = "Arial";
                                style_Address.SetFont(font_Address);
                                newCell.CellStyle = style_Address;
                                break;
                            case 2:
                                ICellStyle style_Name = workbook.CreateCellStyle();
                                style_Name.Alignment = HorizontalAlignment.Center;
                                style_Name.VerticalAlignment = VerticalAlignment.Bottom;
                                style_Name.WrapText = true;
                                //style_Name.IsLocked = true;

                                IFont font_Name = workbook.CreateFont();
                                font_Name.FontHeightInPoints = 11;
                                font_Name.FontName = "Arial";
                                style_Name.SetFont(font_Name);
                                newCell.CellStyle = style_Name;
                                break;
                            default: break;
                        }


                    }


                }


            }



            //依次把DataTable中各行导出
            //foreach (DataRow row in dtSource.Rows)
            //{
            //    #region 填充内容
            //    IRow dataRow = sheet.GetRow(rowIndex);


            //    if (dataRow == null)
            //        dataRow = sheet.CreateRow(rowIndex);

            //    //Excel某行各列依次填充内容
            //    int columnIndex = 0;        // 开始列
            //    foreach (DataColumn column in dtSource.Columns)
            //    {
            //        // 列序号赋值
            //        if (columnIndex > dtSource.Columns.Count)

            //            break;

            //        ICell newCell = dataRow.GetCell(columnIndex);
            //        if (newCell == null)
            //            newCell = dataRow.CreateCell(columnIndex);

            //        string drValue = row[column].ToString();

            //        switch (column.DataType.ToString())
            //        {
            //            case "System.String"://字符串类型
            //                newCell.SetCellValue(drValue);
            //                break;
            //            case "System.DateTime"://日期类型
            //                DateTime dateV;
            //                DateTime.TryParse(drValue, out dateV);
            //                newCell.SetCellValue(dateV);

            //                break;
            //            case "System.Boolean"://布尔型
            //                bool boolV = false;
            //                bool.TryParse(drValue, out boolV);
            //                newCell.SetCellValue(boolV);
            //                break;
            //            case "System.Int16"://整型
            //            case "System.Int32":
            //            case "System.Int64":
            //            case "System.Byte":
            //                int intV = 0;
            //                int.TryParse(drValue, out intV);
            //                newCell.SetCellValue(intV);
            //                break;
            //            case "System.Decimal"://浮点型
            //            case "System.Double":
            //                double doubV = 0;
            //                double.TryParse(drValue, out doubV);
            //                newCell.SetCellValue(doubV);
            //                break;
            //            case "System.DBNull"://空值处理
            //                newCell.SetCellValue("");
            //                break;
            //            default:
            //                newCell.SetCellValue("");
            //                break;
            //        }
            //        columnIndex++;
            //    }
            //    #endregion

            //    rowIndex++;
            //}


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_Association
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_Association(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_Association(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_Association(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);
                if (dataRow == null)
                    dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)

                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_PersonAssociation
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_PersonAssociation(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_PersonAssociation(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_PersonAssociation(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);
                if (dataRow == null)
                    dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    //if (columnIndex >= dtSource.Columns.Count + 1)
                    if (columnIndex >= 4)

                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report_Person
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI_Person(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI_Person(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }

                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI_Person(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = dtSource.Rows.Count;        // 每个类别的总行数
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "虹口区科协";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "虹口区科协"; //填加xls文件作者信息
                si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
                si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
                si.Comments = "虹口区科协"; //填加xls文件作者信息
                si.Title = "虹口区科协"; //填加xls文件标题信息
                si.Subject = "虹口区科协";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);
                if (dataRow == null)
                    dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    //if (columnIndex >= dtSource.Columns.Count + 1)
                    if (columnIndex >= 2)//为了隐去联合查询得到DataTable中的Id字段，将excel列数规定为2列
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"

            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report3
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI3(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string Browser)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI3(dtSource, strTemplateFileName, flg, titleName))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "UTF-8";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                }
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI3(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 90;        // 每个类别的总行数
            int rowIndex = 3;       // 起始行
            int dtRowIndex = dtSource.Rows.Count * 5;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "校服管理";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "校服管理"; //填加xls文件作者信息
                si.ApplicationName = "校服管理"; //填加xls文件创建程序信息
                si.LastAuthor = "校服管理"; //填加xls文件最后保存者信息
                si.Comments = "校服管理"; //填加xls文件作者信息
                si.Title = "校服管理"; //填加xls文件标题信息
                si.Subject = "校服管理";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            int rowIndex1 = rowIndex;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 1;        // 开始列（0为标题列，从1开始）
                int columnIndex1 = 3;
                //int columnIndex2 = 4;
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)
                        break;
                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);


                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            #region cqz
                            if (column.ColumnName == "cqz")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzparentsconfirm")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzparentsmeeting")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 2);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzpublicinfo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 3);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzuniformphoto")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 4);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqznumber")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 5);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzhavecontract")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 6);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzcontractrecorddate")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 7);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzcompanyreport")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 8);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzschoolreportNow")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 9);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzschoolreportNo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 10);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "cqzhavecontractNO")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 11);
                                newCell1.SetCellValue(intV);
                            }
                            #endregion

                            #region xz
                            else if (column.ColumnName == "xz")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzparentsconfirm")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzparentsmeeting")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 2);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzpublicinfo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 3);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzuniformphoto")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 4);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xznumber")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 5);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzhavecontract")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 6);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzcontractrecorddate")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 7);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzcompanyreport")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 8);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzschoolreportNow")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 9);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzschoolreportNo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 10);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "xzhavecontractNO")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 1);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 11);
                                newCell1.SetCellValue(intV);
                            }
                            #endregion

                            #region dz
                            else if (column.ColumnName == "dz")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzparentsconfirm")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzparentsmeeting")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 2);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzpublicinfo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 3);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzuniformphoto")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 4);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dznumber")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 5);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzhavecontract")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 6);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzcontractrecorddate")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 7);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzcompanyreport")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 8);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzschoolreportNow")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 9);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzschoolreportNo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 10);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "dzhavecontractNO")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 2);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 11);
                                newCell1.SetCellValue(intV);
                            }
                            #endregion

                            #region ydf
                            else if (column.ColumnName == "ydf")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfparentsconfirm")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfparentsmeeting")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 2);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfpublicinfo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 3);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfuniformphoto")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 4);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfnumber")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 5);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfhavecontract")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 6);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfcontractrecorddate")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 7);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfcompanyreport")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 8);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfschoolreportNow")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 9);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfschoolreportNo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 10);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "ydfhavecontractNO")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 3);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 11);
                                newCell1.SetCellValue(intV);
                            }
                            #endregion

                            #region qt
                            else if (column.ColumnName == "qt")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtparentsconfirm")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 1);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtparentsmeeting")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 2);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtpublicinfo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 3);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtuniformphoto")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 4);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtnumber")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 5);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qthavecontract")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 6);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtcontractrecorddate")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 7);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtcompanyreport")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 8);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtschoolreportNow")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 9);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qtschoolreportNo")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 10);
                                newCell1.SetCellValue(intV);
                            }
                            else if (column.ColumnName == "qthavecontractNO")
                            {
                                IRow dataRow1 = sheet.GetRow(rowIndex1 + 4);
                                ICell newCell1 = dataRow1.GetCell(columnIndex1 + 11);
                                newCell1.SetCellValue(intV);
                            }
                            #endregion

                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex = rowIndex + 5;
                rowIndex1 = rowIndex1 + 5;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region Report4
        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        public static void ExportExcelForDtByNPOI4(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, string area, string TimeBatch)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNPOI4(dtSource, strTemplateFileName, flg, titleName, area, TimeBatch))
            {
                ////using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                ////{
                byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);

                #region 客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "GB2312";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion

                ////    fs.Flush();
                ////}
            }
        }

        /// <summary>
        /// 利用模板，DataTable导出到Excel（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNPOI4(DataTable dtSource, string strTemplateFileName, int flg, string titleName, string area, string TimeBatch)
        {

            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int totalIndex = 1000;        // 每个类别的总行数
            int rowIndex = 3;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            ISheet sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "校服管理";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "校服管理"; //填加xls文件作者信息
                si.ApplicationName = "校服管理"; //填加xls文件创建程序信息
                si.LastAuthor = "校服管理"; //填加xls文件最后保存者信息
                si.Comments = "校服管理"; //填加xls文件作者信息
                si.Title = "校服管理"; //填加xls文件标题信息
                si.Subject = "校服管理";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            IRow headerRow = sheet.GetRow(0);
            ICell headerCell = headerRow.GetCell(0);
            headerCell.SetCellValue(titleName);
            //上报区县
            IRow headerRow1 = sheet.GetRow(1);
            ICell headerCell1 = headerRow1.GetCell(1);
            headerCell1.SetCellValue(area);
            //上报时间
            IRow headerRow2 = sheet.GetRow(1);
            ICell headerCell2 = headerRow2.GetCell(3);
            headerCell2.SetCellValue(TimeBatch);
            #endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < rowIndex + totalIndex; i++)
            {
                IRow dataRowD = sheet.GetRow(i);
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.GetRow(rowIndex);

                int columnIndex = 0;        // 开始列（0为标题列，从1开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count + 1)
                        break;

                    ICell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }


            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear "0"
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    IRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            ICell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case ICellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(ICellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case ICellType.BLANK:

            //                    case ICellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;


                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        HSSFWorkbook hssfworkbook;
        void InitializeWorkbook()
        {
            //Create a entry of DocumentSummaryInformation
            hssfworkbook = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "虹口区科协";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //Create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "虹口区科协";
            si.Author = "虹口区科协"; //填加xls文件作者信息
            si.ApplicationName = "虹口区科协"; //填加xls文件创建程序信息
            si.LastAuthor = "虹口区科协"; //填加xls文件最后保存者信息
            si.Comments = "虹口区科协"; //填加xls文件作者信息
            si.Title = "虹口区科协"; //填加xls文件标题信息
            hssfworkbook.SummaryInformation = si;
        }

        MemoryStream GetExcelStream()
        {
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }

        void GenerateData(string sheetName, DataTable dt)//
        {
            ISheet sheet = hssfworkbook.CreateSheet(sheetName);
            IRow rowFirst = sheet.CreateRow(0);
            rowFirst.CreateCell(0).SetCellValue("学会名称");

            for (int k = 0; k < dt.Columns.Count; k++)
            {
                switch (dt.Columns[k].ColumnName)
                {
                    case "AssociationName":
                        rowFirst.CreateCell(k).SetCellValue("学会名称");
                        break;
                    case "TotalNum":
                        rowFirst.CreateCell(k).SetCellValue("会员总数");
                        break;
                    case "TotalNum_China":
                        rowFirst.CreateCell(k).SetCellValue("中国国籍人数（名）");
                        break;
                    case "TotalNum_Foreign":
                        rowFirst.CreateCell(k).SetCellValue("外籍人数（名）");
                        break;
                    case "TotalPercent_China":
                        rowFirst.CreateCell(k).SetCellValue("中国国籍比例");
                        break;
                    case "TotalPercent_Foreign":
                        rowFirst.CreateCell(k).SetCellValue("外籍比例");
                        break;
                    case "TotalNum_Han":
                        rowFirst.CreateCell(k).SetCellValue("汉族人数");
                        break;
                    case "TotalNum_Minority":
                        rowFirst.CreateCell(k).SetCellValue("少数民族人数");
                        break;
                    case "TotalPercent_Han":
                        rowFirst.CreateCell(k).SetCellValue("汉族比例");
                        break;
                    case "TotalPercent_Minority":
                        rowFirst.CreateCell(k).SetCellValue("少数民族比例");
                        break;
                    case "TotalNum_ThisYear":
                        rowFirst.CreateCell(k).SetCellValue("当年新增会员数");
                        break;
                    case "TotalNum_Man":
                        rowFirst.CreateCell(k).SetCellValue("男性人数（名）");
                        break;
                    case "TotalNum_Woman":
                        rowFirst.CreateCell(k).SetCellValue("女性人数（名）");
                        break;
                    case "TotalPercent_Man":
                        rowFirst.CreateCell(k).SetCellValue("男性比例");
                        break;
                    case "TotalPercent_Woman":
                        rowFirst.CreateCell(k).SetCellValue("女性比例");
                        break;
                }

            }
            for (int i = 1; i < dt.Rows.Count + 1; i++)
            {
                IRow row = sheet.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                }
            }

        }

        public void ExportClass(string sheetName, string excelNameXls, string Browser, DataTable dt)//
        {
            string filename = excelNameXls;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            if (Browser.IndexOf("firefox") > -1 || Browser.IndexOf("chrome") > -1)
            {
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + filename));
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
            }

            HttpContext.Current.Response.Clear();
            //初始化
            InitializeWorkbook();

            //导入数据
            GenerateData(sheetName, dt);
            GetExcelStream().WriteTo(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.End();
        }
    }
}