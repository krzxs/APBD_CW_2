namespace APBD_CW_2.Domain.Devices;

public class Laptop : Device
{
    public string Processor { get; set; }
    public int Ram { get; set; }
    public string GraphicsCard { get; set; }

    public Laptop(string name, string processor, int ram, string graphicsCard) : base(name)
    {
        Processor = processor;
        Ram = ram;
        GraphicsCard = graphicsCard;
    }

    private Laptop(string id, string name, DeviceStatus status, string processor, int ram, string graphicsCard) : base(id, name, status)
    {
        Processor = processor;
        Ram = ram;
        GraphicsCard = graphicsCard;
    }

    public static Laptop Restore(string id, string name, DeviceStatus status, string processor, int ram,
        string graphicsCard)
    {
        return new Laptop(id, name, status, processor, ram, graphicsCard);
    }

    public override string GetDeviceType()
    {
        return "Laptop";
    }

    public override string GetSpecificDescription()
    {
        return $"CPU: {Processor}, RAM: {Ram} MB, GPU: {GraphicsCard}";
    }
}