using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;

namespace iText.Highlevel.Chapter02 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C02E09_JekyllHydeV5 {
        public const String SRC = "../../../resources/txt/jekyll_hyde.txt";

        public const String DEST = "../../../results/chapter02/jekyll_hyde_v5.pdf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C02E09_JekyllHydeV5().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            // Initialize document
            Document document = new Document(pdf);
            //Set column parameters
            float offSet = 36;
            float gutter = 23;
            float columnWidth = (PageSize.A4.GetWidth() - offSet * 2) / 2 - gutter;
            float columnHeight = PageSize.A4.GetHeight() - offSet * 2;
            //Define column areas
            Rectangle[] columns = new Rectangle[] { new Rectangle(offSet, offSet, columnWidth, columnHeight), new Rectangle
                (offSet + columnWidth + gutter, offSet, columnWidth, columnHeight) };
            document.SetRenderer(new ColumnDocumentRenderer(document, columns));
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED).SetFont(font).SetHyphenation(new HyphenationConfig("en"
                , "uk", 3, 3));
            StreamReader sr = File.OpenText(SRC);
            String line;
            Paragraph p;
            bool title = true;
            AreaBreak nextPage = new AreaBreak(AreaBreakType.NEXT_PAGE);
            while ((line = sr.ReadLine()) != null) {
                p = new Paragraph(line);
                if (title) {
                    p.SetFont(bold).SetFontSize(12);
                    title = false;
                }
                else {
                    p.SetFirstLineIndent(36);
                }
                if (String.IsNullOrEmpty(line)) {
                    document.Add(nextPage);
                    title = true;
                }
                document.Add(p);
            }
            //Close document
            document.Close();
        }
    }
}
