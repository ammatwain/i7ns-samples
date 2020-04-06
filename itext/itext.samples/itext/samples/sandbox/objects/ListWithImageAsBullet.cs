﻿/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

For more information, please contact iText Software at this address:
sales@itextpdf.com
*/

using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Samples.Sandbox.Objects
{
    public class ListWithImageAsBullet
    {
        public static readonly string DEST = "results/sandbox/objects/list_with_image_bullet.pdf";
        public static readonly string IMG = "../../../resources/img/bulb.gif";

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new ListWithImageAsBullet().ManipulatePdf(DEST);
        }

        protected void ManipulatePdf(string dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            ImageData image = ImageDataFactory.Create(IMG);
            PdfImageXObject xObject = new PdfImageXObject(image);

            List list = new List()
                .SetListSymbol(new Image(xObject))
                .Add("Hello World")
                .Add("This is a list item with a lot of text. It will certainly take more than one line." +
                     " This shows that the list item is indented and that the image is used as bullet.")
                .Add("This is a test");
            doc.Add(list);

            doc.Close();
        }
    }
}