public interface IUserService
{
    public Task<User> CreateUserAsync(User newUser);
    public List<User> GetUsers();
    public Task<User> UpdateUserAsync(User userToUpdate);
    public Task<bool> DeleteUserAsync(User userToDelete);
    public string LoginUser(User userDetail);
}

public class UserService : IUserService
{
    IUserRepository _repo;
    public UserService(IUserRepository userRepository)
    {
        _repo = userRepository;
    }

    public async Task<User> CreateUserAsync(User newUser)
    {
        return await _repo.CreateUserAsync(newUser);
    }

    public List<User> GetUsers()
    {
        return _repo.GetUsers();
    }

    public async Task<User> UpdateUserAsync(User userToUpdate)
    {
        return await _repo.UpdateUserAsync(userToUpdate);
    }

    public async Task<bool> DeleteUserAsync(User userToDelete)
    {
        return await _repo.DeleteUserAsync(userToDelete);
    }

    public string LoginUser(User userDetail)
    {
        List<User> existingUsers = _repo.GetUsers();
        if (existingUsers.Contains(userDetail))
            return _repo.GenerateToken(Const.TOKEN_LENGTH);
        return string.Empty;
    }
}