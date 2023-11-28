namespace gouito;

public interface IValueValidator<in TSource>
{
    bool Check(TSource value);
}