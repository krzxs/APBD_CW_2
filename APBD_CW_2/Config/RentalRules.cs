namespace APBD_CW_2.Config;

public static class RentalRules
{
    private const decimal PenaltyPerDay = 10.0m;

    private const decimal MaxPenalty = 500.0m;

    public static decimal CalculatePenalty(DateTime dueDate, DateTime actualReturnDate)
    {
        var daysLate = (actualReturnDate - dueDate).Days;
        return daysLate <= 0 ? 0.0m : Math.Min(daysLate * PenaltyPerDay, MaxPenalty);
    }
}