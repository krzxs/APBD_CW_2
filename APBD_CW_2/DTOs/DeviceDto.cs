using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Exceptions;

namespace APBD_CW_2.DTOs;

public record DeviceDto(
    string Type,
    string Id,
    string Name,
    DeviceStatus Status,
    string? Processor,
    int? Ram,
    string? GraphicsCard,
    string? Resolution,
    int? Lumens,
    double? MegaPixels,
    int? BatteryLifeShots)
{
    public static DeviceDto From(Device d) => d switch
    {
        Laptop l => new DeviceDto(d.GetDeviceType(), l.Id, l.Name, l.Status,
            l.Processor, l.Ram, l.GraphicsCard,
            null, null,
            null, null),
        Projector p => new DeviceDto(d.GetDeviceType(), p.Id, p.Name, p.Status,
            null, null, null,
            p.Resolution, p.Lumens,
            null, null),
        Camera c => new DeviceDto(d.GetDeviceType(), c.Id, c.Name, c.Status,
            null, null, null,
            null, null,
            c.MegaPixels, c.BatteryLifeShots),
        _ => throw new UnknownTypeException(d.GetDeviceType())
    };

    public Device ToDevice() => Type switch
    {
        "Laptop" => Laptop.Restore(Id, Name, Status, Processor!, Ram!.Value, GraphicsCard!),
        "Projector" => Projector.Restore(Id, Name, Status, Resolution!, Lumens!.Value),
        "Camera" => Camera.Restore(Id, Name, Status, MegaPixels!.Value, BatteryLifeShots!.Value),
        _ => throw new UnknownTypeException(Type)
    };
}