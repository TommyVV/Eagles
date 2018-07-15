using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Collections.Generic;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
using Eagles.Application.Model.Export;
using Eagles.Interface.Configuration;
using Eagles.Interface.Core.ExportFile;
using Eagles.Interface.DataAccess.Export;
using Eagles.DomainService.Model.User;
using System.ComponentModel;

namespace Eagles.DomainService.Core.Export
{
    public class ExportFileHandler : IExportHandler
    {
        private readonly IExportAccess exportAccess;
        private readonly IEaglesConfig configuration;

        public ExportFileHandler(IExportAccess exportAccess, IEaglesConfig configuration)
        {
            this.exportAccess = exportAccess;
            this.configuration = configuration;
        }

        public ExportResponse ExportFile(ExportRequest request)
        {
            var result = exportAccess.ExportInfo<TbUserInfo>(request.ExportSql);
            var a = ListToDataTable(result);
            return null;
        }
        
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="heading">工作簿Worksheet</param>
        /// <param name="showSrNo">是否显示行编号</param>
        /// <param name="columnsToTale">要导出的列</param>
        /// <returns></returns>
        //public void ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTale)
        //{
        //    FileStream stream = new FileStream(@"D:\abc.xlsx", FileMode.Create);
        //    using (ExcelPackage package = new ExcelPackage(stream))
        //    {
        //        ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(string.Format("{0}Data", heading));
        //        int startRowFrom = string.IsNullOrEmpty(heading) ? 1 : 3;
        //        if (showSrNo)
        //        {
        //            DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
        //            dataColumn.SetOrdinal(0);
        //            int index = 1;
        //            foreach (DataRow item in dataTable.Rows)
        //            {
        //                item[0] = index;
        //                index++;
        //            }
        //        }
        //        workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
        //        int columnIndex = 1;
        //        foreach (DataColumn item in dataTable.Columns)
        //        {
        //            ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
        //            int maxLength = columnCells.Max(cell => cell.Value == null ? 1 : cell.Value.ToString().Count());
        //            if (maxLength < 150)
        //            {
        //                workSheet.Column(columnIndex).AutoFit();
        //            }
        //            columnIndex++;
        //        }
        //        using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
        //        {
        //            r.Style.Font.Color.SetColor(System.Drawing.Color.White);
        //            r.Style.Font.Bold = true;
        //            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
        //        }
        //        using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
        //        {
        //            r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
        //            r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
        //            r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
        //            r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
        //        }
        //        for (int i = 0; i >= dataTable.Columns.Count - 1; i++)
        //        {
        //            if (i == 0 && showSrNo)
        //            {
        //                continue;
        //            }
        //            if (!columnsToTale.Contains(dataTable.Columns[i].ColumnName))
        //            {
        //                workSheet.DeleteRow(i + 1);
        //            }
        //        }
        //        if (!String.IsNullOrEmpty(heading))
        //        {
        //            workSheet.Cells["A1"].Value = heading;
        //            workSheet.Cells["A1"].Style.Font.Size = 20;
        //            workSheet.InsertColumn(1, 1);
        //            workSheet.InsertRow(1, 1);
        //            workSheet.Column(1).Width = 5;
        //        }
        //        package.Save();
        //    }
        //}

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> data, string [] columns)
        {
            DataTable dataTable = new DataTable();
            for (int i = 0; i < columns.Length; i++)
            {
                dataTable.Columns.Add(columns[i]);
            }
            foreach (var item in data)
            {
                dataTable.Rows.Add(item);
            }
            return dataTable;
        }
    }
}