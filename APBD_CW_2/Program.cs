using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Domain.Users;
using APBD_CW_2.Services;
using APBD_CW_2.UI;

var dataService = new DataService("data.json");
var (deviceRepository, userRepository, rentalRepository) = dataService.Load();

var deviceService = new DeviceService(deviceRepository);
var userService = new UserService(userRepository);
var rentalService = new RentalService(rentalRepository, userService, deviceService);
var reportService = new ReportService(deviceService, rentalService, userService);

if (userService.GetAll().Count == 0 && deviceService.GetAll().Count == 0 && rentalService.GetAll().Count == 0)
    ExampleData(userService, deviceService);

var menu = new ConsoleMenu(dataService, deviceService, rentalService, reportService, userService);
menu.Run();

return;

static void ExampleData(UserService users, DeviceService devices)
{
    users.Add(new Student("Anna", "Kowalska"));
    users.Add(new Student("Piotr", "Nowak"));
    users.Add(new Employee("Maria", "Wiśniewska"));
    users.Add(new Employee("Tomasz", "Zając"));

    devices.Add(new Laptop("Dell XPS 15", "Intel Core i9-13900H", 32768, "NVIDIA RTX 4060"));
    devices.Add(new Laptop("MacBook Pro M3", "Apple M3 Pro", 18432, "Apple M3 Pro GPU"));
    devices.Add(new Laptop("Lenovo ThinkPad X1", "Intel Core i7-1365U", 16384, "Intel Iris Xe"));
    devices.Add(new Projector("Epson EB-W51", "WXGA 1280×800", 3800));
    devices.Add(new Projector("BenQ MH560", "Full HD 1920×1080", 4000));
    devices.Add(new Camera("Canon EOS R50", 24, 390));
    devices.Add(new Camera("Sony Alpha A7 IV", 33, 520));
}