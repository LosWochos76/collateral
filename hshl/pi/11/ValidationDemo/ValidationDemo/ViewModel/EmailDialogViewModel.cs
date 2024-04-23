using CommunityToolkit.Mvvm.Input;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;

namespace ValidationDemo;

public class EmailDialogViewModel : ValidatableBase
{
    private RelayCommand<Window> ok_command;
    public ICommand OkCommand { get; set; }

    private RelayCommand<Window> cancel_command;
    public ICommand CancelCommand { get; set; }

    public EmailDialogViewModel(string email_text)
    {
        EmailText = email_text;

        ok_command = new RelayCommand<Window>((window) => Ok(window), (window) => { return !HasErrors; });
        OkCommand = ok_command;

        cancel_command = new RelayCommand<Window>((window) => Cancel(window));
        CancelCommand = cancel_command;
    }

    public void Ok(Window window)
    {
        if (window is not null)
        {
            window.DialogResult = true;
            window.Close();
        }
    }

    public void Cancel(Window window) 
    {
        if (window is not null)
        {
            window.DialogResult = false;
            window.Close();
        }
    }

    private string email_text;
    public string EmailText
    {
        get { return email_text; }
        set
        {
            if (value is null || value.Equals(email_text))
                return;

            email_text = value;
            NotifyPropertyChanged();

            if (CheckMailAdress(email_text))
            {
                if (RemoveError(nameof(EmailText)))
                    ok_command?.NotifyCanExecuteChanged();
            }
            else
            {
                if (AddError(nameof(EmailText), "Email-Adresse ungültig!"))
                    ok_command?.NotifyCanExecuteChanged();
            }
        }
    }

    public static bool CheckMailAdress(string email_text)
    {
        MailAddress addr;
        return MailAddress.TryCreate(email_text, out addr);
    }

    public EmailDialogViewModel Clone()
    {
        return new EmailDialogViewModel(EmailText);
    }
}