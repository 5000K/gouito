// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612

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