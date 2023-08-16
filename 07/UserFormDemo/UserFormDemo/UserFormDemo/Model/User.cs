using System;

namespace UserFormDemo.Model;

public class User
{
    private string vorname;
    private string nachname;

    public string Vorname
    {
        get { return vorname;}
        set { vorname = value; }
    }

    public string Nachname
    {
        get { return nachname; }
        set { nachname = value; }
    }

    public User Clone()
    {
        return new User() { Vorname = Vorname, Nachname = Nachname };
    }

    public override string ToString()
    {
        return Vorname + " " + Nachname;
    }
}
