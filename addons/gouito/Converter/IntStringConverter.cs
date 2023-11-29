// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612

namespace gouito.Converter;

public class IntStringConverter: IConverter<int, string>
{
    public string Convert(int value)
    {
        return value.ToString();
    }

    public int ConvertBack(string value)
    {
        return int.TryParse(value, out var i) ? i : default;
    }
}