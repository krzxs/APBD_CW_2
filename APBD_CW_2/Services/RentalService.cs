using APBD_CW_2.Config;
using APBD_CW_2.Domain;
using APBD_CW_2.Domain.Devices;
using APBD_CW_2.Exceptions;
using APBD_CW_2.Repositories;

namespace APBD_CW_2.Services;

public class RentalService
{
    private readonly RentalRepository _rentalRepository;
    private readonly UserService _userService;
    private readonly DeviceService _deviceService;

    public RentalService(RentalRepository rentalRepository, UserService userService, DeviceService deviceService)
    {
        _rentalRepository = rentalRepository;
        _userService = userService;
        _deviceService = deviceService;
    }

    public Rental FindById(string rentalId)
    {
        var rental = _rentalRepository.FindById(rentalId);
        return rental ?? throw new RentalNotFoundException(rentalId);
    }

    public Rental RentDevice(string userId, string deviceId, int days)
    {
        if (days <= 0)
        {
            throw new InvalidRentalDaysException();
        }

        var user = _userService.FindById(userId);
        var device = _deviceService.FindById(deviceId);

        if (device.Status != DeviceStatus.Available)
        {
            throw new DeviceNotAvailableException(deviceId);
        }

        var activeRentals = GetActiveByUser(userId).Count;

        if (activeRentals >= user.RentalLimit)
        {
            throw new RentalLimitExceededException(userId, user.RentalLimit);
        }

        var rental = new Rental(user, device, DateTime.Now, days);
        device.Status = DeviceStatus.Rented;
        _rentalRepository.Add(rental);

        return rental;
    }

    public void ReturnDevice(string rentalId)
    {
        var rental = FindById(rentalId);

        if (!rental.IsActive())
        {
            throw new RentalAlreadyReturnedException(rentalId);
        }

        var returnDate = DateTime.Now;
        var calculatedPenalty = RentalRules.CalculatePenalty(rental.DueDate, returnDate);

        rental.MarkReturned(returnDate, calculatedPenalty);
        rental.Device.Status = DeviceStatus.Available;
    }

    public List<Rental> GetAll()
    {
        return _rentalRepository.GetAll();
    }

    public List<Rental> GetAllActive(string? userId = null)
    {
        return _rentalRepository.GetAll().Where(r => r.IsActive() && !r.IsOverdue() && (userId == null || r.User.Id == userId)).ToList();
    }

    public List<Rental> GetAllOverdue(string? userId = null)
    {
        return _rentalRepository.GetAll().Where(r => r.IsOverdue() && (userId == null || r.User.Id == userId)).ToList();
    }

    public List<Rental> GetAllCompleted(string? userId = null)
    {
        return _rentalRepository.GetAll().Where(r => !r.IsActive() && (userId == null || r.User.Id == userId)).ToList();
    }

    public List<Rental> GetActiveByUser(string userId)
    {
        return _rentalRepository.GetAll().Where(r => r.IsActive() && r.User.Id.Equals(userId)).ToList();
    }

    public decimal GetTotalPenalties()
    {
        return _rentalRepository.GetAll().Where(r => !r.IsActive()).Sum(r => r.PenaltyAmount);
    }
}