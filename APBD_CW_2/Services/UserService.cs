using APBD_CW_2.Domain.Users;
using APBD_CW_2.Exceptions;
using APBD_CW_2.Repositories;

namespace APBD_CW_2.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Add(User user)
    {
        _userRepository.Add(user);
    }

    public User FindById(string id)
    {
        var user = _userRepository.FindById(id);
        return user ?? throw new UserNotFoundException(id);
    }

    public List<User> GetAll()
    {
        return _userRepository.GetAll();
    }
}