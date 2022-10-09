using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QR.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       TicketDetail tick = new TicketDetail();
        

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy(TicketDetail ticketDetail)

        {
            var filepath = "C:\\Users\\yash.adhikari\\source\\repos\\QR\\QR\\wwwroot\\Qr\\";
            var jsonString = JsonConvert.SerializeObject(ticketDetail);
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save( ms, ImageFormat.Jpeg);
                    var qrCodeImage = "data:image/Png;base64," + Convert.ToBase64String(ms.ToArray());
                    ViewBag.qrCodeImage = qrCodeImage;
                    FileStream fs = new FileStream(string.Format(@"{0}{1}.Jpeg", filepath,DateTime.Now.Ticks), FileMode.Create, FileAccess.Write);
                    if (fs.CanWrite)
                    {
                        byte[] Img = ms.ToArray();
                        fs.Write(Img,0,Img.Length);
                    }
                    fs.Flush();
                    fs.Close();

                    ViewBag.path = string.Format(@"{0}{1}.Jpeg", filepath, DateTime.Now.Ticks);


                }
            }




            return View(ticketDetail);
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
