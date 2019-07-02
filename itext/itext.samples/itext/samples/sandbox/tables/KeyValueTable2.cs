/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
Authors: iText Software.

For more information, please contact iText Software at this address:
sales@itextpdf.com
*/

using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Samples.Sandbox.Tables
{
    public class KeyValueTable2
    {
        public static readonly string DEST = "../../results/sandbox/tables/key_value_table2.pdf";

        protected PdfFont regular;

        protected PdfFont bold;

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new KeyValueTable2().ManipulatePdf(DEST);
        }

        private void ManipulatePdf(string dest)
        {
            regular = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            UserObject rohit = new UserObject();
            rohit.Name = "Rohit";
            rohit.Id = "6633429";
            rohit.Reputation = 1;
            rohit.JobTitle = "Copy/paste artist";

            UserObject bruno = new UserObject();
            bruno.Name = "Bruno Lowagie";
            bruno.Id = "1622493";
            bruno.Reputation = 42690;
            bruno.JobTitle = "Java Rockstar";

            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            Document document = new Document(pdf);

            document.Add(CreateTable(rohit, bruno));

            document.Close();
        }

        private Table CreateTable(UserObject user1, UserObject user2)
        {
            Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(bold).Add(new Paragraph("Name:")));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).Add(new Paragraph(user1.Name)));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).Add(new Paragraph(user2.Name)));

            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(bold).Add(new Paragraph("Id:")));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).Add(new Paragraph(user1.Id)));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).Add(new Paragraph(user2.Id)));

            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(bold).Add(new Paragraph("Reputation:")));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular)
                .Add(new Paragraph(user1.Reputation.ToString())));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular)
                .Add(new Paragraph(user2.Reputation.ToString())));

            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(bold).Add(new Paragraph("Job title:")));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).Add(new Paragraph(user1.JobTitle)));
            table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).Add(new Paragraph(user2.JobTitle)));

            return table;
        }

        private class UserObject
        {
            public string Name { set; get; }

            public string Id { set; get; }

            public string JobTitle { set; get; }

            public int Reputation { set; get; }
        }
    }
}