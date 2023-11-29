// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612

using System.Windows.Input;
using Godot;

namespace gouito.Target;

public static class TargetExtensions
{
    public static IBindingTarget<bool> VisibilityTarget(this CanvasItem item)
    {
        return new VisibilityTarget(item);
    }
    
    public static IBindingTarget<bool> VisibilityTarget(this Node3D item)
    {
        return new VisibilityTarget(item);
    }

    
    public static IBindingTarget<string> TextTarget(this Label node)
    {
        return new TextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this Label3D node)
    {
        return new TextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this TextEdit node)
    {
        return new TextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this LineEdit node)
    {
        return new TextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this RichTextLabel node)
    {
        return new TextBindingTarget(node);
    }

    public static IBindingTarget<ICommand> CommandTarget(this BaseButton button, ButtonCommandExecutionPolicy policy = ButtonCommandExecutionPolicy.OnDown)
    {
        return new CommandTarget(button, policy);
    }
    
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