using System;
using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.License;

namespace iText.Samples.Sandbox.Typography.Odia
{
    public class OdiaCircleAnnotation
    {
        public const String DEST = "results/sandbox/typography/OdiaCircleAnnotation.pdf";

        public static void Main(String[] args)
        {
            // Load the license file to use typography features
            LicenseKey.LoadLicenseFile(Environment.GetEnvironmentVariable("ITEXT7_LICENSEKEY") +
                                       "/itextkey-typography.xml");

            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new OdiaCircleAnnotation().CreatePDF(DEST);
        }

        public virtual void CreatePDF(String dest)
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(dest));

            // ଜନ୍ମକାଳରୁ
            String text = "\u0B1C\u0B28\u0B4D\u0B2E\u0B15\u0B3E\u0B33\u0B30\u0B41";

            // Create a rectangle for an annotation
            Rectangle rectangleAnnot = new Rectangle(55, 750, 35, 35);

            // Create the annotation, set its contents and color
            PdfAnnotation annotation = new PdfCircleAnnotation(rectangleAnnot);
            annotation
                    .SetContents(text)
                    .SetColor(ColorConstants.MAGENTA);

            // Add an empty page to the document, then add the annotation to the page
            PdfPage page = pdfDocument.AddNewPage();
            page.AddAnnotation(annotation);

            pdfDocument.Close();
        }
    }
}