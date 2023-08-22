using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1;

public partial class MainWindow : Window
{
    private Random rnd = new Random();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
        var next_x = rnd.NextDouble() * (canvas.ActualWidth - button.ActualWidth);
        var next_y = rnd.NextDouble() * (canvas.ActualHeight - button.ActualHeight);
        Canvas.SetTop(button, next_y);
        Canvas.SetLeft(button, next_x);
    }

    private void button_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
