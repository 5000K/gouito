// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

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