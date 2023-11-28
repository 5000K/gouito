namespace gouito;

public class NullValidator<TSource> : IValueValidator<TSource>
{
    public bool Check(TSource value)
    {
        return true;
    }
}