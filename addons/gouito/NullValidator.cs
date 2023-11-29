// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

namespace gouito;

public class NullValidator<TSource> : IValueValidator<TSource>
{
    public bool Check(TSource value)
    {
        return true;
    }
}