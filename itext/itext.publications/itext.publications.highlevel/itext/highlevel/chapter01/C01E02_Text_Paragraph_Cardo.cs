using System;
using System.IO;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Highlevel.Chapter01 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C01E02_Text_Paragraph_Cardo {
        public const String DEST = "../../../results/chapter01/text_paragraph_cardo.pdf";

        public const String REGULAR = "../../../resources/fonts/Cardo-Regular.ttf";

        public const String BOLD = "../../../resources/fonts/Cardo-Bold.ttf";

        public const String ITALIC = "../../../resources/fonts/Cardo-Italic.ttf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C01E02_Text_Paragraph_Cardo().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            // Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            // Initialize document
            Document document = new Document(pdf);
            // Add content
            PdfFont font = PdfFontFactory.CreateFont(REGULAR, true);
            PdfFont bold = PdfFontFactory.CreateFont(BOLD, true);
            PdfFont italic = PdfFontFactory.CreateFont(ITALIC, true);
            Text title = new Text("The Strange Case of Dr. Jekyll and Mr. Hyde").SetFont(bold);
            Text author = new Text("Robert Louis Stevenson").SetFont(font);
            Paragraph p = new Paragraph().SetFont(italic).Add(title).Add(" by ").Add(author);
            document.Add(p);
            //Close document
            document.Close();
        }
    }
}
