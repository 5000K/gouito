using System.ComponentModel;

namespace gouito;

/// <summary>
/// Wraps any binding target to allow for conversion and very basic (rejecting) validation.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public class WrappedBindingTarget<TSource, TTarget>: IBindingTarget<TSource>
{
    private readonly IBindingTarget<TTarget> _target;
    private readonly IConverter<TSource, TTarget> _converter;
    private readonly IValueValidator<TTarget> _validator;

    private TSource _lastValidValue = default;

    public WrappedBindingTarget(IBindingTarget<TTarget> target, IConverter<TSource, TTarget> converter = default, IValueValidator<TTarget> validator = default)
    {
        _target = target;
        _validator = validator;
        _converter = converter;
        _validator = validator ?? new NullValidator<TTarget>();

        if (converter == default)
        {
            if (typeof(TSource) == typeof(TTarget))
            {
                converter = (IConverter<TSource, TTarget>)new IdentityConverter<TSource>();
            }
            else
            {
                throw new GouitoNoConverterException(typeof(TSource).Name, typeof(TTarget).Name);
            }
        }
        
        target.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // suppress value change if value is not valid.
        if (!_validator.Check(_target.Value))
        {
            Value = _lastValidValue;
            return;
        }
        
        PropertyChanged?.Invoke(this, e);
    }

    public event PropertyChangedEventHandler PropertyChanged;
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