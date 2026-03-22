using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Domain.Filters;
using APBD_CW_2.Domain.Users;
using APBD_CW_2.Exceptions;
using APBD_CW_2.Services;

namespace APBD_CW_2.UI;

public class ConsoleMenu
{
    private readonly DataService _dataService;
    private readonly DeviceService _deviceService;
    private readonly RentalService _rentalService;
    private readonly ReportService _reportService;
    private readonly UserService _userService;

    public ConsoleMenu(DataService dataService, DeviceService deviceService, RentalService rentalService, ReportService reportService, UserService userService)
    {
        _dataService = dataService;
        _deviceService = deviceService;
        _rentalService = rentalService;
        _reportService = reportService;
        _userService = userService;
    }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        PrintWelcomeMessage();

        while (true)
        {
            PrintMainMenu();
            var choice = ReadLine("Your choice: ");

            try
            {
                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        ListUsers();
                        break;
                    case "3":
                        AddDevice();
                        break;
                    case "4":
                        ListDevices();
                        break;
                    case "5":
                        MarkDeviceUnavailable();
                        break;
                    case "6":
                        MarkDeviceAvailable();
                        break;
                    case "7":
                        RentDevice();
                        break;
                    case "8":
                        ReturnDevice();
                        break;
                    case "9":
                        ShowUserRentals();
                        break;
                    case "10":
                        ShowRentalReport();
                        break;
                    case "11":
                        ShowSummaryReport();
                        break;
                    case "12":
                        SaveData();
                        break;
                    case "0":
                        SaveOnExit();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Enter a number from the menu.");
                        break;
                }
            }
            catch (AppException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown error: {e.Message}.");
            }

