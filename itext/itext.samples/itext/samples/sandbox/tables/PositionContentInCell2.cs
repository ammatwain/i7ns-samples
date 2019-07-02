/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
Authors: iText Software.

For more information, please contact iText Software at this address:
sales@itextpdf.com
*/

using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Samples.Sandbox.Tables
{
    public class PositionContentInCell2
    {
        public static readonly string DEST = "../../results/sandbox/tables/position_content_in_cell2.pdf";

        public static readonly String IMG = "../../resources/img/info.png";

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new PositionContentInCell2().ManipulatePdf(DEST);
        }

        /// <exception cref="System.Exception"/>
        private void ManipulatePdf(String dest)
        {
            // 1. Create a Document which contains a table:
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);
            Table table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            Cell cell1 = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            Cell cell4 = new Cell();
            Cell cell5 = new Cell();
            Cell cell6 = new Cell();
            Cell cell7 = new Cell();
            Cell cell8 = new Cell();

            // 2. Inside that table, make each cell with specific height:
            cell1.SetHeight(50);
            cell2.SetHeight(50);
            cell3.SetHeight(50);
            cell4.SetHeight(50);
            cell5.SetHeight(50);
            cell6.SetHeight(50);
            cell7.SetHeight(50);
            cell8.SetHeight(50);
            
            Image img = new Image(ImageDataFactory.Create(IMG));
            
            // 3. Each cell has the same background image
            // 4. Add text in front of the image at specific position
            cell1.SetNextRenderer(new ImageAndPositionRenderer(cell1, 0, 1, img,
                "Top left", TextAlignment.LEFT));
            cell2.SetNextRenderer(new ImageAndPositionRenderer(cell2, 1, 1, img,
                "Top right", TextAlignment.RIGHT));
            cell3.SetNextRenderer(new ImageAndPositionRenderer(cell3, 0.5f, 1, img,
                "Top center", TextAlignment.CENTER));
            cell4.SetNextRenderer(new ImageAndPositionRenderer(cell4, 0.5f, 0, img,
                "Bottom center", TextAlignment.CENTER));
            cell5.SetNextRenderer(new ImageAndPositionRenderer(cell5, 0.5f, 0.5f,
                new Image(ImageDataFactory.Create(IMG)),
                "Middle center", TextAlignment.CENTER));
            cell6.SetNextRenderer(new ImageAndPositionRenderer(cell6, 0.5f, 0.5f,
                new Image(ImageDataFactory.Create(IMG)),
                "Middle center", TextAlignment.CENTER));
            cell7.SetNextRenderer(new ImageAndPositionRenderer(cell7, 0, 0, img,
                "Bottom left", TextAlignment.LEFT));
            cell8.SetNextRenderer(new ImageAndPositionRenderer(cell8, 1, 0, img,
                "Bottom right", TextAlignment.RIGHT));

            // Wrap it all up!
            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);
            table.AddCell(cell5);
            table.AddCell(cell6);
            table.AddCell(cell7);
            table.AddCell(cell8);
            doc.Add(table);
            
            doc.Close();
        }

        private class ImageAndPositionRenderer : CellRenderer
        {
            private Image img;

            private String content;

            private TextAlignment? alignment;

            private float wPct;

            private float hPct;

            public ImageAndPositionRenderer(Cell modelElement, float wPct, float hPct, Image img,
                String content, TextAlignment? alignment)
                : base(modelElement)
            {
                this.img = img;
                this.content = content;
                this.alignment = alignment;
                this.wPct = wPct;
                this.hPct = hPct;
            }

            public override void Draw(DrawContext drawContext)
            {
                base.Draw(drawContext);
                drawContext.GetCanvas().AddXObject(img.GetXObject(), GetOccupiedAreaBBox());
                drawContext.GetCanvas().Stroke();

                UnitValue fontSizeUv = GetPropertyAsUnitValue(Property.FONT_SIZE);
                float x = GetOccupiedAreaBBox().GetX() + wPct * GetOccupiedAreaBBox().GetWidth();
                float y = GetOccupiedAreaBBox().GetY() + hPct *
                          (GetOccupiedAreaBBox().GetHeight() - (fontSizeUv.IsPointValue()
                               ? fontSizeUv.GetValue()
                               : 12f) * 1.5f);
                new Document(drawContext.GetDocument()).ShowTextAligned(content, x, y, alignment);
            }
        }
    }
}