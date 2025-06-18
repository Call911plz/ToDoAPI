using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    public Task<User> CreateUserAsync(User newUser);
    public List<User> GetUsers();
    public Task<User> UpdateUserAsync(User userToUpdate);
    public Task<bool> DeleteUserAsync(User userToDelete);
    public string GenerateToken(int tokenLength);
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

    public string GenerateToken(int tokenLength)
    {
        string token = "";
        Random rnd = new();

        for (int i = 0; i < tokenLength; i++)
        {
            int character = rnd.Next(0, 62);
            int startPos = 0;
            switch (character)
            {
                case > 35:
                    character -= 35;
                    startPos = 97;
                    break;
                case > 9:
                    character -= 9;
                    startPos = 65;
                    break;
                default:
                    startPos = 48;
                    break;
            }
            token += Convert.ToChar(startPos + character);
        }

        if (token.All(char.IsLetterOrDigit) == false)
            throw new Exception("Token generated non-alphanumeric letter");

        return token;
    }
}