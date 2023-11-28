using System.ComponentModel;

namespace gouito;

public interface IBindingTarget<T>: INotifyPropertyChanged
{
    public string PropertyName { get; }
    public T Value { get; set; }
}