using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Watch;

public partial class WatchControl : UserControl
{
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
        hour.Angle = (current.Hour % 12) * 30 + ((double)current.Minute / 60) * 30;
        minute.Angle = current.Minute * 6 + ((double)current.Second / 60) * 6;
        second.Angle = current.Second * 6;
    }
}
