public interface IUserService
{
    public Task<LoginAuthToken> CreateUserAsync(User newUser);
    public List<User> GetUsers();
    public Task<User> UpdateUserAsync(User userToUpdate);
    public Task<bool> DeleteUserAsync(User userToDelete);
    public LoginAuthToken? LoginUser(User userDetail);
}

public class UserService : IUserService
{
    IUserRepository _repo;
    public UserService(IUserRepository userRepository)
    {
        _repo = userRepository;
    }

    public async Task<LoginAuthToken> CreateUserAsync(User newUser)
    {
        await _repo.CreateUserAsync(newUser);
        return _repo.GenerateToken(newUser.Password);
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

    public LoginAuthToken? LoginUser(User userDetail)
    {
        List<User> existingUsers = _repo.GetUsers();
        if (existingUsers.Contains(userDetail))
            return _repo.GenerateToken(userDetail.Password);
        return null;
    }
}