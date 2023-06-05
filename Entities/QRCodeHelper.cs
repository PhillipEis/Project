using QRCoder;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using static QRCoder.PayloadGenerator;
using UIMock.Entities;

namespace UIMock
{
    public class QRCodeHelper
    {
        private static byte[] GenerateQRCode(string text, int pixelSize = 60, int margin = 4, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.Q)
        {
            // Create QR code
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(text, eccLevel);
            var qrCode = new PngByteQRCode(qrCodeData);

            // Get PNG image data
            var pngData = qrCode.GetGraphic(pixelSize, true);

            // Return PNG data as byte array
            return pngData;

        }

        public async Task<ImageSource> LoadQRCode()
        {
            string unixTimestamp = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            byte[] aesKey = new byte[] { 50, 150, 20, 240, 55, 38, 178, 142, 103, 73, 168, 157, 235, 124, 243, 97, 118, 13, 203, 175, 29, 25, 106, 138, 236, 149, 49, 191, 1, 81, 234, 54 };
            byte[] aesIV = new byte[] { 243, 21, 136, 50, 4, 15, 143, 58, 54, 122, 22, 40, 82, 146, 232, 148 };

            var encryptedBytes = EncryptData.Encrypt($"{unixTimestamp}-Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sit amet odio convallis nisi finibus dapibus. Pellentesque fringilla sed felis sed faucibu", aesKey, aesIV);

            var encryptedBase64 = Convert.ToBase64String(encryptedBytes);

            var qrCodeBytes = QRCodeHelper.GenerateQRCode(encryptedBase64);

            var imageSource = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

            return imageSource;
        }
    }
}
