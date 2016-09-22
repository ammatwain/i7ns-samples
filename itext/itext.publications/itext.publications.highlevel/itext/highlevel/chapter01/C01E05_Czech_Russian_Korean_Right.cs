/*
* This example was written by Bruno Lowagie
* in the context of the book: iText 7 building blocks
*/

using System;
using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Test.Attributes;

namespace itext.publications.highlevel.itext.highlevel.chapter01 {
    /// <author>Bruno Lowagie (iText Software)</author>
    [WrapToTest]
    public class C01E05_Czech_Russian_Korean_Right {
        public const String DEST = "results/chapter01/czech_russian_korean.pdf";

        public const String FONT = "../../resources/fonts/FreeSans.ttf";

        public const String HCRBATANG = "../../resources/fonts/HANBatang.ttf";

        public const String CZECH = "Podivn\u00fd p\u0159\u00edpad Dr. Jekylla a pana Hyda";

        public const String RUSSIAN = "\u0421\u0442\u0440\u0430\u043d\u043d\u0430\u044f " + "\u0438\u0441\u0442\u043e\u0440\u0438\u044f "
             + "\u0434\u043e\u043a\u0442\u043e\u0440\u0430 " + "\u0414\u0436\u0435\u043a\u0438\u043b\u0430 \u0438 "
             + "\u043c\u0438\u0441\u0442\u0435\u0440\u0430 " + "\u0425\u0430\u0439\u0434\u0430";

        public const String KOREAN = "\ud558\uc774\ub4dc, \uc9c0\ud0ac, \ub098";

        /// <exception cref="System.IO.IOException"/>
        public static void Main(String[] args) {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new C01E05_Czech_Russian_Korean_Right().CreatePdf(DEST);
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdf(String dest) {
            // Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            // Initialize document
            Document document = new Document(pdf);
            // Add content
            PdfFont font1250 = PdfFontFactory.CreateFont(FONT, PdfEncodings.CP1250, true);
            document.Add(new Paragraph().SetFont(font1250).Add(CZECH).Add(" by Robert Louis Stevenson"));
            PdfFont font1251 = PdfFontFactory.CreateFont(FONT, "Cp1251", true);
            document.Add(new Paragraph().SetFont(font1251).Add(RUSSIAN).Add(" by Robert Louis Stevenson"));
            PdfFont fontUnicode = PdfFontFactory.CreateFont(HCRBATANG, PdfEncodings.IDENTITY_H, true);
            document.Add(new Paragraph().SetFont(fontUnicode).Add(KOREAN).Add(" by Robert Louis Stevenson"));
            //Close document
            document.Close();
        }
    }
}
