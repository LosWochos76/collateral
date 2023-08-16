using System.Windows;
using UserFormDemo.Model;

namespace UserFormDemo;

public partial class UserDialog : Window
{
    public UserDialog(User user)
    {
        InitializeComponent();
        DataContext = user;
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
}
