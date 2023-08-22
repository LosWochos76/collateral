using System.Windows;
using UserFormDemo.Model;

namespace UserFormDemo;

public partial class MainWindow : Window
{
    private User user;
    
    public MainWindow()
    {
        InitializeComponent();

        user = new User() { Vorname = "Hans", Nachname = "Dampf" };
    }

    private void Button_Edit_Click(object sender, RoutedEventArgs e)
    {
        var clone = user.Clone();
        var ud = new UserDialog(clone);
        var result = ud.ShowDialog();

        if (result == true)
            user = clone;
    }

    private void Button_Show_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(user.ToString());
    }
}
