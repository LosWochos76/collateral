using System.Windows;
using UserFormDemo.Model;

namespace UserFormDemo;

public partial class UserDialog : Window
{
    public  User CurrentUser { get; set; }

    public UserDialog(User user)
    {
        InitializeComponent();

        CurrentUser = user;
        DataContext = this;
    }

    private void Button_Ok_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void Button_Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void Button_Change_Data_Click(object sender, RoutedEventArgs e)
    {
        CurrentUser.Vorname = "Peter";
        CurrentUser.Nachname = "Müller";
    }
}
