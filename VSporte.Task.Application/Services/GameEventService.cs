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
    public class GameEventService : RepositoryBase<GameEventEntity>, IGameEventService
    {
        private readonly AppDbContext _context;

        public GameEventService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CreateGameEventAsync(GameEventDto dto)
        {
            var entity = new GameEventEntity
            {
                PlayerId = dto.PlayerId,
                ClubId = dto.ClubId,
                Type = dto.Type,
                MomentTime  = dto.MomentTime
            };

            try
            {
                await CreateAsync(entity);
                await SaveChangesAsync();

                return entity.Id;
            }
            catch
            {
                throw new WarningException("Something went wrong...");
            }
        }

        public async Task<int> DeleteGameEventAsync(int gameEventId)
        {
            var entity = await FindByCondition(o => o.Id == gameEventId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return 0;

            Delete(entity);

            await SaveChangesAsync();

            return entity.Id;
        }

        public async Task<GameEventDto> GetGameEventByIdAsync(int gameEventId)
        {
            var entity = await FindByCondition(o => o.Id == gameEventId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return null;

            return new GameEventDto
            {
                Id = entity.Id,
                PlayerId = entity.PlayerId,
                ClubId = entity.ClubId,
                Type = entity.Type,
                MomentTime = entity.MomentTime
            };
        }

        public async Task<List<GameEventDto>> GetGameEventListAsync()
        {
            var entities = await FindAll()
                .ToListAsync();

            List<GameEventDto> gameEventList = new List<GameEventDto>();

            entities.ForEach(i => gameEventList.Add(new GameEventDto
            {
                Id = i.Id,
                PlayerId = i.PlayerId,
                ClubId = i.ClubId,
                Type = i.Type,
                MomentTime = i.MomentTime
            }));

            return gameEventList;
        }

        public async Task<int> UpdateGameEventAsync(GameEventDto dto)
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
