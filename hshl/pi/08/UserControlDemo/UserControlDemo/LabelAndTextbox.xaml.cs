using System.Windows;
using System.Windows.Controls;

namespace UserControlDemo;

public partial class LabelAndTextbox : UserControl
{
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(LabelAndTextbox), new PropertyMetadata(""));

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(LabelAndTextbox), new PropertyMetadata(""));

    public string Label 
    { 
        get
        {
            return (string)GetValue(LabelProperty);
        }

        set
        {
            SetValue(LabelProperty, value);
        }
    }

    public string Text
    {
        get
        {
            return (string)GetValue(TextProperty);
        }

        set
        {
            SetValue(TextProperty, value);
        }
    }

    public LabelAndTextbox()
    {
        InitializeComponent();

        LayoutRoot.DataContext = this;
    }
}
