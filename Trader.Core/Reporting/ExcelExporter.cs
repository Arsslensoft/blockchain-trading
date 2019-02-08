using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Trader.Core.Reporting;

namespace TachyonFix.Core.Reporting
{
   public class ExcelExporter : Exporter
   {
       public override string ExporterName { get; set; } = "Excel";
        public override string FileExtensions { get; set; } = "xlsx";

        private ICellStyle ExpressionStyle;
        private IFont CourierNew;
        private IFont BoldCourierNew;
        void InitStyles(IWorkbook workbook)
        {

            BoldCourierNew = workbook.CreateFont();
            BoldCourierNew.FontHeightInPoints = 11;
            BoldCourierNew.FontName = "Courier New";
            BoldCourierNew.IsBold = true;

            CourierNew = workbook.CreateFont();
            CourierNew.FontHeightInPoints = 11;
            CourierNew.FontName = "Courier New";

            ExpressionStyle = workbook.CreateCellStyle();
            ExpressionStyle.WrapText = true;
            ExpressionStyle.SetFont(CourierNew);
        }
        void PrepareHeader(ISheet sheet, IWorkbook workbook, DataTable dt)
        {
            // header row
            var hr = sheet.CreateRow(0);
            // set header row


            ICellStyle aircraftstyle = workbook.CreateCellStyle();
            aircraftstyle.Alignment = HorizontalAlignment.Center;
            aircraftstyle.VerticalAlignment = VerticalAlignment.Center;
            aircraftstyle.WrapText = true;
            aircraftstyle.SetFont(BoldCourierNew);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var r = hr.CreateCell(i);
                r.SetCellValue(dt.Columns[i].ColumnName);
                r.CellStyle = aircraftstyle;
            }

        }

         void ExportRows(ISheet sheet, IWorkbook workbook, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
           
                var rr = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var expr = rr.CreateCell(j);
                    expr.SetCellValue(dt.Rows[i].ItemArray[j].ToString());
                    expr.CellStyle = ExpressionStyle;
                }

            }
        }
        public override void Export(string f, DataTable dt, string additionalData = null)
        {
            NPOI.SS.UserModel.IWorkbook workbook = new XSSFWorkbook();
            InitStyles(workbook);
            var sheet = workbook.CreateSheet("Data");
            PrepareHeader(sheet, workbook, dt);
            ExportRows(sheet, workbook, dt);

            if (File.Exists(f))
                File.Delete(f);
            FileStream sw = File.Create(f);
            workbook.Write(sw);
            sw.Close();
        }
    }
}
