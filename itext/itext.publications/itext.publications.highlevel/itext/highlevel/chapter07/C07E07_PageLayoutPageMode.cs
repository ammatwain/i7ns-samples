using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;

namespace iText.Highlevel.Chapter07 {
    /// <author>Bruno Lowagie (iText Software)</author>
    public class C07E07_PageLayoutPageMode {
        public const String SRC = "../../../resources/txt/jekyll_hyde.txt";

        public const String DEST = "../../../results/chapter07/page_mode_page_layout.pdf";

        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C07E07_PageLayoutPageMode().CreatePdf(DEST);
        }

        public virtual void CreatePdf(String dest) {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.GetCatalog().SetPageLayout(PdfName.TwoColumnRight);
            pdf.GetCatalog().SetPageMode(PdfName.UseThumbs);
            PdfPage page = pdf.AddNewPage();
            page.SetPageLabel(PageLabelNumberingStyle.LOWERCASE_ROMAN_NUMERALS, null);
            Document document = new Document(pdf);
            document.Add(new Paragraph().Add("Page left blank intentionally"));
            document.Add(new AreaBreak());
            document.Add(new Paragraph().Add("Page left blank intentionally"));
            document.Add(new AreaBreak());
            document.Add(new Paragraph().Add("Page left blank intentionally"));
            document.Add(new AreaBreak());
            page = pdf.GetLastPage();
            page.SetPageLabel(PageLabelNumberingStyle.DECIMAL_ARABIC_NUMERALS, null, 1);
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
            IList<Util.Pair<String, Util.Pair<String, int>>> toc = new List<Util.Pair
                <String, Util.Pair<String, int>>>();
            while ((line = sr.ReadLine()) != null) {
                p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title) {
                    name = String.Format("title{0:00}", counter++);
                    p.SetFont(bold).SetFontSize(12).SetKeepWithNext(true).SetDestination(name);
                    title = false;
                    document.Add(p);
                    toc.Add(new Util.Pair<string,Util.Pair<string,int>>(name, new Util.Pair<string,int>(line, pdf.GetNumberOfPages())));
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
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            p = new Paragraph().SetFont(bold).Add("Table of Contents").SetDestination("toc");
            document.Add(p);
            page = pdf.GetLastPage();
            page.SetPageLabel(null, "TOC", 1);
            toc.RemoveAt(0);
            IList<TabStop> tabstops = new List<TabStop>();
            tabstops.Add(new TabStop(580, TabAlignment.RIGHT, new DottedLine()));
            foreach (Util.Pair<String, Util.Pair<String, int>> entry in toc) {
                Util.Pair<String, int> text = entry.Value;
                p = new Paragraph().AddTabStops(tabstops).Add(text.Key).Add(new Tab()).Add(text.Value.ToString()).SetAction
                    (PdfAction.CreateGoTo(entry.Key));
                document.Add(p);
            }
            document.Close();
        }
    }
}
