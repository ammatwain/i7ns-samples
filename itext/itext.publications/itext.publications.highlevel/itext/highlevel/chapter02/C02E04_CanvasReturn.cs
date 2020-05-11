using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;

namespace iText.Highlevel.Chapter02 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C02E04_CanvasReturn {
        public const String DEST = "../../../results/chapter02/canvas_return.pdf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C02E04_CanvasReturn().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PdfPage page = pdf.AddNewPage();
            PdfCanvas pdfCanvas = new PdfCanvas(page);
            Rectangle rectangle = new Rectangle(36, 650, 100, 100);
            pdfCanvas.Rectangle(rectangle);
            pdfCanvas.Stroke();
            iText.Layout.Canvas canvas1 = new iText.Layout.Canvas(pdfCanvas, rectangle);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            Text title = new Text("The Strange Case of Dr. Jekyll and Mr. Hyde").SetFont(bold);
            Text author = new Text("Robert Louis Stevenson").SetFont(font);
            Paragraph p = new Paragraph().Add(title).Add(" by ").Add(author);
            canvas1.Add(p);
            PdfPage page2 = pdf.AddNewPage();
            PdfCanvas pdfCanvas2 = new PdfCanvas(page2);
            iText.Layout.Canvas canvas2 = new iText.Layout.Canvas(pdfCanvas2, rectangle);
            canvas2.Add(new Paragraph("Dr. Jekyll and Mr. Hyde"));
            PdfPage page1 = pdf.GetFirstPage();
            PdfCanvas pdfCanvas1 = new PdfCanvas(page1.NewContentStreamBefore(), page1.GetResources(), pdf);
            rectangle = new Rectangle(100, 700, 100, 100);
            pdfCanvas1.SaveState().SetFillColor(ColorConstants.CYAN).Rectangle(rectangle).Fill().RestoreState();
            iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas1, rectangle);
            canvas.Add(new Paragraph("Dr. Jekyll and Mr. Hyde"));
            //Close document
            pdf.Close();
        }
    }
}
