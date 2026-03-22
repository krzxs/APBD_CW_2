namespace APBD_CW_2.Exceptions;

public class UnknownTypeException : AppException
{
    public UnknownTypeException(string type) : base($"Unknown type - {type} (object is not serializable).")
    {
    }
}