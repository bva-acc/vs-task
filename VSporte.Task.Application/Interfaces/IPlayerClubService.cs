using VSporte.Task.API.DTOs;

namespace VSporte.Task.API.Interfaces
{
    public interface IPlayerClubService
    {
        Task<List<PlayerClubDto>> GetPlayerClubListAsync();

        Task<PlayerClubDto> GetPlayerClubByIdAsync(int playerClubId);

        Task<int> CreatePlayerClubAsync(PlayerClubDto dto);

        Task<int> UpdatePlayerClubAsync(PlayerClubDto dto);

        Task<int> DeletePlayerClubAsync(int playerClubId);
    }
}
