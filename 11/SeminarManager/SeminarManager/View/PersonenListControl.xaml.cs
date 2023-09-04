using System.Windows.Controls;

namespace SeminarManager;

public partial class PersonenListControl : UserControl
{
    public PersonenListControl()
    {
        InitializeComponent();

        DataContext = new PersonenListControlViewModel();
    }
}
