using Elements.DTO;
using PDFManager.Core;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QRCodeManager.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PDFManager.Common
{
   public class PDFGenerator
    {
        public MemoryStream generatePDFInMemory()
        {
            // Create PDF document and assign a page to it
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();

            //Create graphic object for drawing string and images
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Arial", 10);

            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height),
#pragma warning disable CS0618 // Type or member is obsolete
             XStringFormat.Center);
#pragma warning restore CS0618 // Type or member is obsolete

            // Save doc on memory stream so that PDF can be generated on the fly
            MemoryStream ms = new MemoryStream();
            doc.Save(ms, true);

            MemoryStream pdfStream = new MemoryStream(ms.ToArray());
            return pdfStream;
        }

        public void sendEmail()
        {
            MailMessage mm = generateMessage();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "qian0053@easv365.dk";
            NetworkCred.Password = "sgx77apm";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);

        }

        private MailMessage generateMessage()
        {
            MailMessage mm = new MailMessage("qian0053@easv365.dk", "melissamaqianhua@gmail.com");
            mm.Subject = "Test PDF";
            mm.Body = "Test PDF Attachment";
            mm.Attachments.Add(new Attachment(generatePDFInMemory(), "test.Pdf", "application/pdf"));
            mm.IsBodyHtml = true;
            return mm;
        }

        public void formatePDF(EventDTO eventSingle)
        {
            
            Bitmap qr = QRGenerator.CreateQRCodeAsBitmap(eventSingle.Id);
            // Bitmap logo = new Bitmap (@"\\Mac\\Home\\Desktop\\project\\test\\easvtickets\\PDFManager\\Core\\image001.jpg");
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            Bitmap logo = (Bitmap)logoEASV.EasvLogo;
            //Create graphic object for drawing string and images

            XImage img = XImage.FromGdiPlusImage(qr);
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XImage img2 = XImage.FromGdiPlusImage(logo);
            XFont font = new XFont("Arial", 14);

            //            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height),
            //#pragma warning disable CS0618 // Type or member is obsolete
            //                         XStringFormat.Center);
            //#pragma warning restore CS0618 // Type or member is obsolete

            gfx.DrawString("Hi, Dear XX:", font, XBrushes.Black,
new XRect(40, 0, page.Width - 400, 100), XStringFormats.Center);
            gfx.DrawString("This is your ticket.", font, XBrushes.Black,
new XRect(40, 0, page.Width - 300, 150), XStringFormats.Center);

            gfx.DrawString("You have bought ticket of "+eventSingle.Name+".", font, XBrushes.Black,
new XRect(40, 0, page.Width - 300, 200), XStringFormats.Center);
            //gfx.DrawLine(XPens.Black, 40, 130, page.Width - 40, 130);
            gfx.DrawString("The price is " + eventSingle.Price + ".", font, XBrushes.Black,
new XRect(40, 0, page.Width - 300, 250), XStringFormats.Center);
            gfx.DrawLine(XPens.Black, 40, 150, page.Width - 40, 150);

            gfx.DrawImage(img, 80, 180);

            gfx.DrawLine(XPens.Black, 40, 600, page.Width - 40, 600);

            gfx.DrawString("Med venlig hilsen", font, XBrushes.Black,
new XRect(40, 0, page.Width - 300, 1250), XStringFormats.Center);
            gfx.DrawString("Erhvervsakademi Sydvest", font, XBrushes.Black,
new XRect(40, 0, page.Width - 300, 1300), XStringFormats.Center);
            gfx.DrawImage(img2, 80, 680);
            gfx.DrawString("Spangsbjerg Kirkevej 103 - 6700 Esbjerg", font, XBrushes.Black,
new XRect(40, 0, page.Width - 300, 1500), XStringFormats.Center);

            string filename = "C:\\Users\\huaye\\Desktop\\HelloWorld.pdf";
            doc.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
