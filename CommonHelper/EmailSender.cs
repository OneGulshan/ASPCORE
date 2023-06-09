using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace CommonHelper
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //throw new NotImplementedException(); // hamein yahan throw nahi karna hai hamein yahan return karna hai as a dummy task bec ham yahan email sent nahi kar rahe ham yahan dummy bana rahe hain isko
            //return Task.CompletedTask;
            var toemail = new MimeMessage(); // This is a MimeKit Part
            toemail.From.Add(MailboxAddress.Parse("MyGmailId")); // jhan se mail jati hai vo hamne yhan set kr dia hai
            toemail.To.Add(MailboxAddress.Parse(email));
            toemail.Subject = subject;
            toemail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage }; // mimikit me hamare pass mime msg ata hai

            using(var emailClient = new SmtpClient()) // This is a Mailkit Part // emailClient <- this is our Smtp Client here, SmtpClient se agar ham conect kar rahe hain to hamin PostAddress(gmail=587) ka Port no. chaiye
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls); // sath me hamein authentication chaiye(login + pass), sath me ham MimeKit ke throw security provide kara sakte hain tls ki
                emailClient.Authenticate("gulshankumar.mailid01@gmail.com", "Gulshan@123");
                emailClient.SendAsync(toemail); // yahan hamne apna email sent kia hai
                emailClient.Disconnect(true); // or yahan hamne apne Connected obj ko disconnect kia hai
            }
            return Task.CompletedTask;
        }
    }
}
//Install MailKit & MimeKit for Mail Sending, MimeKit req for Mail Form accessing/using, MailKit For Smtp Client using
//Agar Authenticated User Reg karega to use 1 message bhej denge as a Tocken jo ki Identity Server ke throw kia jata hai, agr ham email ki settings kar deta hain to auto hame vo msg sent kr dia jaega
//Secure Less app turns on req in Gmail for sent a mail