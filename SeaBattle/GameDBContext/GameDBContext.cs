using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameDBContext
{
    public class GameDbContext : DbContext
    {
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Rewards> Rewards { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CurrentBattles> CurrentBattles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GameDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
