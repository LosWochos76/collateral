using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace Pong;

public partial class MainWindow : Window
{
    private double delta_x = 5;
    private double delta_y = 5;

    public MainWindow()
    {
        InitializeComponent();

        var dp = new DispatcherTimer();
        dp.Interval = new TimeSpan(0, 0, 0, 0, 5);
        dp.Tick += Dp_Tick;
        dp.Start();
    }

    private void Dp_Tick(object? sender, EventArgs e)
    {
        var x = Canvas.GetLeft(ball);
        var y = Canvas.GetTop(ball);

        if (x >= canvas.ActualWidth - ball.ActualWidth || x <= 0)
            delta_x *= -1;

        if (y >= canvas.ActualHeight - ball.ActualHeight || y <= 0)
            delta_y *= -1;

        x += delta_x;
        y += delta_y;

        Canvas.SetLeft(ball, x);
        Canvas.SetTop(ball, y);
    }
}
