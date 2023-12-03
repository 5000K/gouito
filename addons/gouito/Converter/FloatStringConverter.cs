// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.Globalization;

namespace gouito.Converter;

public class FloatStringConverter: IConverter<float, string>
{
    private readonly string _format;
    private readonly CultureInfo _cultureInfo;

    public FloatStringConverter(string format = "#0.00", CultureInfo cultureInfo = default)
    {
        _format = format;

        if (cultureInfo == default)
        {
            _cultureInfo = CultureInfo.InvariantCulture;
        }
        else
        {
            _cultureInfo = cultureInfo;
        }
    }

    public string Convert(float value)
    {
        return value.ToString(_format);
    }

    public float ConvertBack(string value)
    {
        return float.TryParse(value, NumberStyles.Float, _cultureInfo, out var i) ? i : default;
    }
}