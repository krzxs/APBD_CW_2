namespace APBD_CW_2.Domain.Devices;

public class Camera : Device
{
    public double MegaPixels { get; set; }
    public int BatteryLifeShots { get; set; }

    public Camera(string name, double megaPixels, int batteryLifeShots) : base(name)
    {
        MegaPixels = megaPixels;
        BatteryLifeShots = batteryLifeShots;
    }

    private Camera(string id, string name, DeviceStatus status, double megaPixels, int batteryLifeShots) : base(id, name, status)
    {
        MegaPixels = megaPixels;
        BatteryLifeShots = batteryLifeShots;
    }

    public static Camera Restore(string id, string name, DeviceStatus status, double megaPixels, int batteryLifeShots)
    {
        return new Camera(id, name, status, megaPixels, batteryLifeShots);
    }

    public override string GetDeviceType()
    {
        return "Camera";
    }

    public override string GetSpecificDescription()
    {
        return $"Effective Pixels: {MegaPixels} M, Battery Life: {BatteryLifeShots} Shots";
    }
}