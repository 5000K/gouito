using Godot;

namespace gouito.Target;

public static class TargetExtensions
{
    public static IBindingTarget<bool> BindVisibility(this CanvasItem item)
    {
        return new VisibilityTarget(item);
    }
    
    public static IBindingTarget<bool> BindVisibility(this Node3D item)
    {
        return new VisibilityTarget(item);
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