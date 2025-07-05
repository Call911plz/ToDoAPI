using Microsoft.EntityFrameworkCore;
using Isopoh.Cryptography.Argon2;

public interface IUserRepository
{
    public Task<User> CreateUserAsync(User newUser);
    public List<User> GetUsers();
    public Task<User> UpdateUserAsync(User userToUpdate);
    public Task<bool> DeleteUserAsync(User userToDelete);
    public LoginAuthToken GenerateToken(string password);
}

public class UserRepository(ToDoDbContext context) : IUserRepository
{
    protected readonly ToDoDbContext _context = context;

    public async Task<User> CreateUserAsync(User newUser)
    {
        var savedUser = _context.Users.Add(newUser);

        await _context.SaveChangesAsync();

        return savedUser.Entity;
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public async Task<User> UpdateUserAsync(User userToUpdate)
    {
        User userInDb = await _context.Users.SingleAsync(user => userToUpdate.Id == user.Id);

        userInDb.Email = userToUpdate.Email;
        userInDb.Name = userToUpdate.Name;
        userInDb.Password = userToUpdate.Password;

        await _context.SaveChangesAsync();

        return userInDb;
    }

    public async Task<bool> DeleteUserAsync(User userToDelete)
    {
        return await _context.Users
            .Where(user => user.Id == userToDelete.Id)
            .ExecuteDeleteAsync() > 0;
    }

    public LoginAuthToken GenerateToken(string password)
    {
        LoginAuthToken token = new LoginAuthToken()
        {
            Token = Argon2.Hash(password)
        };

        return token;
    }
}