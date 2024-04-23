using System.Windows;

namespace ValidationDemo;

public partial class PersonDialog : Window
{
    public PersonDialog(PersonDialogViewModel p)
    {
        InitializeComponent();
        DataContext = p;
    }
}