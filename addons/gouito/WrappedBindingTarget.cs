// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

namespace gouito;

/// <summary>
/// Wraps any binding target to allow for conversion and very basic (rejecting) validation.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public class WrappedBindingTarget<TSource, TTarget>: IBindingTarget<TSource>
{
    private readonly IBindingTarget<TTarget> _target;
    private readonly IConverter<TSource, TTarget> _converter = null!;
    private readonly IValueValidator<TTarget> _validator;

    [MaybeNull] private TSource _lastValidValue;

    public WrappedBindingTarget(IBindingTarget<TTarget> target, [AllowNull] IConverter<TSource, TTarget> converter = default, [AllowNull]IValueValidator<TTarget> validator = default)
    {
        _target = target;
        _validator = validator ?? new NullValidator<TTarget>();
        

        if (converter == default)
        {
            if (typeof(TSource) == typeof(TTarget))
            {
                _converter = (IConverter<TSource, TTarget>)new IdentityConverter<TSource>();
            }
            else
            {
                throw new GouitoNoConverterException(typeof(TSource).Name, typeof(TTarget).Name);
            }
        }
        else
        {
            _converter = converter;
        }
        
        target.PropertyChanged += OnPropertyChanged;
        _target.ManagedNodeDisposed += OnExitingTree;
    }

    public event IBindingTarget<TSource>.ManagedNodeDisposedHandler ManagedNodeDisposed;
    private void OnExitingTree(IBindingTarget<TTarget> target)
    {
        ManagedNodeDisposed?.Invoke(this);
    }


    private void OnPropertyChanged([AllowNull] object sender, PropertyChangedEventArgs e)
    {
        // suppress value change if value is not valid.
        if (!_validator.Check(_target.Value))
        {
            if (_lastValidValue != null) Value = _lastValidValue;
            
            return;
        }
        
        PropertyChanged?.Invoke(this, e);
    }

    public event PropertyChangedEventHandler PropertyChanged = null!;
    public string PropertyName => _target.PropertyName;

    public TSource Value
    {
        get => _converter.ConvertBack(_target.Value);
        set
        {
            _lastValidValue = value;
            _target.Value = _converter.Convert(value);
        }
    }
}