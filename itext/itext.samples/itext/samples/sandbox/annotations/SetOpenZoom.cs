using System;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Samples.Sandbox.Annotations
{
    public class SetOpenZoom
    {
        public static readonly String DEST = "results/sandbox/annotations/open_at_100pct.pdf";

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new SetOpenZoom().ManipulatePdf(DEST);
        }

        protected void ManipulatePdf(String dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc, new PageSize(612, 792));
            doc.Add(new Paragraph("Hello World"));

            // Set the height of a page to 842 points and zoom value to 1 (which means 100% zoom)
            PdfExplicitDestination zoomPage = PdfExplicitDestination.CreateXYZ(pdfDoc.GetPage(1),
                0, 842, 1);
            pdfDoc.GetCatalog().SetOpenAction(zoomPage);

            doc.Close();
        }
    }
}