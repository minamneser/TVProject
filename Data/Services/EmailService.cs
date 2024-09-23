using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailService : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        var smtpServer = emailSettings["SmtpServer"];
        var smtpPort = int.Parse(emailSettings["SmtpPort"]);
        var smtpUser = emailSettings["SmtpUser"];
        var smtpPass = emailSettings["SmtpPassword"];

        var emailMessage = new MimeMessage
        {
            Subject = subject,
            Body = new TextPart("html") { Text = message },
            From = { new MailboxAddress("YourApp", smtpUser) },
            To = { new MailboxAddress("", toEmail) }
        };

        using (var client = new SmtpClient())
        {           
            client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            await client.ConnectAsync(smtpServer, smtpPort, smtpPort == 465 ? MailKit.Security.SecureSocketOptions.SslOnConnect : MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpUser, smtpPass);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
