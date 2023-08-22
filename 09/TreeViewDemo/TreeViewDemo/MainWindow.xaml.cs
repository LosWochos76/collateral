using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace TreeViewDemo;

public partial class MainWindow : Window
{
    public ObservableCollection<Element> Elements { get; set; } = new ObservableCollection<Element>();

    public MainWindow()
    {
        InitializeComponent();

        ReadDirectoryStructureAsync();
        DataContext = this;
    }

    private Task ReadDirectoryStructureAsync()
    {
        return Task.Run(() => { ReadDirectoryStructure(); });
    }

    private void ReadDirectoryStructure()
    {
        var dir = new Directory("c:/");
        Elements.Add(dir);
        ReadDirectoryStructure(dir);
    }

    private void ReadDirectoryStructure(Directory dir)
    {
        try
        {
            foreach (var d in System.IO.Directory.EnumerateDirectories(dir.Name))
            {
                var new_dir = new Directory(d);
                dir.Content.Add(new_dir);
                ReadDirectoryStructure(new_dir);
            }

            foreach (var f in System.IO.Directory.EnumerateFiles(dir.Name))
            {
                dir.Content.Add(new File(f));
            }
        }
        catch { }
    }
}
