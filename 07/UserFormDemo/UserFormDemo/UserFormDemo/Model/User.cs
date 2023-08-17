using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UserFormDemo.Model;

public class User : INotifyPropertyChanged
{
    private string vorname;
    private string nachname;

    public string Vorname
    {
        get { return vorname;}
        set 
        { 
            vorname = value;
            NotifyPropertyChanged();
        }
    }

    public string Nachname
    {
        get { return nachname; }
        set 
        { 
            nachname = value;
            NotifyPropertyChanged();
            NotifyPropertyChanged("VollerName");
        }
    }

    public string VollerName
    {
        get
        {
            return vorname + " " + nachname;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
