/*
* This example was written by Bruno Lowagie
* in the context of the book: iText 7 building blocks
*/
using System;
using System.IO;

using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;

namespace iText.Highlevel.Chapter06 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C06E10_TOC_OutlinesNames {
        public const String SRC = "../../../resources/txt/jekyll_hyde.txt";

        public const String DEST = "../../../results/chapter06/jekyll_hyde_outline1.pdf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C06E10_TOC_OutlinesNames().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.GetCatalog().SetPageMode(PdfName.UseOutlines);
            // Initialize document
            Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED).SetHyphenation(new HyphenationConfig("en", "uk", 3, 3))
                .SetFont(font).SetFontSize(11);
            StreamReader sr = File.OpenText(SRC);
            String name;
            String line;
            Paragraph p;
            bool title = true;
            int counter = 0;
            PdfOutline outline = null;
            while ((line = sr.ReadLine()) != null) {
                p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title) {
                    name = String.Format("title{0:00}", counter++);
                    outline = CreateOutline(outline, pdf, line, name);
                    p.SetFont(bold).SetFontSize(12).SetKeepWithNext(true).SetDestination(name);
                    title = false;
                    document.Add(p);
                }
                else {
                    p.SetFirstLineIndent(36);
                    if (String.IsNullOrEmpty(line)) {
                        p.SetMarginBottom(12);
                        title = true;
                    }
                    else {
                        p.SetMarginBottom(0);
                    }
                    document.Add(p);
                }
            }
            //Close document
            document.Close();
        }

        public virtual PdfOutline CreateOutline(PdfOutline outline, PdfDocument pdf, String title, String name) {
            if (outline == null) {
                outline = pdf.GetOutlines(false);
                outline = outline.AddOutline(title);
                outline.AddDestination(PdfDestination.MakeDestination(new PdfString(name)));
                return outline;
            }
            PdfOutline kid = outline.AddOutline(title);
            kid.AddDestination(PdfDestination.MakeDestination(new PdfString(name)));
            return outline;
        }
    }
}
