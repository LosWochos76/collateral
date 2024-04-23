using System.Globalization;
using System.Net.Mail;
using System.Windows.Controls;

namespace ValidationDemo;

public class EmailValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string email_text = value as string;
        if (!CheckMailAdress(email_text))
            return new ValidationResult(false, "Email-Adresse ist falsch!");

        return new ValidationResult(true, null);
    }

    private static bool CheckMailAdress(string email_text)
    {
        MailAddress addr;
        return MailAddress.TryCreate(email_text, out addr);
    }
}
