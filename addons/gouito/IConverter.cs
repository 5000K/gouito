// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612

namespace gouito;

public interface IConverter<TSource, TTarget>
{
    TTarget Convert(TSource value);
    TSource ConvertBack(TTarget value);
}