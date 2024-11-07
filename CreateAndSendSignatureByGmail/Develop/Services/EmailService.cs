using OpenPop.Pop3;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CryptLab1WebAppMVC.Services
{
    public class EmailService
    {
        public static int numberOfMessage = 0;

        public static bool AccessToEmailAllowed(string typeUserPath, string email, string emailDir, string applicationPassword)
        {
            try
            {
                Pop3Client client = new Pop3Client();
                client.Connect("pop.gmail.com", 995, true);
                client.Authenticate(email, applicationPassword);

                byte[] emailBytes = Encoding.Default.GetBytes(email);
                byte[] applicationPasswordBytes = Encoding.Default.GetBytes(applicationPassword);

                System.IO.File.WriteAllBytes(typeUserPath + emailDir + "Email.txt", emailBytes);
                System.IO.File.WriteAllBytes(typeUserPath + emailDir + "ApplicationPassword.txt", applicationPasswordBytes);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Dictionary<string, string> GetMessage(string receiverDirPath, string emailDir, string keysDir)
        {
            Pop3Client client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);

            byte[] receiverEmail = System.IO.File.ReadAllBytes(receiverDirPath + emailDir + "Email.txt");
            byte[] applicationPassword = System.IO.File.ReadAllBytes(receiverDirPath + emailDir + "ApplicationPassword.txt");

            client.Authenticate(Encoding.Default.GetString(receiverEmail), Encoding.Default.GetString(applicationPassword));

            int messageCount = client.GetMessageCount() - numberOfMessage;
            string subject = client.GetMessage(messageCount).Headers.Subject;
            string date = client.GetMessage(messageCount).Headers.DateSent.ToString("yyyy-MM-dd");
            string senderEmail = client.GetMessage(messageCount).Headers.From.Address;

            var attachments = client.GetMessage(messageCount).FindAllAttachments();
            foreach (var attachment in attachments)
            {
                attachment.Save(new System.IO.FileInfo(System.IO.Path.Combine(receiverDirPath + keysDir, attachment.FileName)));
            }

            var HtmlMessage = client.GetMessage(messageCount).FindAllTextVersions();
            string textMessage = "";

            if (HtmlMessage.FirstOrDefault() != null)
            {
                textMessage = HtmlMessage[0].GetBodyAsText();
                byte[] msg = Encoding.Default.GetBytes(textMessage);
                System.IO.File.WriteAllBytes(receiverDirPath + keysDir + "Data.txt", msg);
            }

            Dictionary<string, string> savedInfo = new Dictionary<string, string>();
            savedInfo.Add("subject", subject);
            savedInfo.Add("date", date);
            savedInfo.Add("email", senderEmail);
            savedInfo.Add("textMessage", textMessage);

            return savedInfo;
        }

        public static void SendEmailMessage(string senderEmail, string applicationPassword, string receiverEmail, string senderDirPath, string keysDir)
        {
            // Отправка по почте
            MailAddress from = new MailAddress(senderEmail);
            MailAddress to = new MailAddress(receiverEmail);

            MailMessage mailMessage = new MailMessage(from, to);

            string Subject = "Verification of digital signature";
            mailMessage.Subject = Subject;

            byte[] message = System.IO.File.ReadAllBytes(senderDirPath + keysDir + "Data.txt");
            Encoding.Default.GetString(message);
            mailMessage.Body = Encoding.Default.GetString(message);

            // Вложения к письму - публичный ключ и подпись
            mailMessage.Attachments.Add(new Attachment(senderDirPath + keysDir + "PublicKey.db"));
            mailMessage.Attachments.Add(new Attachment(senderDirPath + keysDir + "Signature.db"));

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(senderEmail, applicationPassword);
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);
        }

    }
}
