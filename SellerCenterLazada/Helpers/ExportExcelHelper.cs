using DevComponents.DotNetBar.SuperGrid;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SellerCenterLazada
{
    public static class ExcelExportHelper
    {
        public static ExcelWorksheet worksheet;
        public static string ExcelContentType
        {
            get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //Get column headers
            bool isDisplayNameAttributeDefined = false;
            string[] headers = new string[Props.Length];
            int colCount = 0;
            foreach (PropertyInfo prop in Props)
            {
                isDisplayNameAttributeDefined = Attribute.IsDefined(prop, typeof(DisplayNameAttribute));

                if (isDisplayNameAttributeDefined)
                {
                    DisplayNameAttribute dna = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute));
                    if (dna != null)
                        headers[colCount] = dna.DisplayName;
                }
                else
                    headers[colCount] = prop.Name;

                colCount++;
                isDisplayNameAttributeDefined = false;
            }
            dataTable = new DataTable(typeof(T).Name);

            //Add column headers to datatable
            foreach (var header in headers)
            {
                dataTable.Columns.Add(header);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (properties[i].PropertyType.IsArray || properties[i].PropertyType.IsGenericType)
                    {
                        values[i] = properties[i].GetValue(item);
                    }
                    else if (properties[i].PropertyType.Equals(typeof(DateTime?)))
                    {
                        values[i] = ((DateTime?)properties[i].GetValue(item));
                    }
                    else if (properties[i].PropertyType.Equals(typeof(bool)))
                    {
                        values[i] = ((bool?)properties[i].GetValue(item)) == true ? "YES" : "NO";
                    }
                    else
                    {
                        values[i] = properties[i].GetValue(item) != null ? properties[i].GetValue(item).ToString().Replace("&#x0D;", "").Trim() : null;
                    }
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataTable ListToDataTableDrawColumn<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //Get column headers
            bool isDisplayNameAttributeDefined = false;
            string[] headers = new string[Props.Length];
            int colCount = 0;
            foreach (PropertyInfo prop in Props)
            {
                isDisplayNameAttributeDefined = Attribute.IsDefined(prop, typeof(DisplayNameAttribute));

                if (isDisplayNameAttributeDefined)
                {
                    DisplayNameAttribute dna = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute));
                    if (dna != null)
                        headers[colCount] = dna.DisplayName;
                }
                else
                    headers[colCount] = prop.Name;

                colCount++;
                isDisplayNameAttributeDefined = false;
            }
            dataTable = new DataTable(typeof(T).Name);

            //Add column headers to datatable
            foreach (var header in headers)
            {
                dataTable.Columns.Add(header);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (properties[i].PropertyType.IsArray || properties[i].PropertyType.IsGenericType)
                    {
                        values[i] = properties[i].GetValue(item);
                    }
                    else if (properties[i].PropertyType.Equals(typeof(DateTime?)))
                    {
                        values[i] = ((DateTime?)properties[i].GetValue(item));
                    }
                    else if (properties[i].PropertyType.Equals(typeof(bool)))
                    {
                        values[i] = ((bool?)properties[i].GetValue(item)) == true ? "YES" : "NO";
                    }
                    else
                    {
                        values[i] = properties[i].GetValue(item) != null ? properties[i].GetValue(item).ToString().Replace("&#x0D;", "").Trim() : null;
                    }
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static void FormatHeaderExportExcel(ExcelWorksheet workSheet, int startRowFrom, DataTable dataTable)
        {
            // format header - bold, yellow on black 
            using (ExcelRange range = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
            {
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Justify;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#007db8"));
            }
        }
        public static void FormatCellAndBorderForExportExcel(ExcelWorksheet workSheet, int startRowFrom, DataTable dataTable, bool isPrintHeaders = true)
        {
            var endRange = isPrintHeaders ? startRowFrom + dataTable.Rows.Count : startRowFrom + dataTable.Rows.Count - 1;
            using (ExcelRange range = workSheet.Cells[startRowFrom, 1, endRange, dataTable.Columns.Count])
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                range.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                range.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                range.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                range.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
            }
        }
        private static void FormatHeaderCenter(ExcelWorksheet workSheet, DataTable dataTable)
        {
            int totalColumn = dataTable.Columns.Count;
            using (ExcelRange title = workSheet.Cells[2, 1, 2, totalColumn])
            {
                title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
        private static void AddHeadingForStatusSummary(ExcelWorksheet workSheet, string heading, DataTable dataTable)
        {
            if (!String.IsNullOrEmpty(heading) && workSheet != null)
            {
                int totalCols = dataTable.Columns.Count;
                using (ExcelRange title = workSheet.Cells[1, 1, 1, totalCols])
                {
                    title.Merge = true;
                    title.Value = heading;
                    title.Style.Font.Bold = true;
                    title.Style.Font.Size = 14;
                    title.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
            }
        }
        private static void AddHeadingForExcellFile(ExcelWorksheet workSheet, string heading)
        {
            if (!String.IsNullOrEmpty(heading) && workSheet != null)
            {
                int totalCols = workSheet.Dimension.End.Column;
                using (ExcelRange title = workSheet.Cells[1, 1, 1, totalCols])
                {
                    title.Merge = true;
                    title.Value = heading;
                    title.Style.Font.Bold = true;
                    title.Style.Font.Size = 14;
                    title.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
            }
        }
        public static MemoryStream ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, bool isPrintHeaders = true, params string[] redudantColums)
        {
            var memoryStream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;
                // No.
                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("No", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }
                // format header export excel
                FormatHeaderExportExcel(workSheet, startRowFrom, dataTable);
                // end format header

                // add the content into the Excel file 
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, isPrintHeaders);
                workSheet.Row(1).Height = 24;
                // autofit width of cells with small content 
                int columnIndex = 1;

                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value == null ? 0 : cell.Value.ToString().Count());
                    if (maxLength <= 10)
                    {
                        workSheet.Column(columnIndex).Width = 10;
                    }
                    else if (maxLength > 10 && maxLength < 60)
                    {
                        workSheet.Column(columnIndex).Width = 30;
                    }
                    else if (maxLength >= 60)
                    {
                        workSheet.Column(columnIndex).Width = 50;
                    }
                    workSheet.Column(columnIndex).Style.WrapText = true;
                    workSheet.Column(columnIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    workSheet.Column(columnIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    columnIndex++;
                }

                // format cells - add borders 
                FormatCellAndBorderForExportExcel(workSheet, startRowFrom, dataTable, isPrintHeaders);
                // end format cells - add borders

                // removed ignored columns
                if (redudantColums != null && redudantColums.Length > 0)
                {
                    for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                    {
                        if (i == 0 && showSrNo)
                        {
                            continue;
                        }
                        if (redudantColums.Contains(dataTable.Columns[i].ColumnName))
                        {
                            workSheet.DeleteColumn(i + 1);
                        }
                    }
                }
                //add heading for excel file
                AddHeadingForExcellFile(workSheet, heading);

                package.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }
            return memoryStream;
        }
        private static void SetAutofitWidthOfCell(DataTable dataTable, ExcelWorksheet workSheet)
        {
            int columnIndex = 1;
            if (workSheet.Dimension != null)
            {
                foreach (DataColumn column in dataTable.Columns)
                {

                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value == null ? 0 : cell.Value.ToString().Count());
                    if (maxLength <= 10)
                    {
                        workSheet.Column(columnIndex).Width = 10;
                    }
                    else if (maxLength > 10 && maxLength < 60)
                    {
                        workSheet.Column(columnIndex).Width = 30;
                    }
                    else if (maxLength >= 60)
                    {
                        workSheet.Column(columnIndex).Width = 50;
                    }
                    workSheet.Column(columnIndex).Style.WrapText = true;
                    workSheet.Column(columnIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    columnIndex++;
                }
            }
        }

        #region Export List User to excel
        //user management
        public static MemoryStream ExportExcelUserListColorDrawColumn<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] redudantColums)
        {
            return ExportExcelUserListColorDrawColumn(ListToDataTableDrawColumn<T>(data), Heading, showSlno, redudantColums);
        }

        public static MemoryStream Export(GridPanel data, string workSheetName)
        {
            MemoryStream memoryStream = new MemoryStream();
            var listColumnIndex = new List<int>();
            DataTable dataTableReceive = new DataTable();

            DataColumn dataColumn = dataTableReceive.Columns.Add("No", typeof(string));
            dataColumn.SetOrdinal(0);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(workSheetName);

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (data.Columns[i].Visible)
                    {
                        dataTableReceive.Columns.Add(data.Columns[i].HeaderText?? data.Columns[i].Name, typeof(string));
                        listColumnIndex.Add(i);
                    }
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataRow dr = dataTableReceive.NewRow();
                    var col = 0;
                    dr[col++] = (i + 1);
                    foreach (var item in listColumnIndex)
                    {
                        dr[col++] = data.GetCell(i, item).Value;
                    }
                    dataTableReceive.Rows.Add(dr);
                }
                workSheet.Cells["A1"].LoadFromDataTable(dataTableReceive, true);
                workSheet.Row(1).Height = 24;
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }
            return memoryStream;
        }


        public static MemoryStream ExportExcelUserListColorDrawColumn(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] redudantColums)
        {
            MemoryStream memoryStream = new MemoryStream();
            DataTable dataTableReceive = new DataTable();


            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 2;
                // No.
                if (showSrNo)
                {
                    DataColumn dataColumn = dataTableReceive.Columns.Add("No", typeof(string));
                    dataColumn.SetOrdinal(0);
                    dataTableReceive.Columns.Add("Account Login", typeof(string));
                    dataTableReceive.Columns.Add("User Name", typeof(string));
                    dataTableReceive.Columns.Add("Corporate Email", typeof(string));
                    //dataTableReceive.Columns.Add("Group", typeof(string));
                    dataTableReceive.Columns.Add("Role", typeof(string));
                    dataTableReceive.Columns.Add("Active Directory Title", typeof(string));
                    dataTableReceive.Columns.Add("Status", typeof(string));
                    dataTableReceive.Columns.Add("Last Modified By", typeof(string));
                    dataTableReceive.Columns.Add("Last Modified", typeof(string));
                    dataTableReceive.Columns.Add("Last Login", typeof(string));
                    for (int iRow = 0; iRow < dataTable.Rows.Count + 1; iRow++)
                    {
                        if (iRow != dataTable.Rows.Count)
                        {
                            dataTableReceive.Rows.Add((iRow + 1).ToString()
                            , dataTable.Rows[iRow]["AzureAccount"]
                            , dataTable.Rows[iRow]["UserName"]
                            , dataTable.Rows[iRow]["CorporateEmail"]
                            //, dataTable.Rows[iRow]["Groups"]
                            , dataTable.Rows[iRow]["Roles"]
                            , dataTable.Rows[iRow]["ActiveDirectoryTitle"]
                            , dataTable.Rows[iRow]["IsActived"]
                            , dataTable.Rows[iRow]["UpdatedBy"]
                            , dataTable.Rows[iRow]["UpdatedDateClient"]
                            , dataTable.Rows[iRow]["LastLoggedInClient"]
                           );

                        }
                    }

                    workSheet.Cells["A" + (startRowFrom + 1)].LoadFromDataTable(dataTableReceive, true);
                    workSheet.Row(1).Height = 24;

                    //// autofit width of cells with small content 
                    SetAutofitWidthOfCell(dataTableReceive, workSheet);
                    //MergeTitleQuarterlyDetail(workSheet);
                    //MergeRowCellHeaderQuarterlyDetail(workSheet, dataTableReceive);
                    //// format cells - add borders 
                    FormatCellAndBorderForExportExcelUserList(workSheet, startRowFrom + 1, dataTableReceive);

                    FormatHeaderCenter(workSheet, dataTableReceive);

                    ////add heading for excel file
                    AddHeadingForStatusSummary(workSheet, heading, dataTableReceive);
                    //FormatHeaderExportExcelCustomQuarterlyDetail(workSheet, startRowFrom, dataTableReceive);


                    //int rowCount = dataTableReceive.Rows.Count;
                    //FormatCenterExcelRange(workSheet, startRowFrom, dataTable, rowCount);

                    int?[] arrayMin = new int?[] { };
                    // +3: 3 row of header
                    SetColorAndBackGroudColorForCellsUserList(workSheet, startRowFrom, dataTableReceive.Rows.Count + 3, arrayMin, dataTableReceive);

                    //setColorAndBoldForAverageQuarterlyDetail(workSheet, dataTableReceive);

                    // end format header
                    package.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                }
            }
            return memoryStream;
        }
        public static void FormatCellAndBorderForExportExcelUserList(ExcelWorksheet workSheet, int startRowFrom, DataTable dataTable)
        {
            using (ExcelRange range = workSheet.Cells[startRowFrom, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                range.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                range.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                range.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                range.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
            }
            using (ExcelRange range = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
            {
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Justify;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#007db8"));
            }
        }
        public static void SetColorAndBackGroudColorForCellsUserList(ExcelWorksheet workSheet, int startRowFrom, int rowCount, int?[] listMin, DataTable dataTable)
        {
            startRowFrom = startRowFrom + 1;
            int totalCol = dataTable.Columns.Count;
            string[] arrayCharacterColumn = { "H" };
            for (int row = 4; row < rowCount + 1; row++)
            {
                for (int c = 0; c < arrayCharacterColumn.Length; c++)
                {
                    var rowValue = workSheet.Cells[string.Format("{0}{1}", arrayCharacterColumn[c], row)].Value != null ? workSheet.Cells[string.Format("{0}{1}", arrayCharacterColumn[c], row)].Value : "";
                    if (rowValue.Equals("Active"))
                    {
                        workSheet.Cells[string.Format("{0}{1}", arrayCharacterColumn[c], row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[string.Format("{0}{1}", arrayCharacterColumn[c], row)].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(110, 162, 4));
                    }
                    else if (rowValue.Equals("Inactive"))
                    {
                        workSheet.Cells[string.Format("{0}{1}", arrayCharacterColumn[c], row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[string.Format("{0}{1}", arrayCharacterColumn[c], row)].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(170, 170, 170));
                    }
                }
            }
        }
        #endregion
    }
    public class NotExportAttribute : Attribute { };

}