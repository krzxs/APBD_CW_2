using APBD_CW_2.Domain.Users;

namespace APBD_CW_2.Repositories;

public class UserRepository
{
    private readonly List<User> _users;

    public UserRepository()
    {
        _users = new();
    }

    public UserRepository(List<User> users)
    {
        _users = users;
    }

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? FindById(string id)
    {
        return _users.FirstOrDefault(u => u.Id.Equals(id));
    }

    public List<User> GetAll()
    {
        return _users;
    }
}