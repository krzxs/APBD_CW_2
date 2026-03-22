using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Exceptions;
using APBD_CW_2.Repositories;

namespace APBD_CW_2.Services;

public class DeviceService
{
    private readonly DeviceRepository _deviceRepository;

    public DeviceService(DeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public void Add(Device device)
    {
        _deviceRepository.Add(device);
    }

    public Device FindById(string id)
    {
        var device = _deviceRepository.FindById(id);
        return device ?? throw new DeviceNotFoundException(id);
    }

    public void MarkUnavailable(string id)
    {
        var device = FindById(id);
        if (device.Status == DeviceStatus.Available)
        {
            device.Status = DeviceStatus.Unavailable;
        }
    }

    public void MarkAvailable(string id)
    {
        var device = FindById(id);
        if (device.Status == DeviceStatus.Unavailable)
        {
            device.Status = DeviceStatus.Available;
        }
    }

    public List<Device> GetAll()
    {
        return _deviceRepository.GetAll();
    }

    public List<Device> GetAllAvailable()
    {
        return _deviceRepository.GetAll().Where(d => d.Status == DeviceStatus.Available).ToList();
    }

    public List<Device> GetAllRented()
    {
        return _deviceRepository.GetAll().Where(d => d.Status == DeviceStatus.Rented).ToList();
    }

    public List<Device> GetAllUnavailable()
    {
        return _deviceRepository.GetAll().Where(d => d.Status == DeviceStatus.Unavailable).ToList();
    }
}