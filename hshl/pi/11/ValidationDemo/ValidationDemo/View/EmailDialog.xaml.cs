using System.Windows;

namespace ValidationDemo;

public partial class EmailDialog : Window
{
    public EmailDialog(EmailDialogViewModel email)
    {
        InitializeComponent();
        DataContext = email;
    }
}
