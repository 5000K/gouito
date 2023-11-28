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