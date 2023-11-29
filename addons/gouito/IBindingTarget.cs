// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;

namespace gouito;

public interface IBindingTarget<T>: INotifyPropertyChanged
{
    public string PropertyName { get; }
    public T Value { get; set; }
}