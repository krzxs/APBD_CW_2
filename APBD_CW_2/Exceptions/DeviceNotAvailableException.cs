namespace APBD_CW_2.Exceptions;

public class DeviceNotAvailableException : Exception
{
    public DeviceNotAvailableException(string id) : base($"Device with id {id} is not available.")
    {
    }
}