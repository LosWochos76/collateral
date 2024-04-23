using System.Windows;
using System.Windows.Controls;

namespace StartEnd;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        var start_date = start.SelectedDate;
        var end_date = end.SelectedDate;

        if (start_date is null || end_date is null)
            button.IsEnabled = false;
        else if (start_date > end_date)
            button.IsEnabled = false;
        else
            button.IsEnabled = true;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
