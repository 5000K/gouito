// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;
using Godot;

namespace gouito.Target;

public class ColorSelfModulateBindingTarget : IBindingTarget<Color>
{
    private CanvasItem _item;

    public ColorSelfModulateBindingTarget(CanvasItem item)
    {
        _item = item;
        item.TreeExiting += OnExitingTree;
    }

    private void OnExitingTree()
    {
        ManagedNodeDisposed?.Invoke(this);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public event IBindingTarget<Color>.ManagedNodeDisposedHandler ManagedNodeDisposed;
    public string PropertyName { get; } = "selfModulate";

    public Color Value
    {
        get => _item.SelfModulate;
        set => _item.SelfModulate = value;
    }
}

public static partial class TargetExtensions
{
    public static IBindingTarget<Color> SelfModulateTarget(this CanvasItem item)
    {
        return new ColorSelfModulateBindingTarget(item);
    }
}
