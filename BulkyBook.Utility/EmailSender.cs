using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //mail function is not working


            //  var emailToSend = new MimeMessage();
            //  emailToSend.From.Add(MailboxAddress.Parse("hello@bulkybook.com"));
            //  emailToSend.From.Add(MailboxAddress.Parse(email));
            //  emailToSend.Subject=subject;
            //  emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage };

            //  //send email
            //using(var emailClient=new SmtpClient())
            //  {
            //      emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //      emailClient.Authenticate("nettest291@gmail.com", "test@245");
            //      emailClient.SendAsync(emailToSend);
            //      emailClient.Disconnect(true);
            //  }

            //so use fake implmentation
            return Task.CompletedTask;
        }
    }
}
