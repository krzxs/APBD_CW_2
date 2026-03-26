using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Domain.Users;
using APBD_CW_2.Exceptions;

namespace APBD_CW_2.Domain;

public class Rental : BaseObject
{
    public User User { get; }
    public Device Device { get; }
    public DateTime StartDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ActualReturnDate { get; private set; }
    public decimal PenaltyAmount { get; private set; }

    public Rental(User user, Device device, DateTime startDate, int days)
    {
        User = user;
        Device = device;
        StartDate = startDate;
        DueDate = startDate.AddDays(days);
    }

    private Rental(string id, User user, Device device, DateTime startDate, DateTime dueDate, DateTime? actualReturnDate, decimal penaltyAmount) : base(id)
    {
        User = user;
        Device = device;
        StartDate = startDate;
        DueDate = dueDate;
        ActualReturnDate = actualReturnDate;
        PenaltyAmount = penaltyAmount;
    }

    public static Rental Restore(string id, User user, Device device, DateTime startDate, DateTime dueDate, DateTime? actualReturnDate, decimal penaltyAmount)
    {
        return new Rental(id, user, device, startDate, dueDate, actualReturnDate, penaltyAmount);
    }

    public bool IsActive()
    {
        return !ActualReturnDate.HasValue;
    }

    public bool IsOverdue()
    {
        return IsActive() && DateTime.Now > DueDate;
    }

    public void MarkReturned(DateTime returnDate, decimal penalty)
    {
        ActualReturnDate = returnDate;
        PenaltyAmount = penalty;
    }

    public override string ToString()
    {
        var currentStatus = IsActive() ? (IsOverdue() ? "Overdue" : "Active") : $"Returned At: {ActualReturnDate:dd-MM-yyyy}";
        var actualPenalty = PenaltyAmount > 0 ? $" | Penalty: {PenaltyAmount:C}" : "";
        return $"({Id}) {User.FirstName} {User.LastName} -> {Device.Name} | From: {StartDate:dd-MM-yyyy}, Due Date: {DueDate:dd-MM-yyyy} | {currentStatus}{actualPenalty}";
    }
}