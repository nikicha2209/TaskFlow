using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace TaskFlow.Web.Infrastructure.Identity;

public sealed class DevelopmentEmailSender(
    IWebHostEnvironment environment,
    ILogger<DevelopmentEmailSender> logger) : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (!environment.IsDevelopment())
        {
            throw new InvalidOperationException(
                "Configure a production email sender before using Identity email features.");
        }

        logger.LogInformation(
            "Development email to {Email} with subject {Subject}: {HtmlMessage}",
            email,
            subject,
            htmlMessage);

        return Task.CompletedTask;
    }
}
