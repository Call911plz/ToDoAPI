using Microsoft.EntityFrameworkCore;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
}