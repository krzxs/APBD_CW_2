using APBD_CW_2.Domain.Filters;

namespace APBD_CW_2.Services;

public class ReportService
{
    private readonly DeviceService _deviceService;
    private readonly RentalService _rentalService;
    private readonly UserService _userService;

    public ReportService(DeviceService deviceService, RentalService rentalService, UserService userService)
    {
        _deviceService = deviceService;
        _rentalService = rentalService;
        _userService = userService;
    }

    public string GenerateDeviceReport(DeviceFilter filter = DeviceFilter.All)
    {
        var lines = new List<string>();
        var devices = _deviceService.GetAll();
        var filteredDevices = filter switch
        {
            DeviceFilter.Available => _deviceService.GetAllAvailable(),
            DeviceFilter.Rented => _deviceService.GetAllRented(),
            DeviceFilter.Unavailable => _deviceService.GetAllUnavailable(),
            _ => devices
        };

        var header = filter == DeviceFilter.All ? "ALL DEVICES" : $"DEVICES - FILTER: {GetFilterDisplay(filter)}";
        lines.Add(header);
        if (filteredDevices.Count == 0)
        {
            lines.Add("No DEVICES found");
        }
        else
        {
            foreach (var device in filteredDevices)
            {
                lines.Add($"{device}");
            }
        }
        lines.Add($"Total: {filteredDevices.Count}");

        return string.Join(Environment.NewLine, lines);
    }

    public string GenerateRentalReport(RentalFilter filter = RentalFilter.All, string? userId = null)
    {
        var lines = new List<string>();
        var rentals = _rentalService.GetAll();
        if (userId != null)
        {
            rentals = rentals.Where(r => r.User.Id == userId).ToList();
        }
        var filteredRentals = filter switch
        {
            RentalFilter.Active => _rentalService.GetAllActive(userId),
            RentalFilter.Overdue => _rentalService.GetAllOverdue(userId),
            RentalFilter.Completed => _rentalService.GetAllCompleted(userId),
            _ => rentals
        };

        var userHeader = userId != null ? $" (user: {userId})" : "";
        var header = filter == RentalFilter.All ? $"ALL RENTALS{userHeader}" : $"RENTALS - FILTER: {GetFilterDisplay(filter)}{userHeader}";
        lines.Add(header);
        if (filteredRentals.Count == 0)
        {
            lines.Add("No RENTALS found");
        }
        else
        {
            foreach (var rental in filteredRentals)
            {
                lines.Add($"{rental}");
            }
        }
        lines.Add($"Total: {filteredRentals.Count}");

        return string.Join(Environment.NewLine, lines);
    }

    public string GenerateSummary()
    {
        var lines = new List<string>();
        var devices = _deviceService.GetAll();
        var rentals = _rentalService.GetAll();
        var users = _userService.GetAll();
        var availableDevices = _deviceService.GetAllAvailable().Count;
        var rentedDevices = _deviceService.GetAllRented().Count;
        var unavailableDevices = _deviceService.GetAllUnavailable().Count;
        var activeRentals = _rentalService.GetAllActive().Count;
        var overdueRentals = _rentalService.GetAllOverdue().Count;
        var completedRentals = _rentalService.GetAllCompleted().Count;
        var totalPenalties = _rentalService.GetTotalPenalties();

        var header = "SUMMARY REPORT";
        lines.Add(header);
        lines.Add($"Total Users: {users.Count}");
        lines.Add($"Total Devices: {devices.Count}");
        lines.Add($"\tAvailable: {availableDevices}");
        lines.Add($"\tRented: {rentedDevices}");
        lines.Add($"\tUnavailable: {unavailableDevices}");
        lines.Add($"Total Rentals: {rentals.Count}");
        lines.Add($"\tActive: {activeRentals}");
        lines.Add($"\tOverdue: {overdueRentals}");
        lines.Add($"\tCompleted: {completedRentals}");
        lines.Add($"Total Penalties: {totalPenalties:C}");

        return string.Join(Environment.NewLine, lines);
    }

    private static string GetFilterDisplay(DeviceFilter filter) => filter switch
    {
        DeviceFilter.Available => "Available",
        DeviceFilter.Rented => "Rented",
        DeviceFilter.Unavailable => "Unavailable",
        _ => "All"
    };

    private static string GetFilterDisplay(RentalFilter filter) => filter switch
    {
        RentalFilter.Active => "Active",
        RentalFilter.Completed => "Completed",
        RentalFilter.Overdue => "Overdue",
        _ => "All"
    };
}