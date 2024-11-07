using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CryptLab1WebAppMVC.Controllers
{
    public class CreateSignatureController : Controller
    {
        private readonly ILogger<CreateSignatureController> _logger;
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment _appEnvironment;
        public string senderDirPath;
        public string keysDir;

        public CreateSignatureController(ILogger<CreateSignatureController> logger, Microsoft.AspNetCore.Hosting.IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            senderDirPath = _appEnvironment.WebRootPath + "/FilesOfSender/";
            keysDir = "KeysAndSignature/";
        }

        public IActionResult CreateSignaturePage(string status = "", string text = "")
        {
            if (status == "keysAreCreated")
                ViewBag.ActionType = status;

            else if (status == "signatureIsCreated")
            {
                ViewBag.ActionType = status;
                ViewBag.Text = text;
            }

            return View();
        }

        public IActionResult CreateKeys()
        {
            DigitalSignature.CreateKeys(senderDirPath, keysDir);

            return RedirectToAction("CreateSignaturePage", "CreateSignature", new { status = "keysAreCreated" });
        }

        [HttpPost]
        public IActionResult CreateSignatureForm()
        {
            var context = HttpContext;
            var form = context.Request.Form;

            if (form.ContainsKey("text"))
            {
                string text = form["text"];
                DigitalSignature.CreateSignature(senderDirPath, keysDir, text);

                return RedirectToAction("CreateSignaturePage", "CreateSignature", new { status = "signatureIsCreated", text });
            }

            else
                return RedirectToAction("CreateSignaturePage", "CreateSignature", new { status = "error" });
        }
    }
}