namespace APBD_CW_2.Exceptions;

public class InvalidRentalDaysException : AppException
{
    public InvalidRentalDaysException() : base("Rental days must be greater than zero.")
    {
    }
}