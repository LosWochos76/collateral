using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace GridViewDemo;


public partial class MainWindow : Window
{
    public ObservableCollection<Mitglied> Mitglieder { get; set; } = new ObservableCollection<Mitglied>();

    public MainWindow()
    {
        InitializeComponent();

        Mitglieder.Add(new Mitglied("Peter", "Müller", Convert.ToDateTime("1980-07-22")));
        Mitglieder.Add(new Mitglied("Marianne", "Schmidt", Convert.ToDateTime("1991-03-11")));
        Mitglieder.Add(new Mitglied("Erkan", "Özgür", Convert.ToDateTime("1989-10-13")));
        DataContext = this;
    }
}
