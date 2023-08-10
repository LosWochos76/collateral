using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IsEnabled;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void text_TextChanged(object sender, TextChangedEventArgs e)
    {
        MailAddress addr;
        if (MailAddress.TryCreate(text.Text, out addr))
            button.IsEnabled = true;
        else
            button.IsEnabled = false;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
