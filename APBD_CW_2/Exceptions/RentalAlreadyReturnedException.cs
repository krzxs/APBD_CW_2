namespace APBD_CW_2.Exceptions;

public class RentalAlreadyReturnedException : AppException
{
    public RentalAlreadyReturnedException(string id) : base($"Rental with id {id} was already returned.")
    {
    }
}