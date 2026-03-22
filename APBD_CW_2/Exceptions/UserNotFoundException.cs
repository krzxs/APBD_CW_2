namespace APBD_CW_2.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string id) : base($"User with id {id} was not found.")
    {
    }
}