using VSporte.Task.API.DTOs;

namespace VSporte.Task.API.Interfaces
{
    public interface IClubService
    {
        Task<List<ClubDto>> GetClubListAsync();

        Task<ClubDto> GetClubByIdAsync(int clubId);

        Task<int> CreateClubAsync(ClubDto dto);

        Task<int> UpdateClubAsync(ClubDto dto);

        Task<int> DeleteClubAsync(int clubId);
    }
}
