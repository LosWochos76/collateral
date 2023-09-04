using System.Windows;

namespace SeminarManager;

public partial class EditPersonWindow : Window
{
    public EditPersonWindow(Person obj)
    {
        InitializeComponent();

        DataContext = new PersonEditViewModel(obj);
    }
}
