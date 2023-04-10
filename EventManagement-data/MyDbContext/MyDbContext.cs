using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
    // Add DbSet properties for your entities here
    public DbSet<Event> Events { get; set; }

    public DbSet<User> Users { get; set; }
}