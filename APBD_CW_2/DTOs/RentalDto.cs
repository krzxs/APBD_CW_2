using APBD_CW_2.Domain;
using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Domain.Users;

namespace APBD_CW_2.DTOs;

public record RentalDto(
    string Id,
    string UserId,
    string DeviceId,
    DateTime StartDate,
    DateTime DueDate,
    DateTime? ActualReturnDate,
    decimal PenaltyAmount
)
{
    public static RentalDto From(Rental r)
    {
        return new RentalDto(r.Id, r.User.Id, r.Device.Id, r.StartDate, r.DueDate, r.ActualReturnDate, r.PenaltyAmount);
    }

    public Rental ToRental(User user, Device device)
    {
        return Rental.Restore(Id, user, device, StartDate, DueDate, ActualReturnDate, PenaltyAmount);
    }
}