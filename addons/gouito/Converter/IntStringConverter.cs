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