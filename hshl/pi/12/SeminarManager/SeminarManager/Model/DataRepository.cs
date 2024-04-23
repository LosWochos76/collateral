namespace SeminarManager;

public class DataRepository
{
    public PersonRepository Persons { get; private set; }
    public SeminarRepository Seminars { get; private set; }

    public DataRepository()
    {
        Persons = new PersonRepository();
        Seminars = new SeminarRepository();
    }

    public void Clear()
    {
        Persons.Clear();
        Seminars.Clear();
    }
}