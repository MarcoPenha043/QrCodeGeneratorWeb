using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class GenerateQRCodeModel : PageModel
{
    [BindProperty]
    public string InputText { get; set; }

    public string QrCodeImageBase64 { get; set; }

    public void OnGet() { }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(InputText))
        {
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrData = qrGenerator.CreateQrCode(InputText, QRCodeGenerator.ECCLevel.Q))
            {
                using (var qrCode = new QRCoder.QRCode(qrData))
                using (var bitmap = qrCode.GetGraphic(20))
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    QrCodeImageBase64 = Convert.ToBase64String(stream.ToArray());
                }
            }
        }
    }
}