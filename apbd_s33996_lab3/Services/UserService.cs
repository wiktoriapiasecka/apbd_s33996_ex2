using apbd_s33996_lab3.Models.Users;
using apbd_s33996_lab3.Repositories;

namespace apbd_s33996_lab3.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(User user)
    {
        _userRepository.Add(user);
    }

    public List<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User? GetUserById(int id)
    {
        return _userRepository.GetById(id);
    }
}