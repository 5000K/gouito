// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;
using Godot;

namespace gouito.Target;

public class Texture2DBindingTarget: IBindingTarget<Texture2D>
{
    private readonly IBindingTarget<Texture2D> _subBindingTarget;


    public Texture2DBindingTarget(TextureRect textureRect)
    {
        _subBindingTarget = new TextureRectBindingTarget(textureRect);
    }
    

    public event PropertyChangedEventHandler PropertyChanged
    {
        add => _subBindingTarget.PropertyChanged += value;
        remove => _subBindingTarget.PropertyChanged -= value;
    }

    public event IBindingTarget<Texture2D>.ManagedNodeDisposedHandler ManagedNodeDisposed
    {
        add => _subBindingTarget.ManagedNodeDisposed += value;
        remove => _subBindingTarget.ManagedNodeDisposed -= value;
    }

    public string PropertyName => _subBindingTarget.PropertyName;

    public Texture2D Value
    {
        get => _subBindingTarget.Value;
        set => _subBindingTarget.Value = value;
    }
    
    
    public class TextureRectBindingTarget: IBindingTarget<Texture2D>
    {
        private TextureRect _textureRect;

        public TextureRectBindingTarget(TextureRect textureRect)
        {
            _textureRect = textureRect;
            textureRect.TreeExiting += OnExitingTree;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string PropertyName { get; } = "texture";

        public Texture2D Value
        {
            get => _textureRect.Texture;
            set => _textureRect.Texture = value;
        }
        public event IBindingTarget<Texture2D>.ManagedNodeDisposedHandler ManagedNodeDisposed;
        private void OnExitingTree()
        {
            ManagedNodeDisposed?.Invoke(this);
        }
    }
}

public static partial class TargetExtensions
{
    public static IBindingTarget<Texture2D> TextureTarget(this TextureRect rect)
    {
        return new Texture2DBindingTarget.TextureRectBindingTarget(rect);
    }
}