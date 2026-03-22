using APBD_CW_2.Domain;

namespace APBD_CW_2.Repositories;

public class RentalRepository
{
    private readonly List<Rental> _rentals;

    public RentalRepository()
    {
        _rentals = new();
    }

    public RentalRepository(List<Rental> rentals)
    {
        _rentals = rentals;
    }

    public void Add(Rental rental)
    {
        _rentals.Add(rental);
    }

    public Rental? FindById(string id)
    {
        return _rentals.FirstOrDefault(r => r.Id.Equals(id));
    }

    public List<Rental> GetAll()
    {
        return _rentals;
    }
}