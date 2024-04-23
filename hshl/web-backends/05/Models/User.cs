namespace ToDoService.Models;

public class User : Entity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string EMail { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
}