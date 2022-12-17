using Application.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;
using VSporte.Task.Infrastructure.Persistence;
using VSporte.Task.API.Entities;

namespace VSporte.Task.API.Services
{
    public class PlayerService : RepositoryBase<PlayerEntity>, IPlayerService
    {
        private readonly AppDbContext _context;

        public PlayerService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CreatePlayerAsync(PlayerDto dto)
        {
            var entity = new PlayerEntity
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Number = dto.Number
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

        public async Task<int> DeletePlayerAsync(int playerId)
        {
            var entity = await FindByCondition(o => o.Id == playerId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return 0;

            Delete(entity);

            await SaveChangesAsync();

            return entity.Id;
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int playerId)
        {
            var entity = await FindByCondition(o => o.Id == playerId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return null;

            return new PlayerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Patronymic = entity.Patronymic,
                Number = entity.Number
            };
        }

        public async Task<List<PlayerDto>> GetPlayerListAsync()
        {
            var entities = await FindAll()
                .ToListAsync();

            List<PlayerDto> playerList = new List<PlayerDto>();

            entities.ForEach(i => playerList.Add(new PlayerDto
            {
                Id = i.Id,
                Name = i.Name,
                Surname = i.Surname,
                Patronymic = i.Patronymic,
                Number = i.Number
            }));

            return playerList;
        }

        public async Task<int> UpdatePlayerAsync(PlayerDto dto)
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
