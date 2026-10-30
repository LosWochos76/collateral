using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ToDoUI.Misc;

public class BackgroundEmailSender : BackgroundService
{
    private ILogger<BackgroundEmailSender> logger;
    private EmailQueue mailQueue;
    private MailSettings settings;

    public BackgroundEmailSender(ILogger<BackgroundEmailSender> logger, EmailQueue mailQueue, IOptions<MailSettings> settings)
    {
        this.logger = logger;
        this.mailQueue = mailQueue;
        this.settings = settings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (mailQueue.HasMessages)
            {
                var message = mailQueue.Dequeue();
                if (!await SendMailAsync(message, stoppingToken))
                    mailQueue.Enqueue(message);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task<bool> SendMailAsync(MimeMessage message, CancellationToken stoppingToken)
    {
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, settings.SecureSocketOptions, stoppingToken);
            if (!string.IsNullOrEmpty(settings.Username))
                await client.AuthenticateAsync(settings.Username, settings.Password ?? string.Empty, stoppingToken);
            await client.SendAsync(message, stoppingToken);
            await client.DisconnectAsync(true, stoppingToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not send mail!");
            return false;
        }

        return true;
    }
}
