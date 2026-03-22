namespace APBD_CW_2.Exceptions;

public class RentalLimitExceededException : AppException
{
    public RentalLimitExceededException(string userId, int limit) : base($"User with id {userId} exceeded limit of {limit} devices.")
    {
    }
}