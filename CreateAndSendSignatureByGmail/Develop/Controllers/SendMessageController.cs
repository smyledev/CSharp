using CryptLab1WebAppMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CryptLab1WebAppMVC.Controllers
{
    public class SendMessageController : Controller
    {
        private readonly ILogger<SendMessageController> _logger;
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment _appEnvironment;
        public string senderDirPath;
        public string emailDir;
        public string keysDir;

        public SendMessageController(ILogger<SendMessageController> logger, Microsoft.AspNetCore.Hosting.IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            senderDirPath = _appEnvironment.WebRootPath + "/FilesOfSender/";
            emailDir = "EmailAndPassword/";
            keysDir = "KeysAndSignature/";
        }

        [HttpGet("/send_email_message")]
        public IActionResult SendMessagePage(string statusOfAccess = "", string statusOfSend = "", string senderEmail = "", string applicationPassword = "", string receiverEmail = "")
        {
            ViewBag.StatusAccess = statusOfAccess;
            ViewBag.StatusOfSend = statusOfSend;
            ViewBag.SenderEmail = senderEmail;
            ViewBag.ApplicationPassword = applicationPassword;
            ViewBag.ReceiverEmail = receiverEmail;

            return View();
        }

        [HttpPost]
        public IActionResult GetAccessToEmailForSend()
        {
            var context = HttpContext;
            var form = context.Request.Form;

            string senderEmail = form["senderEmail"];
            string applicationPassword = form["applicationPassword"];

            if (EmailService.AccessToEmailAllowed(senderDirPath, senderEmail, emailDir, applicationPassword))
                return RedirectToAction("SendMessagePage", "SendMessage", new { statusOfAccess = "accessAllowed", senderEmail, applicationPassword });
            else
                return RedirectToAction("SendMessagePage", "SendMessage", new { statusOfAccess = "accessDenied", senderEmail, applicationPassword });
        }


        [HttpPost]
        public IActionResult SendingEmailMessageWithSignature()
        {
            var context = HttpContext;
            var form = context.Request.Form;

            string senderEmail = form["senderEmail"];
            string applicationPassword = form["applicationPassword"];

            string receiverEmail;

            if (System.IO.File.Exists(senderDirPath + keysDir + "Data.txt"))
            {
                if (form.ContainsKey("receiverEmail"))
                {
                    receiverEmail = form["receiverEmail"];

                    EmailService.SendEmailMessage(senderEmail, applicationPassword, receiverEmail, senderDirPath, keysDir);

                    ViewBag.TextMessage = "";
                    ViewBag.Receiver = "";

                    return RedirectToAction("SendMessagePage", "SendMessage", new { statusOfAccess = "accessAllowed", statusOfSend = "messageWasSent", senderEmail, applicationPassword, receiverEmail });
                }

                else
                    return RedirectToAction("SendMessagePage", "SendMessage", new { statusOfAccess = "accessAllowed", statusOfSend = "error", senderEmail, applicationPassword });
            }

            else
                return RedirectToAction("SendMessagePage", "SendMessage", new { statusOfAccess = "accessAllowed", statusOfSend = "errorWithMessage", senderEmail, applicationPassword });
        }
    }
}