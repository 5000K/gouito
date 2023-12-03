// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.Windows.Input;
using Godot;

namespace gouito.Target;

public static partial class TargetExtensions
{

    #region WrappedBindingTarget
    
    public static IBindingTarget<TS> Wrap<TS, TT>(this IBindingTarget<TT> target, IConverter<TS, TT> converter)
    {
        return new WrappedBindingTarget<TS, TT>(target, converter: converter);
    }
    
    public static IBindingTarget<TS> Wrap<TS>(this IBindingTarget<TS> target,  IValueValidator<TS> validator)
    {
        return new WrappedBindingTarget<TS, TS>(target, validator: validator);
    }
    
    public static IBindingTarget<TS> Wrap<TS, TT>(this IBindingTarget<TT> target, IConverter<TS, TT> converter, IValueValidator<TT> validator)
    {
        return new WrappedBindingTarget<TS, TT>(target, converter: converter, validator: validator);
    }
    
    #endregion
    
}