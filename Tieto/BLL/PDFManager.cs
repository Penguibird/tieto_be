using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public class PDFManager : IPDFManager
    {
        public byte[] CreatePdf(Trip trip)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document();

            var pdfwriter = PdfWriter.GetInstance(document, ms);
            document.Open();
            PdfContentByte cb = pdfwriter.DirectContent;

            Paragraph companyName = new Paragraph(new Phrase("Tieto Czech s.r.o."))
            {
                Font = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, false)),
                Alignment = Element.ALIGN_LEFT
            };

            byte[] image = File.ReadAllBytes(Path.GetFullPath("~/../Assets/logo.png").Replace("~\\", ""));

            Image logo = Image.GetInstance(image);
            float logoWidth = 80;
            float logoHeight = (float)(80 / 2.97897898);
            logo.SetAbsolutePosition(document.Right - logoWidth, document.Top - logoHeight);
            logo.ScaleAbsolute(logoWidth, logoHeight);

            document.Add(companyName);
            document.Add(logo);

            ColumnText ct = new ColumnText(cb);
            string[] texts = { "Regulation - Appendix 1", "Reimbursement of traveling expenses - order", "V 2019_02.0", "Internal", "2019-03-12" };
            for (int i = 0; i < 5; i++)
            {
                Paragraph p = new Paragraph(new Phrase(texts[i]))
                {
                    Font = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, false), Font.DEFAULTSIZE - 2, i == 0 ? Font.BOLD : Font.NORMAL),
                    Alignment = Element.ALIGN_LEFT
                };
                ct.AddElement(p);
            }

            ct.SetSimpleColumn(document.Right / 2 - 80, document.Top, document.Right / 2 + 160, document.Top - 100);
            ct.Go();

            cb.MoveTo(document.Left, document.Top - 100);
            cb.LineTo(document.Right, document.Top - 100);
            cb.Stroke();

            Font headerTitleFont = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, false), Font.DEFAULTSIZE - 3, Font.NORMAL);
            Font headerContentFont = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, false), Font.DEFAULTSIZE - 3, Font.BOLD);
            Font headerContentFontItalic = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, false), Font.DEFAULTSIZE - 3, Font.ITALIC);

            PdfPTable headerTable = new PdfPTable(4);
            headerTable.SetWidths(new float[] { 1.5f, 1.0f, 0.7f, 0.8f});
            headerTable.TotalWidth = document.Right - document.Left;
            headerTable.AddCell(new PdfPCell(new Phrase("Tieto Czech s.r.o.")) { Colspan = 1, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f, PaddingLeft = 0f, BorderWidthBottom = 1f });
            headerTable.AddCell(new PdfPCell(new Phrase("Business Trip Order", new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, false), Font.DEFAULTSIZE + 2, Font.BOLD))) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10f, PaddingRight = 0f, BorderWidthBottom = 1f });

            headerTable.AddCell(new PdfPCell(new Phrase("Surname, Name, Title:", headerTitleFont)) { Border = PdfPCell.NO_BORDER, PaddingLeft = 0f, PaddingTop = 10f });
            headerTable.AddCell(new PdfPCell(new Phrase("Name Surname", headerContentFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, PaddingTop = 10f, PaddingRight = 0f });
            headerTable.AddCell(new PdfPCell(new Phrase("Project:", headerTitleFont)) { Border = PdfPCell.NO_BORDER, PaddingTop = 10f, PaddingLeft = 20f });
            headerTable.AddCell(new PdfPCell(new Phrase(trip.Project, headerContentFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, PaddingRight = 0, PaddingTop = 10f });

            headerTable.AddCell(new PdfPCell(new Phrase("Date and signature of employee's superior:", headerTitleFont)) { Border = PdfPCell.NO_BORDER, PaddingLeft = 0f });
            headerTable.AddCell(new PdfPCell(new Phrase("")/*Replace with field*/) { Border = PdfPCell.BOTTOM_BORDER, BorderWidthBottom = 1f, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, PaddingRight = 0f });
            headerTable.AddCell(new PdfPCell(new Phrase("Task:", headerTitleFont)) { Border = PdfPCell.NO_BORDER, PaddingLeft = 20f });
            headerTable.AddCell(new PdfPCell(new Phrase(trip.Task, headerContentFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, PaddingRight = 0 });

            headerTable.AddCell(new PdfPCell(new Phrase("Purpose of the trip:", headerTitleFont)) { Border = PdfPCell.NO_BORDER, PaddingLeft = 0f });
            headerTable.AddCell(new PdfPCell(new Phrase(trip.Purpose, headerContentFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, PaddingRight = 0f });
            headerTable.AddCell(new PdfPCell(new Phrase("Comment:", headerTitleFont)) { Border = PdfPCell.NO_BORDER, PaddingLeft = 20f });
            headerTable.AddCell(new PdfPCell(new Phrase(trip.Comment != null && trip.Comment != "" ? trip.Comment : "No comment", trip.Comment != null && trip.Comment != "" ? headerContentFont : headerContentFontItalic)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, PaddingRight = 0 });


            headerTable.WriteSelectedRows(0, 9, document.Left, document.Top - 105, cb);

            document.Close();
            ms.Close();

            byte[] pdf = ms.ToArray();

            return pdf;
        }
    }
}
