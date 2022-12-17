using VSporte.Task.API.DTOs;

namespace VSporte.Task.API.Interfaces
{
    public interface IPlayerService
    {
        Task<List<PlayerDto>> GetPlayerListAsync();

        Task<PlayerDto> GetPlayerByIdAsync(int playerId);

        Task<int> CreatePlayerAsync(PlayerDto dto);

        Task<int> UpdatePlayerAsync(PlayerDto dto);

        Task<int> DeletePlayerAsync(int playerId);
    }
}
