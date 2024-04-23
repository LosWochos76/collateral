using System.Windows;

namespace Celsius2FahrenheitWithSlider;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Slider_ValueChanged(null, null);
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        var c = slider.Value;
        celsius.Content = string.Format("Temperatur in °C: {0:F2}.", c);
        fahrenheit.Content = string.Format("Temperatur in °F: {0:F2}.", c * 9 / 5 + 32);
    }
}
