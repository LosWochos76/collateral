using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Watch;

public partial class WatchControl : UserControl
{
    private const double DegToRad = Math.PI / 180;

    public WatchControl()
    {
        InitializeComponent();

        var dp = new DispatcherTimer();
        dp.Interval = new System.TimeSpan(0, 0, 0, 0, 100);
        dp.Tick += Dp_Tick;
        dp.Start();

        UpdateClock();
    }

    private void Dp_Tick(object? sender, System.EventArgs e)
    {
        UpdateClock();
    }

    private void UpdateClock()
    {
        var current = DateTime.Now;

        var degree = (current.Hour % 12) * 30 + ((double)current.Minute / 60) * 30;
        hour.X2 = 200 + 100 * Math.Sin(DegToRad * degree);
        hour.Y2 = 200 - 100 * Math.Cos(DegToRad * degree);

        degree = current.Minute * 6 + ((double)current.Second / 60) * 6;
        minute.X2 = 200 + 150 * Math.Sin(DegToRad * degree);
        minute.Y2 = 200 - 150 * Math.Cos(DegToRad * degree);

        degree = current.Second * 6;
        second.X2 = 200 + 200 * Math.Sin(DegToRad * degree);
        second.Y2 = 200 - 200 * Math.Cos(DegToRad * degree);
    }
}
