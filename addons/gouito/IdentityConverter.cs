// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

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