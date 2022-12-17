using Application.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;
using VSporte.Task.Infrastructure.Persistence;
using VSporte.Task.API.Entities;
using VSporte.Task.API.Models;

namespace VSporte.Task.API.Services
{
    public class PlayerClubService : RepositoryBase<PlayerClubEntity>, IPlayerClubService
    {
        private readonly AppDbContext _context;

        public PlayerClubService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CreatePlayerClubAsync(PlayerClubDto dto)
        {
            var entity = new PlayerClubEntity
            {
                PlayerId = dto.PlayerId,
                ClubId = dto.ClubId
            };

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                await CreateAsync(entity);
                await SaveChangesAsync();

                transaction.Commit();

                return entity.Id;
            }
            catch
            {
                transaction.Rollback();

                throw new WarningException("Something went wrong...");
            }
        }

        public async Task<int> DeletePlayerClubAsync(int playerClubId)
        {
            var entity = await FindByCondition(o => o.Id == playerClubId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return 0;

            Delete(entity);

            await SaveChangesAsync();

            return entity.Id;
        }

        public async Task<PlayerClubDto> GetPlayerClubByIdAsync(int playerClubId)
        {
            var entity = await FindByCondition(o => o.Id == playerClubId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return null;

            return new PlayerClubDto
            {
                Id = entity.Id,
                PlayerId = entity.PlayerId,
                ClubId = entity.ClubId
            };
        }

        public async Task<List<PlayerClubDto>> GetPlayerClubListAsync()
        {
            var entities = await FindAll()
                .ToListAsync();

            List<PlayerClubDto> playerClubList = new List<PlayerClubDto>();

            entities.ForEach(i => playerClubList.Add(new PlayerClubDto
            {
                Id = i.Id,
                PlayerId = i.PlayerId,
                ClubId = i.ClubId
            }));

            return playerClubList;
        }

        public async Task<int> UpdatePlayerClubAsync(PlayerClubDto dto)
        {
            var entity = await FindByCondition(o => o.Id == dto.Id)
                .SingleOrDefaultAsync();

            if (entity == null)
                return 0;

            dto.UpdateEntity(entity);

            Update(entity);

            await SaveChangesAsync();

            return entity.Id;
        }
    }
}
