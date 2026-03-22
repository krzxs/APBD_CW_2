namespace APBD_CW_2.Exceptions;

public class InvalidRentalDaysException : Exception
{
    public InvalidRentalDaysException() : base("Rental days must be greater than zero.")
    {
    }
}