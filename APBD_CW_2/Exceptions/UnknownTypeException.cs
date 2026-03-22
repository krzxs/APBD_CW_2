namespace APBD_CW_2.Exceptions;

public class UnknownTypeException : Exception
{
    public UnknownTypeException(string type) : base($"Unknown type - {type} (object is not serializable).")
    {
    }
}