// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Godot;

namespace gouito;

/// <summary>
/// Exposes a simple API for registering bindings. Bindings will get reevaluated whenever the internal view model changes, binding to only the current view model.
/// </summary>
/// <remarks>
/// Usually, you would want your view model to stay constant, and could be considered bad practice.
///
/// This feature is supported for views that recycle elements for better performance. If you need it, you know it.
/// </remarks>
public abstract partial class FluentBindingController<T>: Control, IBindingTarget<T> where T: INotifyPropertyChanged
{
    private T _value;
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    string IBindingTarget<T>.PropertyName => "value";

    public T Value
    {
        get => _value;
        set
        {
            var oldValue = Value;
            if (SetField(ref _value, value))
            {
                if (oldValue != null)
                {
                    DisposeBindings();
                }
                
                BuildBindings();
            }
        }
    }

    private readonly IList<IDisposable> _managedBindings = new List<IDisposable>();
    private readonly IList<BindingCreator> _bindingCreators = new List<BindingCreator>();
    
    private void DisposeBindings()
    {
        foreach (var managedBinding in _managedBindings)
        {
            managedBinding.Dispose();
        }
    }

    private void BuildBindings()
    {
        foreach (var bindingCreator in _bindingCreators)
        {
            _managedBindings.Add(bindingCreator(Value));
        }
    }

    protected delegate IDisposable BindingCreator(T viewModel);

    protected void Register<TBinding>(IBindingTarget<TBinding> target, Expression<Func<T, TBinding>> propertyGetter, BindingDirection direction = BindingDirection.OneWay)
    {
        _bindingCreators.Add(vm => new Binding<TBinding, T>(target, vm, propertyGetter, direction));
    }

    public override void _Ready()
    {
        RegisterBindings();
    }

    protected abstract void RegisterBindings();

}