namespace gouito;

public class IdentityConverter<TIdentity>: IConverter<TIdentity, TIdentity>
{
    public TIdentity Convert(TIdentity value)
    {
        return value;
    }

    public TIdentity ConvertBack(TIdentity value)
    {
        return value;
    }
}