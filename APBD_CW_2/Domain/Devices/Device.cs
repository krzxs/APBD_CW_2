namespace APBD_CW_2.Domain.Devices;

public abstract class Device
{
    public string Id { get; }
    public string Name { get; set; }
    public DeviceStatus Status { get; set; }

    protected Device(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Status = DeviceStatus.Available;
    }

    protected Device(string id, string name, DeviceStatus status)
    {
        Id = id;
        Name = name;
        Status = status;
    }

    private string GetStatusDisplay() => Status switch
    {
        DeviceStatus.Available => "Available",
        DeviceStatus.Rented => "Rented",
        DeviceStatus.Unavailable => "Unavailable",
        _ => "Unknown"
    };

    public abstract string GetDeviceType();
    public abstract string GetSpecificDescription();

    public override string ToString()
    {
        return $"({Id}) {GetDeviceType()}: {Name} | {GetSpecificDescription()} | {GetStatusDisplay()}";
    }
}