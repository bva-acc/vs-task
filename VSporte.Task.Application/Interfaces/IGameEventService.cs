using VSporte.Task.API.DTOs;

namespace VSporte.Task.API.Interfaces
{
    public interface IGameEventService
    {
        Task<List<GameEventDto>> GetGameEventListAsync();

        Task<GameEventDto> GetGameEventByIdAsync(int gameEventId);

        Task<int> CreateGameEventAsync(GameEventDto dto);

        Task<int> UpdateGameEventAsync(GameEventDto dto);

        Task<int> DeleteGameEventAsync(int gameEventId);
    }
}
