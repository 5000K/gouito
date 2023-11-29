// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612


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