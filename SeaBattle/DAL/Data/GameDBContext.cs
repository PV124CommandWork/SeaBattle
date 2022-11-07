using GameDBContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameDBContext.Data
{
    public class GameDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeaBattleDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<CurrentBattle> CurrentBattles { get; set; }
    }
}
