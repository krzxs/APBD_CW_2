namespace APBD_CW_2.Exceptions;

public class RentalNotFoundException : AppException
{
    public RentalNotFoundException(string id) : base($"Rental with id {id} was not found.")
    {
    }
}