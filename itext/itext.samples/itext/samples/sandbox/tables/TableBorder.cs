using System;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Samples.Sandbox.Tables
{
    public class TableBorder
    {
        public static readonly string DEST = "results/sandbox/tables/tables_border.pdf";

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new TableBorder().ManipulatePdf(DEST);
        }

        private void ManipulatePdf(String dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();

            for (int aw = 0; aw < 16; aw++)
            {
                table.AddCell(new Cell().Add(new Paragraph("hi")).SetBorder(Border.NO_BORDER));
            }

            // Notice that one should set renderer after cells are added to the table
            table.SetNextRenderer(new TableBorderRenderer(table));

            doc.Add(table);

            doc.Close();
        }

        private class TableBorderRenderer : TableRenderer
        {
            public TableBorderRenderer(Table modelElement)
                : base(modelElement)
            {
            }            
            
            // If renderer overflows on the next area, iText uses getNextRender() method to create a renderer for the overflow part.
            // If getNextRenderer isn't overriden, the default method will be used and thus a default rather than custom
            // renderer will be created
            public override IRenderer GetNextRenderer()
            {
                return new TableBorderRenderer((Table) modelElement);
            }

            protected override void DrawBorders(DrawContext drawContext)
            {
                Rectangle rect = GetOccupiedAreaBBox();
                drawContext.GetCanvas()
                    .SaveState()
                    .Rectangle(rect.GetLeft(), rect.GetBottom(), rect.GetWidth(), rect.GetHeight())
                    .Stroke()
                    .RestoreState();
            }
        }
    }
}