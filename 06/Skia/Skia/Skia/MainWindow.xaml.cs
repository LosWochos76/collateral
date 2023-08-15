using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Windows;

namespace Skia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        var width = canvas.DeviceClipBounds.Width;
        var height = canvas.DeviceClipBounds.Height;

        var rnd = new Random();
        for (int i=0; i<10000; i++)
        {
            var color = new SKColor(
                (byte)rnd.Next(0, 255),
                (byte)rnd.Next(0, 255),
                (byte)rnd.Next(0, 255));

            var paint = new SKPaint() { Color=color };
            canvas.DrawLine(
                rnd.Next(0, width), rnd.Next(0, height), 
                rnd.Next(0, width), rnd.Next(0, height), paint);
        }
    }
}
