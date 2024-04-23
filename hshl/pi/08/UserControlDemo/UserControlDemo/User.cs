using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UserControlDemo;

public class User : INotifyPropertyChanged
{
    private string vorname = string.Empty;
    private string nachname = string.Empty;
    private string email = string.Empty;

    public string Vorname
    {
        get { return vorname; }
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
        }
    }

    public string EMail
    {
        get { return email; }
        set
        {
            email = value;
            NotifyPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}