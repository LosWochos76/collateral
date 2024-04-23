namespace ToDoService.Models;

public class ToDoListResult
{
    public int Page { get; set; }
    public int PageCount { get; set; }
    public IEnumerable<ToDo> Items { get; set; }
}