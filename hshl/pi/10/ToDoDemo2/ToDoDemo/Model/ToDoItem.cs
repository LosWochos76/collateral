namespace ToDoDemo;

public class ToDoItem
{
    public string Title { get; set; }
    public int Completion { get; set; }

    public ToDoItem(string title, int completion)
    {
        Title = title;
        Completion = completion;
    }
}
