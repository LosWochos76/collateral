namespace ToDoService.Models;

public class ToDoListResult
{
    public int CurrentPage { get; set; }
    public int PagesCount { get; set; }
    public IEnumerable<ToDo> Items { get; set; }
}