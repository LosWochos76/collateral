using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ValidationDemo;

public abstract class ValidatableBase : BindableBase, INotifyDataErrorInfo
{
    private Dictionary<string, string> errors = new Dictionary<string, string>();

    public bool HasErrors { get { return errors.Keys.Count > 0; } }
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName is not null)
        {
            if (errors.ContainsKey(propertyName))
                yield return errors[propertyName];
        }
        else
        {
            foreach (var error in errors.Values)
                yield return error;
        }
    }

    protected void NotifyErrorsChanged(string? propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected bool RemoveError(string property)
    {
        if (errors.ContainsKey(property))
        {
            errors.Remove(property);
            NotifyErrorsChanged(property);
            return true;
        }

        return false;
    }

    protected bool AddError(string property, string error)
    {
        if (!errors.ContainsKey(property))
        {
            errors[property] = error;
            NotifyErrorsChanged(property);
            return true;
        }

        return false;
    }
}
