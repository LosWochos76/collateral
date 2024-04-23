using System.Windows;

namespace WpfMailer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Send_Clicked(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            var mailer = new SslSmtpMailer("smtp.googlemail.com", 465, username.Text, password.Password);
            var result = await mailer.SendMailAsync(to.Text, subject.Text, text.Text);
            button.IsEnabled = true;

            if (result)
                MessageBox.Show("Success!");
            else
                MessageBox.Show("Error!");
        }
    }
}
