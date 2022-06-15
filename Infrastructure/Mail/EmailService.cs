using Application.Contracts.Infrastructure;
using Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Mail;

public class EmailService:IEmailService
{
    private EmailSettings _emailSettings;
    private ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _logger = logger;
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        _logger.LogInformation("Email sent");

        if (response.StatusCode is System.Net.HttpStatusCode.Accepted or System.Net.HttpStatusCode.OK)
            return true;

        _logger.LogError("Email sending failed");

        return false;
    }
}