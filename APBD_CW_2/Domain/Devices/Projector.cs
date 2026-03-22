namespace APBD_CW_2.Domain.Devices;

public class Projector : Device
{
    public string Resolution { get; set; }
    public int Lumens { get; set; }

    public Projector(string name, string resolution, int lumens) : base(name)
    {
        Resolution = resolution;
        Lumens = lumens;
    }

    private Projector(string id, string name, DeviceStatus status, string resolution, int lumens) : base(id, name, status)
    {
        Resolution = resolution;
        Lumens = lumens;
    }

    public static Projector Restore(string id, string name, DeviceStatus status, string resolution, int lumens)
    {
        return new Projector(id, name, status, resolution, lumens);
    }

    public override string GetDeviceType()
    {
        return "Projector";
    }

    public override string GetSpecificDescription()
    {
        return $"Resolution: {Resolution}, Lumens: {Lumens}";
    }
}