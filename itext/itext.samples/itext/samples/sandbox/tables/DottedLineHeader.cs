using System;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Samples.Sandbox.Tables
{
    public class DottedLineHeader
    {
        public static readonly string DEST = "results/sandbox/tables/dotted_line_header.pdf";

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new DottedLineHeader().ManipulatePdf(DEST);
        }

        private void ManipulatePdf(string dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

            // Draws dotted line border in table
            table.SetNextRenderer(new DottedHeaderTableRenderer(table, new Table.RowRange(0, 1)));

            Style noBorder = new Style().SetBorder(Border.NO_BORDER);

            table.AddHeaderCell(new Cell().Add(new Paragraph("A1")).AddStyle(noBorder));
            table.AddHeaderCell(new Cell().Add(new Paragraph("A2")).AddStyle(noBorder));
            table.AddHeaderCell(new Cell().Add(new Paragraph("A3")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("B1")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("B2")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("B3")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("C1")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("C2")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("C3")).AddStyle(noBorder));

            doc.Add(table);
            doc.Add(new Paragraph("Cell event"));

            table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph("A1")).AddStyle(noBorder);

            // Draws dotted line border in cell
            cell.SetNextRenderer(new DottedHeaderCellRenderer(cell));
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("A2")).AddStyle(noBorder);
            cell.SetNextRenderer(new DottedHeaderCellRenderer(cell));
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("A3")).AddStyle(noBorder);
            cell.SetNextRenderer(new DottedHeaderCellRenderer(cell));
            table.AddCell(cell);

            table.AddCell(new Cell().Add(new Paragraph("B1")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("B2")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("B3")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("C1")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("C2")).AddStyle(noBorder));
            table.AddCell(new Cell().Add(new Paragraph("C3")).AddStyle(noBorder));

            doc.Add(table);

            doc.Close();
        }

        private class DottedHeaderTableRenderer : TableRenderer
        {
            public DottedHeaderTableRenderer(Table modelElement, Table.RowRange rowRange)
                : base(modelElement, rowRange)
            {
            }            
            
            // If renderer overflows on the next area, iText uses getNextRender() method to create a renderer for the overflow part.
            // If getNextRenderer isn't overriden, the default method will be used and thus a default rather than custom
            // renderer will be created
            public override IRenderer GetNextRenderer()
            {
                return new DottedHeaderTableRenderer((Table) modelElement, rowRange);
            }

            public override void DrawChildren(DrawContext drawContext)
            {
                base.DrawChildren(drawContext);
                PdfCanvas canvas = drawContext.GetCanvas();
                Rectangle headersArea = headerRenderer.GetOccupiedArea().GetBBox();
                
                canvas.SetLineDash(3f, 3f);
                canvas.MoveTo(headersArea.GetLeft(), headersArea.GetTop());
                canvas.LineTo(headersArea.GetRight(), headersArea.GetTop());
                canvas.MoveTo(headersArea.GetLeft(), headersArea.GetBottom());
                canvas.LineTo(headersArea.GetRight(), headersArea.GetBottom());
                canvas.Stroke();
            }
        }

        private class DottedHeaderCellRenderer : CellRenderer
        {
            public DottedHeaderCellRenderer(Cell modelElement)
                : base(modelElement)
            {
            }            
            
            // If renderer overflows on the next area, iText uses getNextRender() method to create a renderer for the overflow part.
            // If getNextRenderer isn't overriden, the default method will be used and thus a default rather than custom
            // renderer will be created
            public override IRenderer GetNextRenderer()
            {
                return new DottedHeaderCellRenderer((Cell) modelElement);
            }

            public override void Draw(DrawContext drawContext)
            {
                base.Draw(drawContext);
                PdfCanvas canvas = drawContext.GetCanvas();
                Rectangle bbox = GetOccupiedArea().GetBBox();

                canvas.SetLineDash(3f, 3f);
                canvas.MoveTo(bbox.GetLeft(), bbox.GetBottom());
                canvas.LineTo(bbox.GetRight(), bbox.GetBottom());
                canvas.MoveTo(bbox.GetLeft(), bbox.GetTop());
                canvas.LineTo(bbox.GetRight(), bbox.GetTop());
                canvas.Stroke();
            }
        }
    }
}