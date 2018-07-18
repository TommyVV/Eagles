using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Drawing;
using Eagles.Base;
using Eagles.Application.Model.Export;
using Eagles.Interface.Configuration;
using Eagles.Interface.Core.FileExport;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Eagles.DomainService.Core.FileExport
{
    public class FileExportHandler : IFileExportHandler
    {
        private readonly IEaglesConfig configuration;

        public FileExportHandler(IEaglesConfig configuration)
        {
            this.configuration = configuration;
        }

        public ExportResponse ExportFile(ExportRequest request)
        {
            if (string.IsNullOrEmpty(request.ColumnNames))
                throw new TransactionException("M01", "无业务数据");
            DataTable dataTable = new DataTable();
            foreach (var s in request.ColumnNames.Split(','))
                dataTable.Columns.Add(s);
            foreach (var row in request.RowsList)
            {
                dataTable.Rows.Add(row.Rows);
            }


            return new ExportResponse() {ExportFilePath = ""};
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name = "dataTable" > 数据源 </ param >
        /// < param name="heading">工作簿Worksheet</param>
        /// <param name = "showSrNo" > 是否显示行编号 </ param >
        /// < param name="columnsToTale">要导出的列</param>
        /// <returns></returns>
        public string ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTale)
        {
            var exportPath = configuration.EaglesConfiguration.ExportPath;
            var fileFullName = exportPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            using (FileStream stream = new FileStream(fileFullName, FileMode.Create))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(string.Format("{0}Data", heading));
                int startRowFrom = string.IsNullOrEmpty(heading) ? 1 : 3;
                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                int columnIndex = 1;
                foreach (DataColumn item in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value == null ? 1 : cell.Value.ToString().Count());
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }
                    columnIndex++;
                }
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1fb5ad"));
                }
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Top.Color.SetColor(Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(Color.Black);
                    r.Style.Border.Left.Color.SetColor(Color.Black);
                    r.Style.Border.Right.Color.SetColor(Color.Black);
                }
                //for (int i = 0; i >= dataTable.Columns.Count - 1; i++)
                //{
                //    if (i == 0 && showSrNo)
                //    {
                //        continue;
                //    }
                //    if (!columnsToTale.Contains(dataTable.Columns[i].ColumnName))
                //    {
                //        workSheet.DeleteColumn(i + 1);
                //    }
                //}
                if (!string.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;
                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }
                package.Save();
                return fileFullName;
            }
        }
        
    }
}