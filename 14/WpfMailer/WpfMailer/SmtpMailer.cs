using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailer
{
    public class SslSmtpMailer
    {
        private string host;
        private int port;
        private string username;
        private string password;

        public SslSmtpMailer(string host, int port, string username, string password)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        public Task<bool> SendMailAsync(string to, string subject, string message)
        {
            return Task.Factory.StartNew(() => SendMail(to, subject, message));
        }

public bool SendMail(string to, string subject, string message)
{
    using (TcpClient mail = new TcpClient())
    {
        mail.Connect(host, port);

        using (SslStream sslStream = new SslStream(mail.GetStream()))
        {
            sslStream.AuthenticateAsClient(host);

            if (!ReadString(sslStream).Contains("ESMTP"))
                return false;

            WriteString(sslStream, "EHLO example.net");
            if (!ReadString(sslStream).Contains("AUTH LOGIN"))
                return false;

            WriteString(sslStream, "AUTH LOGIN");
            ReadString(sslStream);
            WriteStringBase64(sslStream, username);
            ReadString(sslStream);
            WriteStringBase64(sslStream, password);

            if (!ReadString(sslStream).ToUpper().Contains("ACCEPTED"))
                return false;

WriteString(sslStream, "MAIL FROM:<" + username + ">");
if (!ReadString(sslStream).Contains("OK"))
    return false;

            WriteString(sslStream, "RCPT TO:<" + to + ">");
            if (!ReadString(sslStream).Contains("OK"))
                return false;

            WriteString(sslStream, "DATA");
            if (!ReadString(sslStream).ToUpper().Contains("GO AHEAD"))
                return false;

            WriteString(sslStream, "From: <" + username + ">\r\n" +
                "To: <" + to + ">\r\n" +
                "Subject: " + subject + "\r\n" +
                "\r\n" + message + "\r\n.\r\n");

            if (!ReadString(sslStream).Contains("OK"))
                return false;

            WriteString(sslStream, "QUIT");
        }
    }

    return true;
}

private string ReadString(SslStream stream)
{
    byte[] buffer = new byte[2048];
    int bytes = stream.Read(buffer, 0, buffer.Length);
    return Encoding.ASCII.GetString(buffer, 0, bytes);
}

        private void WriteString(SslStream stream, string s)
        {
            stream.Write(Encoding.ASCII.GetBytes(s + "\r\n"));
        }

        private void WriteStringBase64(SslStream stream, string s)
        {
            s = Convert.ToBase64String(Encoding.ASCII.GetBytes(s));
            stream.Write(Encoding.ASCII.GetBytes(s + "\r\n"));
        }
    }
}
