using System;
using System.Drawing;
using QRCoder;
using QRCodeManager.Core;

namespace QRCodeManager.Common
{
    public static class QRGenerator 
    {
        public static string CreateQRCodeAsBase64String(int ticketId)
        {
            if (ticketId < 1) throw new ArgumentOutOfRangeException();
            var qrCodeByteArray = CreateQRCodeAsByteArray(ticketId);
            var qrCodeBase64String = Convert.ToBase64String(qrCodeByteArray);
            return qrCodeBase64String;
        }

        public static Bitmap CreateQRCodeAsBitmap(int ticketId)
        {
            if (ticketId < 1) throw new ArgumentOutOfRangeException();
            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(ticketId.ToString(), QRCodeGenerator.ECCLevel.H, true);
            var qrCode = new QRCode(qrData);
            var logo = ImageResources.EASV_Logo;
            var qrCodeBitmap = qrCode.GetGraphic(20, Color.Black, Color.White, logo);
            return qrCodeBitmap;
        }

        public static byte[] CreateQRCodeAsByteArray(int ticketId)
        {
            if (ticketId < 1) throw new ArgumentOutOfRangeException();
            var qrCodeBitmap = CreateQRCodeAsBitmap(ticketId);
            var converter = new ImageConverter();
            var qrCodeByteArray = (byte[]) converter.ConvertTo(qrCodeBitmap, typeof(byte[]));
            return qrCodeByteArray;
        }
    }
}
