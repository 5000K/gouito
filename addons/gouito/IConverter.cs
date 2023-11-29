// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

namespace gouito;

public interface IConverter<TSource, TTarget>
{
    TTarget Convert(TSource value);
    TSource ConvertBack(TTarget value);
}