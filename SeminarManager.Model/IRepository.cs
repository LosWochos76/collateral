namespace SeminarManager.Model
{
    public interface IRepository
    {
        IPersonRepository Persons { get; }
        ISeminarRepository Seminars { get; }
    }
}