/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2020 iText Group NV
    Authors: iText Software.

    For more information, please contact iText Software at this address:
    sales@itextpdf.com
 */

using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Samples.Sandbox.Images
{
    public class ImageOnRotatedPage
    {
        public static readonly String DEST = "results/sandbox/images/image_on_rotated_page.pdf";

        public static readonly String IMAGE = "../../../resources/img/cardiogram.png";

        public static void Main(String[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new ImageOnRotatedPage().ManipulatePdf(DEST);
        }

        protected void ManipulatePdf(String dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc, PageSize.A4.Rotate());

            Image img = new Image(ImageDataFactory.Create(IMAGE));
            img.ScaleToFit(770, 523);
            float offsetX = (770 - img.GetImageScaledWidth()) / 2;
            float offsetY = (523 - img.GetImageScaledHeight()) / 2;
            img.SetFixedPosition(36 + offsetX, 36 + offsetY);
            doc.Add(img);

            doc.Close();
        }
    }
}