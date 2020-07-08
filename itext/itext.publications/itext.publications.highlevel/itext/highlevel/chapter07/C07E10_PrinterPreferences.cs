using System;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Highlevel.Chapter07 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C07E10_PrinterPreferences {
        public const String DEST = "../../../results/chapter07/printerpreferences.pdf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C07E10_PrinterPreferences().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PdfViewerPreferences preferences = new PdfViewerPreferences();
            preferences.SetPrintScaling(PdfViewerPreferences.PdfViewerPreferencesConstants.NONE);
            preferences.SetNumCopies(5);
            pdf.GetCatalog().SetViewerPreferences(preferences);
            PdfDocumentInfo info = pdf.GetDocumentInfo();
            info.SetTitle("A Strange Case");
            Document document = new Document(pdf, PageSize.A4.Rotate());
            document.Add(new Paragraph("Mr. Jekyl and Mr. Hyde"));
            document.Close();
        }
    }
}
