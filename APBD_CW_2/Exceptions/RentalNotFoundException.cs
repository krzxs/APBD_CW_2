namespace APBD_CW_2.Exceptions;

public class RentalNotFoundException : Exception
{
    public RentalNotFoundException(string id) : base($"Rental with id {id} was not found.")
    {
    }
}