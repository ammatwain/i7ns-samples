using System;
using System.IO;
using iText.Barcodes;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;

namespace iText.Samples.Sandbox.Barcodes 
{
    public class StampBarcode 
    {
        public static readonly String DEST = "results/sandbox/barcodes/stamp_barcode.pdf";
        public static readonly String SRC = "../../../resources/pdfs/superman.pdf";

        public static void Main(String[] args) 
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            
            new StampBarcode().ManipulatePdf(DEST);
        }

        protected void ManipulatePdf(String dest) 
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(SRC), new PdfWriter(dest));
            
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++) 
            {
                PdfPage pdfPage = pdfDoc.GetPage(i);
                Rectangle pageSize = pdfPage.GetPageSize();
                float x = pageSize.GetLeft() + 10;
                float y = pageSize.GetTop() - 50;
                BarcodeEAN barcode = new BarcodeEAN(pdfDoc);
                barcode.SetCodeType(BarcodeEAN.EAN8);
                barcode.SetCode(CreateBarcodeNumber(i));
                
                PdfFormXObject barcodeXObject = barcode.CreateFormXObject(ColorConstants.BLACK, ColorConstants.BLACK, pdfDoc);
                PdfCanvas over = new PdfCanvas(pdfPage);
                over.AddXObjectAt(barcodeXObject, x, y);
            }
            
            pdfDoc.Close();
        }

        private static String CreateBarcodeNumber(int i) 
        {
            String barcodeNumber = i.ToString();
            barcodeNumber = "00000000".Substring(barcodeNumber.Length) + barcodeNumber;
            
            return barcodeNumber;
        }
    }
}
