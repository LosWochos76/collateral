namespace TreeViewDemo;

public abstract class Element
{
    public string Name { get; set; }

    public Element(string name)
    { 
        this.Name = name; 
    }
}
