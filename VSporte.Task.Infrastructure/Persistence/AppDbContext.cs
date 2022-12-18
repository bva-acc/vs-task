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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClubEntity>().HasData(
                new ClubEntity { Id = 1, Name = "Спартак", ShortName = "Спартак" },
                new ClubEntity { Id = 2, Name = "Крылья Советов", ShortName = "КС" });

            modelBuilder.Entity<PlayerEntity>().HasData(
                new PlayerEntity { Id = 1, Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", Number = 11 },
                new PlayerEntity { Id = 2, Name = "Петр", Surname = "Петров", Patronymic = "Петрович", Number = 22 },
                new PlayerEntity { Id = 3, Name = "Сидор", Surname = "Сидоров", Patronymic = "Сидорович", Number = 33 });

            modelBuilder.Entity<PlayerClubEntity>().HasData(
                new PlayerClubEntity { Id = 1, ClubId = 1, PlayerId = 1 },
                new PlayerClubEntity { Id = 2, ClubId = 2, PlayerId = 2 },
                new PlayerClubEntity { Id = 3, ClubId = 2, PlayerId = 3 });

            modelBuilder.Entity<GameEventEntity>().HasData(
               new GameEventEntity { Id = 1, ClubId = 1, PlayerId = 1, Type = "Забил мяч в свои ворота", MomentTime = "2" },
               new GameEventEntity { Id = 2, ClubId = 2, PlayerId = 2, Type = "Получил желтую карточку", MomentTime = "45+2" });
        }
    }
}