using Microsoft.EntityFrameworkCore;
using VSporte.Task.API.Entities;
using VSporte.Task.API.Models;

namespace VSporte.Task.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<ClubEntity> Clubs { get; set; }
        public virtual DbSet<PlayerEntity> Player { get; set; }
        public virtual DbSet<PlayerClubEntity> PlayerClubs { get; set; }
        public virtual DbSet<GameEventEntity> GameEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}