using APBD_CW_2.Domain.Devices;

namespace APBD_CW_2.Repositories;

public class DeviceRepository
{
    private readonly List<Device> _devices;

    public DeviceRepository()
    {
        _devices = new();
    }

    public DeviceRepository(List<Device> devices)
    {
        _devices = devices;
    }

    public void Add(Device device)
    {
        _devices.Add(device);
    }

    public Device? FindById(string id)
    {
        return _devices.FirstOrDefault(d => d.Id.Equals(id));
    }

    public List<Device> GetAll()
    {
        return _devices;
    }
}