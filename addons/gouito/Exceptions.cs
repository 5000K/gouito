// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System;

namespace gouito;

public class GouitoNoPropertyException : Exception
{
    public GouitoNoPropertyException(): base($"Tried to access a member property but found none."){}
}

public class GouitoNoConverterException : Exception
{
    public GouitoNoConverterException(string type1, string type2):base($"WrappedBindingTarget needs a converter to convert between {type1} and {type2}."){}
}