using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Toodo.Infrastructure.Identity;

namespace Toodo.Application.Emails;

internal sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender, IEmailSender<ApplicationUser>
{
    private readonly ILogger _logger = logger;

    public Task SendEmailAsync(string? email, string subject, string htmlMessage)
    {
        _logger.LogWarning("{Email} {Subject} {HtmlMessage}", email, subject, htmlMessage);

        return Task.CompletedTask;
    }

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        return SendEmailAsync(user.Email, "Confirm your email", confirmationLink);
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        return SendEmailAsync(user.Email, "Reset your password", resetLink);
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        return SendEmailAsync(user.Email, "Reset your password", resetCode);
    }
}