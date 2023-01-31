using Application.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;
using VSporte.Task.Infrastructure.Persistence;
using VSporte.Task.API.Models;

namespace VSporte.Task.API.Services
{
    public class ClubService : RepositoryBase<ClubEntity>, IClubService
    {
        private readonly AppDbContext _context;

        public ClubService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CreateClubAsync(ClubDto dto)
        {
            var entity = new ClubEntity
            {
                Name = dto.Name,
                ShortName = dto.ShortName
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

        public async Task<int> DeleteClubAsync(int clubId)
        {
            var entity = await FindByCondition(o => o.Id == clubId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return 0;

            Delete(entity);

            await SaveChangesAsync();

            return entity.Id;
        }

        public async Task<ClubDto> GetClubByIdAsync(int clubId)
        {
            var entity = await FindByCondition(o => o.Id == clubId)
                .SingleOrDefaultAsync();

            if (entity == null)
                return null;

            return new ClubDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ShortName = entity.ShortName
            };
        }

        public async Task<List<ClubDto>> GetClubListAsync()
        {
            var entities = await FindAll()
                .ToListAsync();

            List<ClubDto> clubList = new List<ClubDto>();

            entities.ForEach(i => clubList.Add(new ClubDto
            {
                Id = i.Id,
                Name = i.Name,
                ShortName = i.ShortName
            }));

            return clubList;
        }

        public async Task<int> UpdateClubAsync(ClubDto dto)
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
