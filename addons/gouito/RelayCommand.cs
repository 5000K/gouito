// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.


// ReSharper disable CheckNamespace
#pragma warning disable CS0067
#pragma warning disable CS0414

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace gouito;

public class RelayCommand: ICommand
{
    public static readonly ICommand NullCommand = new RelayCommand(() => {});
    
    [MaybeNull] private readonly Action _action;

    public RelayCommand(Action action)
    {
        _action = action;
    }

    public bool CanExecute([AllowNull] object parameter)
    {
        return _action != null;
    }

    public void Execute([AllowNull] object parameter)
    {
        if (_action != null)
        {
            _action();
        }
    }

#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
    public event EventHandler CanExecuteChanged = null!;
}

public class RelayCommand<T>: ICommand
{
    [MaybeNull] private readonly Action<T> _action;

    public RelayCommand(Action<T> action)
    {
        _action = action;
    }
    

    public bool CanExecute([AllowNull] object parameter)
    {
        return _action != null && parameter is T;
    }

    public void Execute([AllowNull] object parameter)
    {
        if (_action != null && parameter is T t)
        {
            _action(t);
        }
    }
    
    public event EventHandler CanExecuteChanged = null!;
}
