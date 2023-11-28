using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace gouito;

public static class BindingExtensions
{
    public static Binding<T, TSource> Bind<T, TSource>(this IBindingTarget<T> target, TSource source, Expression<Func<TSource, T>> propertyGetter, BindingDirection direction = BindingDirection.OneWay) where TSource: INotifyPropertyChanged
    {
        return new Binding<T, TSource>(target, source, propertyGetter, direction);
    }
}