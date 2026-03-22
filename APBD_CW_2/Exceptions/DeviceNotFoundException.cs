namespace APBD_CW_2.Exceptions;

public class DeviceNotFoundException : Exception
{
    public DeviceNotFoundException(string id) : base($"Device with id {id} was not found.")
    {
    }
}