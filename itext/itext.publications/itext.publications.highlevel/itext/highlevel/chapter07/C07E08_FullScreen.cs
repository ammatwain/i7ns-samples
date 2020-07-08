using System;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Highlevel.Chapter07 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C07E08_FullScreen {
        public const String DEST = "../../../results/chapter07/fullscreen.pdf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C07E08_FullScreen().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.GetCatalog().SetPageMode(PdfName.FullScreen);
            PdfViewerPreferences preferences = new PdfViewerPreferences();
            preferences.SetNonFullScreenPageMode(PdfViewerPreferences.PdfViewerPreferencesConstants.USE_THUMBS);
            pdf.GetCatalog().SetViewerPreferences(preferences);
            Document document = new Document(pdf, PageSize.A8);
            document.Add(new Paragraph("Mr. Jekyl"));
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Mr. Hyde"));
            document.Close();
        }
    }
}
