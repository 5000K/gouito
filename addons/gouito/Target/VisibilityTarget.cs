// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Godot;
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

namespace gouito.Target;

public class VisibilityTarget: IBindingTarget<bool>
{
    [MaybeNull] private readonly CanvasItem _canvasItem;
    [MaybeNull] private readonly Node3D _node3d;
    private readonly bool _useNode3d;
    
    public VisibilityTarget(CanvasItem node)
    {
        _canvasItem = node;
        _node3d = null!;
        _useNode3d = false;
        node.VisibilityChanged += VisibilityChanged;
    }

    public VisibilityTarget(Node3D node)
    {
        _canvasItem = null!;
        _node3d = node;
        _useNode3d = true;

        node.VisibilityChanged += VisibilityChanged;
    }
    
    private void VisibilityChanged()
    {
        var args = new PropertyChangedEventArgs(PropertyName);
        PropertyChanged?.Invoke(this, args);
    }
    
    public event PropertyChangedEventHandler PropertyChanged = null!;
    public string PropertyName => "visible";

    public bool Value
    {
        get => _useNode3d ? _node3d!.Visible : _canvasItem!.Visible;
        set
        {
            if (_useNode3d)
            {
                _node3d!.Visible = value;
            }
            else
            {
                _canvasItem!.Visible = value;
            }
        }
    }
}