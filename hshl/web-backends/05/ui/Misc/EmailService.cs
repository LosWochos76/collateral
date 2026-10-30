using Common.Misc;
using Common.Models;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ToDoUI.Misc;

public class EmailService
{
    private ILogger<EmailService> logger;
    private IUserRepository userRepository;
    private EmailQueue emailQueue;
    private PasswordHelper passwordHelper;
    private GeneralSettings settings;
    private MailSettings mailSettings;

    public EmailService(ILogger<EmailService> logger, IUserRepository userRepository, EmailQueue emailQueue, 
        PasswordHelper passwordHelper, IOptions<GeneralSettings> settings, IOptions<MailSettings> mailSettings)
    {
        this.logger = logger;
        this.userRepository = userRepository;
        this.emailQueue = emailQueue;
        this.passwordHelper = passwordHelper;
        this.settings = settings.Value;
        this.mailSettings = mailSettings.Value;
    }

    public void SendPasswordResetMail(User user)
    {
        user.PasswordResetToken = passwordHelper.GenerateToken();
        user.PasswordResetTokenExpires = DateTime.UtcNow.AddMinutes(settings.PasswordResetTokenLifetimeMinutes);
        userRepository.Update(user);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("ToDo", mailSettings.FromMailAddress));
        message.To.Add(new MailboxAddress("", user.EMail));
        message.Subject = "ToDo - Reset Password";
        message.Body = new TextPart("plain") {
            Text = $"Reset password: {settings.HomeUrl}Authentication/ResetPassword/{user.PasswordResetToken}" };
        emailQueue.Enqueue(message);
    }

    public void SendRegistrationMail(User user)
    {
        
    }
}
