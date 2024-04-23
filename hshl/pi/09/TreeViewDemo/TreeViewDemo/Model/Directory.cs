using System.Collections.ObjectModel;

namespace TreeViewDemo;

public class Directory : Element
{
    public ObservableCollection<Element> Content { get; set; } = new ObservableCollection<Element>();

    public Directory(string name) : base(name) { }
}
