using MailKit.Security;

namespace ToDoUI.Misc;

public class MailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.Auto;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string FromMailAddress { get; set; } = string.Empty;
}
