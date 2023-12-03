// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

namespace gouito.Converter;

public class InvertedConverter<TIn, TOut>: IConverter<TIn, TOut>
{
    private readonly IConverter<TOut, TIn> _invertedSource;

    public InvertedConverter(IConverter<TOut, TIn> invertedSource)
    {
        _invertedSource = invertedSource;
    }

    public TOut Convert(TIn value)
    {
        return _invertedSource.ConvertBack(value);
    }

    public TIn ConvertBack(TOut value)
    {
        return _invertedSource.Convert(value);
    }
}

public static partial class ConverterExtensions
{
    public static IConverter<TOut, TIn> Invert<TIn, TOut>(this IConverter<TIn, TOut> converter)
    {
        return new InvertedConverter<TOut, TIn>(converter);
    }
} 
