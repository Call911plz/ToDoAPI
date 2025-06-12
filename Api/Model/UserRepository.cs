using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    public Task<User> CreateUser(User newUser);
    public List<User> GetUsers();
    public Task<User> UpdateUser(User userToUpdate);
    public Task<bool> DeleteUser(User userToDelete);
}

public class UserRepository(ToDoDbContext context) : IUserRepository
{
    protected readonly ToDoDbContext _context = context;

    public async Task<User> CreateUser(User newUser)
    {
        var savedUser = _context.Users.Add(newUser);

        await _context.SaveChangesAsync();

        return savedUser.Entity;
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public async Task<User> UpdateUser(User userToUpdate)
    {
        User userInDb = await _context.Users.SingleAsync(user => userToUpdate.Id == user.Id);

        userInDb.Email = userToUpdate.Email;
        userInDb.Name = userToUpdate.Name;
        userInDb.Password = userToUpdate.Password;

        await _context.SaveChangesAsync();

        return userInDb;
    }

    public async Task<bool> DeleteUser(User userToDelete)
    {
        return await _context.Users
            .Where(user => user.Id == userToDelete.Id)
            .ExecuteDeleteAsync() > 0;
    }
}