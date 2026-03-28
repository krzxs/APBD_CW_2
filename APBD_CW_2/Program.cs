using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Domain.Users;
using APBD_CW_2.Exceptions;
using APBD_CW_2.Services;
using APBD_CW_2.UI;

var dataService = new DataService("data.json");
var (deviceRepository, userRepository, rentalRepository) = dataService.Load();

var deviceService = new DeviceService(deviceRepository);
var userService = new UserService(userRepository);
var rentalService = new RentalService(rentalRepository, userService, deviceService);
var reportService = new ReportService(deviceService, rentalService, userService);

if (userService.GetAll().Count == 0 && deviceService.GetAll().Count == 0 && rentalService.GetAll().Count == 0)
    ExampleData(userService, deviceService, rentalService, reportService);

var menu = new ConsoleMenu(dataService, deviceService, rentalService, reportService, userService);
menu.Run();

return;

static void ExampleData(UserService users, DeviceService devices, RentalService rentals, ReportService reports)
{
    Console.WriteLine("Adding Example Users...");
    var user = new Student("Anna", "Kowalska");
    users.Add(user);
    users.Add(new Student("Piotr", "Nowak"));
    users.Add(new Employee("Maria", "Wiśniewska"));
    users.Add(new Employee("Tomasz", "Zając"));

    Console.WriteLine("Adding Example Devices...");
    var device1 = new Laptop("Dell XPS 15", "Intel Core i9-13900H", 32768, "NVIDIA RTX 4060");
    devices.Add(device1);
    devices.Add(new Laptop("MacBook Pro M3", "Apple M3 Pro", 18432, "Apple M3 Pro GPU"));
    devices.Add(new Laptop("Lenovo ThinkPad X1", "Intel Core i7-1365U", 16384, "Intel Iris Xe"));
    var device2 = new Projector("Epson EB-W51", "WXGA 1280×800", 3800);
    devices.Add(device2);
    devices.Add(new Projector("BenQ MH560", "Full HD 1920×1080", 4000));
    var device3 = new Camera("Canon EOS R50", 24, 390);
    devices.Add(device3);
    devices.Add(new Camera("Sony Alpha A7 IV", 33, 520));

    Console.WriteLine("Adding Example Rentals...");
    try
    {
        rentals.RentDevice(user.Id, device1.Id, 1);
        rentals.RentDevice(user.Id, device2.Id, 2);
        rentals.RentDevice(user.Id, device3.Id, 3);
    }
    catch (RentalLimitExceededException e)
    {
        Console.WriteLine(e.Message);
    }

    rentals.GetAll()[1].StartDate = DateTime.Now.AddDays(-3);
    rentals.GetAll()[1].DueDate = DateTime.Now.AddDays(-1);

    Console.WriteLine("Returning Example Rentals...");
    rentals.ReturnDevice(rentals.GetAll()[0].Id);
    rentals.ReturnDevice(rentals.GetAll()[1].Id);

    Console.WriteLine("Displaying Summary Report...");
    Console.WriteLine(reports.GenerateSummary());

    Console.WriteLine("Displaying Rentals Report...");
    Console.WriteLine(reports.GenerateRentalReport());
}