            Console.WriteLine();
        }
    }

    private void AddUser()
    {
        Console.WriteLine("Add User");
        Console.WriteLine("(1) Student\t(2) Employee");
        var type = ReadLine("Enter the type of user: ");
        var firstName = ReadLine("Enter the first name: ");
        var lastName = ReadLine("Enter the last name: ");
        User user = type switch
        {
            "1" => new Student(firstName, lastName),
            "2" => new Employee(firstName, lastName),
            _ => throw new ArgumentException("Unknown type. Enter 1 or 2")
        };
        _userService.Add(user);
        Console.WriteLine($"Added user: {user}.");
    }

    private void ListUsers()
    {
        Console.WriteLine("Listing Users");
        var users = _userService.GetAll();
        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }

        Console.WriteLine($"Total users: {users.Count}");
    }

    private void AddDevice()
    {
        Console.WriteLine("Add Device");
        Console.WriteLine("(1) Laptop\t(2) Projector\t(3) Camera");
        var type = ReadLine("Enter the type of device: ");
        var name = ReadLine("Enter the name of the device: ");
        Device device = type switch
        {
            "1" => new Laptop(name,
                ReadLine("Enter the processor model: "),
                ReadInt("Enter the RAM amount (in MB): "),
                ReadLine("Enter the graphics card model: ")),
            "2" => new Projector(name,
                ReadLine("Enter the resolution: "),
                ReadInt("Enter the brightness (in lumens): ")),
            "3" => new Camera(name,
                ReadDouble("Enter the megapixels count: "),
                ReadInt("Enter the battery life (number of shots): ")),
            _ => throw new ArgumentException("Unknown type. Enter 1, 2 or 3")
        };
        _deviceService.Add(device);
        Console.WriteLine($"Added device: {device}.");
    }

    private void ListDevices()
    {
        Console.WriteLine("Listing Devices");
        Console.WriteLine("Filter: (1) All\t(2) Available\t(3) Rented\t(4) Unavailable");
        var filter = ReadLine("Choose filter [1]: ") switch
        {
            "2" => DeviceFilter.Available,
            "3" => DeviceFilter.Rented,
            "4" => DeviceFilter.Unavailable,
            _ => DeviceFilter.All
        };
        Console.WriteLine(_reportService.GenerateDeviceReport(filter));
    }

    private void MarkDeviceUnavailable()
    {
        Console.WriteLine("Mark device as unavailable");
        Console.WriteLine(_reportService.GenerateDeviceReport(DeviceFilter.Available));
        var id = ReadLine("Enter the id of the device: ");
        _deviceService.MarkUnavailable(id);
        Console.WriteLine($"Device with id {id} marked as unavailable.");
    }

    private void MarkDeviceAvailable()
    {
        Console.WriteLine("Mark device as available");
        Console.WriteLine(_reportService.GenerateDeviceReport(DeviceFilter.Unavailable));
        var id = ReadLine("Enter the id of the device: ");
        _deviceService.MarkAvailable(id);
        Console.WriteLine($"Device with id {id} marked as available.");
    }

    private void RentDevice()
    {
        Console.WriteLine("Rent Device");
        ListUsers();
        var userId = ReadLine("Enter the user id: ");

        Console.WriteLine(_reportService.GenerateDeviceReport(DeviceFilter.Available));
        var deviceId = ReadLine("Enter the id of the device: ");

        var days = ReadInt("Enter the days of the rental: ");
        var rental = _rentalService.RentDevice(userId, deviceId, days);
        Console.WriteLine($"Rented successfully: {rental}.");
    }

    private void ReturnDevice()
    {
        Console.WriteLine("Return Device");
        Console.WriteLine(_reportService.GenerateRentalReport(RentalFilter.Active));
        Console.WriteLine(_reportService.GenerateRentalReport(RentalFilter.Overdue));

        var rentalId = ReadLine("Enter the id of the rental: ");
        _rentalService.ReturnDevice(rentalId);
        var rental = _rentalService.FindById(rentalId);
        Console.WriteLine($"Rental successfully returned: {rental}.");
    }

    private void ShowUserRentals()
    {
        Console.WriteLine("Show User Rentals");
        ListUsers();
        var userId = ReadLine("Enter the id of the user: ");
        Console.WriteLine("Filter: (1) All\t(2) Active\t(3) Overdue\t(4) Completed");
        var filter = ReadLine("Choose filter [1]: ") switch
        {
            "2" => RentalFilter.Active,
            "3" => RentalFilter.Overdue,
            "4" => RentalFilter.Completed,
            _ => RentalFilter.All
        };
        Console.WriteLine(_reportService.GenerateRentalReport(filter, userId));
    }

    private void ShowRentalReport()
    {
        Console.WriteLine("Rentals Report");
        Console.WriteLine("Filter: (1) All\t(2) Active\t(3) Overdue\t(4) Completed");
        var filter = ReadLine("Choose filter [1]: ") switch
        {
            "2" => RentalFilter.Active,
            "3" => RentalFilter.Overdue,
            "4" => RentalFilter.Completed,
            _ => RentalFilter.All
        };
        Console.WriteLine(_reportService.GenerateRentalReport(filter));
    }

    private void ShowSummaryReport()
    {
        Console.WriteLine(_reportService.GenerateSummary());
    }

    private void SaveData()
    {
        _dataService.Save(_deviceService, _userService, _rentalService);
        Console.WriteLine("Data saved.");
    }

    private void SaveOnExit()
    {
        SaveData();
        Console.WriteLine("Goodbye!");
    }

    private void PrintWelcomeMessage()
    {
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        Console.WriteLine("Welcome to UNIVERSITY DEVICES RENTAL SYSTEM");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
    }

    private void PrintMainMenu()
    {
        Console.WriteLine("RENTAL SYSTEM - MAIN MENU");
        Console.WriteLine("[USERS]");
        Console.WriteLine("1. Add User");
        Console.WriteLine("2. List Users");
        Console.WriteLine("[DEVICES]");
        Console.WriteLine("3. Add Device");
        Console.WriteLine("4. List Devices");
        Console.WriteLine("5. Mark Device Unavailable");
        Console.WriteLine("6. Make Device Available Again");
        Console.WriteLine("[RENTALS]");
        Console.WriteLine("7. Rent Device");
        Console.WriteLine("8. Accept The Return");
        Console.WriteLine("9. List User's Rent Devices");
        Console.WriteLine("[REPORTS]");
        Console.WriteLine("10. Rentals Report");
        Console.WriteLine("11. Summary Report");
        Console.WriteLine("[DATA]");
        Console.WriteLine("12. Save data to file");
        Console.WriteLine("[OTHER]");
        Console.WriteLine("0. Exit (auto save)");
    }

    private string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    private int ReadInt(string prompt)
    {
        while (true)
        {
            var raw = ReadLine(prompt);
            if (int.TryParse(raw, out var v)) return v;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    private double ReadDouble(string prompt)
    {
        while (true)
        {
            var raw = ReadLine(prompt);
            if (double.TryParse(raw, out var v)) return v;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}