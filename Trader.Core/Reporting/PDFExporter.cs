using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace Trader.Core.Reporting
{
  public  class PDFExporter : Exporter
  {
      public override string ExporterName { get; set; } = "PDF";
        public override string FileExtensions { get; set; } = "pdf";

        string ConcatStrings(string left, string right)
        {

            if ((left.Length + right.Length) < 110)
            {
                for (int i = (left.Length + right.Length); i < 110 - right.Length; i++)
                    left += " ";
            }
            return left + right;
        }
         void ExportReport(iTextSharp.text.Document doc, DataTable dt, string additionalData = null)
        {

            iTextSharp.text.pdf.draw.LineSeparator line = new iTextSharp.text.pdf.draw.LineSeparator(2f, 100f, iTextSharp.text.BaseColor.BLACK, iTextSharp.text.Element.ALIGN_CENTER, -1);
            iTextSharp.text.Chunk linebreak = new iTextSharp.text.Chunk(line);
            iTextSharp.text.Font black = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            var logo = new iTextSharp.text.Paragraph() { Alignment = 0 };
            logo.Add(new iTextSharp.text.Chunk("Tachyon", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 36, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            logo.Add(new iTextSharp.text.Chunk("Trader", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 36, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(26, 188, 156))));
            doc.Add(logo);
            doc.Add(new iTextSharp.text.Chunk(line));
            doc.Add(new iTextSharp.text.Paragraph(new iTextSharp.text.Chunk(ConcatStrings("Transaction List", DateTime.Now.ToString()), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK))) { Alignment = 0 });
            if (additionalData != null)
            {
                doc.Add(new iTextSharp.text.Chunk(line));
                doc.Add(new iTextSharp.text.Paragraph(new iTextSharp.text.Chunk(additionalData, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK))) { Alignment = 0 });
                doc.Add(new iTextSharp.text.Paragraph(new iTextSharp.text.Chunk(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK))) { Alignment = 0 });
                doc.Add(new iTextSharp.text.Paragraph(" "));
            }
            else
            {
                doc.Add(new iTextSharp.text.Paragraph(new iTextSharp.text.Chunk(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK))) { Alignment = 0 });
                doc.Add(linebreak);
                doc.Add(new iTextSharp.text.Paragraph(" "));
            }
           

            var bf = BaseFont.CreateFont(Environment.CurrentDirectory + @"\arial.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font NormalFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font TFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GREEN);
            iTextSharp.text.Font XFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED);

            PdfPTable table = new PdfPTable(dt.Columns.Count) { WidthPercentage = 100 };
            for (int i = 0; i < dt.Columns.Count; i++)
                table.AddCell(new PdfPCell() { Phrase = new iTextSharp.text.Phrase(dt.Columns[i].ColumnName), BackgroundColor = iTextSharp.text.BaseColor.GRAY });





            //table.SetWidths(new float[] { 3, 25, 25, 8 });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow GridRow = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                    table.AddCell(new iTextSharp.text.Phrase(GridRow.ItemArray[j].ToString(), NormalFont));

            }

            doc.Add(table);


        }
        public override void Export(string f, DataTable dt, string additionalData = null)
        {
            if (File.Exists(f))
                File.Delete(f);
            using (FileStream fs = File.OpenWrite(f))
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                var writer = PdfWriter.GetInstance(document, fs);
                PageEventHelper pageEventHelper = new PageEventHelper();
                writer.PageEvent = pageEventHelper;
                document.Open();
                ExportReport(document, dt, additionalData);
                document.Close();
            }
        }
    }
    public class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;

        iTextSharp.text.Font RunDateFont;
        public PageEventHelper()
        {
            BaseFont bf = BaseFont.CreateFont(Environment.CurrentDirectory + @"\arial.ttf", BaseFont.IDENTITY_H, true);
            RunDateFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
        }
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            String text = "Page " + pageN.ToString() + " of ";
            float len = this.RunDateFont.BaseFont.GetWidthPoint(text, this.RunDateFont.Size);

            iTextSharp.text.Rectangle pageSize = document.PageSize;

            cb.SetRGBColorFill(100, 100, 100);

            cb.BeginText();
            cb.SetFontAndSize(this.RunDateFont.BaseFont, this.RunDateFont.Size);
            cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin));
            cb.ShowText(text);

            cb.EndText();

            cb.AddTemplate(template, document.LeftMargin + len, pageSize.GetBottom(document.BottomMargin));
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnCloseDocument(writer, document);

            template.BeginText();
            template.SetFontAndSize(this.RunDateFont.BaseFont, this.RunDateFont.Size);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber));
            template.EndText();
        }
    }
}
