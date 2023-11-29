// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612
#pragma warning disable CS0067

using System;
using System.Windows.Input;

namespace gouito;

public class RelayCommand: ICommand
{
    private readonly Action _action;

    public RelayCommand(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object parameter)
    {
        return _action != null;
    }

    public void Execute(object parameter)
    {
        if (CanExecute(parameter))
        {
            _action();
        }
    }

    public event EventHandler CanExecuteChanged;
}

public class RelayCommand<T>: ICommand
{
    private readonly Action<T> _action;

    public RelayCommand(Action<T> action)
    {
        _action = action;
    }
    

    public bool CanExecute(object parameter)
    {
        return _action != null && parameter is T;
    }

    public void Execute(object parameter)
    {
        if (_action != null && parameter is T t)
        {
            _action(t);
        }
    }
    
    public event EventHandler CanExecuteChanged;
}
