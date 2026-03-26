namespace APBD_CW_2.Domain.Devices;

public abstract class Device : BaseObject
{
    public string Name { get; set; }
    public DeviceStatus Status { get; set; }

    protected Device(string name)
    {
        Name = name;
        Status = DeviceStatus.Available;
    }

    protected Device(string id, string name, DeviceStatus status) : base(id)
    {
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