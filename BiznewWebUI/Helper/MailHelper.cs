using BiznewWebUI.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace BiznewWebUI.Helper
{
    public class MailHelper
    {
       

        public bool SendEmail(string userEmail, string confirmationLink)
        {
            HtmlContentBuilder contentBuilder = new HtmlContentBuilder();
            var code = contentBuilder.AppendHtml
                 ($"<a href={confirmationLink}></a>");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("muradovtahir01@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));
            
            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true; 
            mailMessage.Body = confirmationLink ;
         
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("muradovtahir01@gmail.com", "4575865t");                    
            client.Host = "smtp.office365.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
