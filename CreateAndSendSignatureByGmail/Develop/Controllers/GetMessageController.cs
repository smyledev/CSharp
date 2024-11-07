using System.Collections.Generic;
using CryptLab1WebAppMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CryptLab1WebAppMVC.Controllers
{
    public class GetMessageController : Controller
    {
        private readonly ILogger<GetMessageController> _logger;
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment _appEnvironment;
        public string receiverDirPath;
        public string emailDir;
        public string keysDir;

        public GetMessageController(ILogger<GetMessageController> logger, Microsoft.AspNetCore.Hosting.IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            receiverDirPath = _appEnvironment.WebRootPath + "/FilesOfReceiver/";
            emailDir = "EmailAndPassword/";
            keysDir = "KeysAndSignature/";
        }

        [HttpGet("/get_email_message")]
        public IActionResult GetMessagePage(string statusOfAccess = "", string statusOfCheckSignature = "", string statusOfChangingMessage = "", string receiverEmail = "", string applicationPassword = "")
        {
            ViewBag.ReceiverEmail = receiverEmail;
            ViewBag.ApplicationPassword = applicationPassword;
            ViewBag.StatusAccess = statusOfAccess;
            ViewBag.StatusOfCheckSignature = statusOfCheckSignature;

            if (statusOfAccess == "accessAllowed")
            {
                // Функция GetMessage читатет сообщение и сохраняет текст сообщения и вложения (откртый ключ и подпись) в каталоге своего приложения
                Dictionary<string, string> savedInfo = EmailService.GetMessage(receiverDirPath, emailDir, keysDir);

                ViewBag.Subject = savedInfo["subject"];
                ViewBag.Email = savedInfo["email"];
                ViewBag.Date = savedInfo["date"];
                ViewBag.Message = savedInfo["textMessage"];
            }

            if (statusOfChangingMessage == "next")
            {
                if (EmailService.numberOfMessage > 3)
                    EmailService.numberOfMessage = 0;
                else
                    EmailService.numberOfMessage++;

                ViewBag.StatusAccess = "accessAllowed";
                ViewBag.StatusOfChangingMessage = statusOfChangingMessage;
            }

            else if (statusOfChangingMessage == "prev")
            {
                if (EmailService.numberOfMessage < 1)
                    EmailService.numberOfMessage = 4;
                else
                    EmailService.numberOfMessage--;

                ViewBag.StatusAccess = "accessAllowed";
                ViewBag.StatusOfChangingMessage = statusOfChangingMessage;
            }

            return View();
        }

        [HttpPost]
        public IActionResult GetAccessToEmailForReceive()
        {
            var context = HttpContext;
            var form = context.Request.Form;

            string receiverEmail = form["receiverEmail"];
            string applicationPassword = form["applicationPassword"];

            if (EmailService.AccessToEmailAllowed(receiverDirPath, receiverEmail, emailDir, applicationPassword))
                return RedirectToAction("GetMessagePage", "GetMessage", new { statusOfAccess = "accessAllowed", receiverEmail, applicationPassword });
            else
                return RedirectToAction("GetMessagePage", "GetMessage", new { statusOfAccess = "accessDenied", receiverEmail, applicationPassword });
        }

        [HttpPost]
        public IActionResult CheckSignature()
        {
            var context = HttpContext;
            var form = context.Request.Form;

            string receiverEmail = form["receiverEmail"];
            string applicationPassword = form["applicationPassword"];

            if (DigitalSignature.VerifyData(receiverDirPath, keysDir))
                return RedirectToAction("GetMessagePage", "GetMessage", new { statusOfAccess = "accessAllowed", statusOfCheckSignature = "success", receiverEmail, applicationPassword });
            else
                return RedirectToAction("GetMessagePage", "GetMessage", new { statusOfAccess = "accessAllowed", statusOfCheckSignature = "error", receiverEmail, applicationPassword });
        }
    }
}