using System.Text.Json;
using System.Text.Json.Serialization;
using APBD_CW_2.DTOs;
using APBD_CW_2.Repositories;

namespace APBD_CW_2.Services;

file class DataSnapshot
{
    public List<DeviceDto> Devices { get; set; } = new();
    public List<UserDto> Users { get; set; } = new();
    public List<RentalDto> Rentals { get; set; } = new();
}

public class DataService
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    public DataService(string filePath)
    {
        _filePath = filePath;
    }

    public void Save(DeviceService deviceService, UserService userService, RentalService rentalService)
    {
        var snapshot = new DataSnapshot
        {
            Devices = deviceService.GetAll().Select(DeviceDto.From).ToList(),
            Users = userService.GetAll().Select(UserDto.From).ToList(),
            Rentals = rentalService.GetAll().Select(RentalDto.From).ToList()
        };
        var json = JsonSerializer.Serialize(snapshot, Options);
        File.WriteAllText(_filePath, json);
    }

    public (DeviceRepository, UserRepository, RentalRepository) Load()
    {
        if (!File.Exists(_filePath))
        {
            return (new DeviceRepository(), new UserRepository(), new RentalRepository());
        }

        var json = File.ReadAllText(_filePath);
        var snapshot = JsonSerializer.Deserialize<DataSnapshot>(json, Options) ?? new DataSnapshot();
        var devices = snapshot.Devices.Select(d => d.ToDevice()).ToList();
        var users = snapshot.Users.Select(u => u.ToUser()).ToList();
        var devicesById = devices.ToDictionary(d => d.Id);
        var usersById = users.ToDictionary(u => u.Id);
        var rentals = snapshot.Rentals
            .Where(r => usersById.ContainsKey(r.UserId) && devicesById.ContainsKey(r.DeviceId))
            .Select(r => r.ToRental(usersById[r.UserId], devicesById[r.DeviceId]))
            .ToList();

        return (new DeviceRepository(devices), new UserRepository(users), new RentalRepository(rentals));
    }
}