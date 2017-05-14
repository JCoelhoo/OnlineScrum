using OnlineScrum.Models;
using System.Data.Entity;

namespace OnlineScrum
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}