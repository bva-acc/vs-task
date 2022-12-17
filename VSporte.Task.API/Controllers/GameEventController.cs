using Microsoft.AspNetCore.Mvc;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;

namespace VSporte.Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameEventController : ControllerBase
    {
        private readonly IGameEventService _gameEventService;

        public GameEventController(IGameEventService gameEventService)
        {
            _gameEventService = gameEventService;
        }

        /// <summary>
        /// Get game event list
        /// </summary>
        [HttpGet]
        [Route("GetGameEventList")]
        public async Task<IActionResult> GetGameEventList()
        {
            var result = await _gameEventService.GetGameEventListAsync();

            if (result.Count == 0)
                return BadRequest("Game events not found");

            return Ok(result);
        }

        /// <summary>
        /// Get game event by id
        /// </summary>
        [HttpGet]
        [Route("GetGameEventById")]
        public async Task<IActionResult> GetGameEventById(int gameEventId)
        {
            var result = await _gameEventService.GetGameEventByIdAsync(gameEventId);

            if (result == null)
                return BadRequest("Game event not found");

            return Ok(result);
        }

        /// <summary>
        /// Create new game event
        /// </summary>
        [HttpPost]
        [Route("CreateGameEvent")]
        public async Task<IActionResult> CreateGameEvent(GameEventDto dto)
        {
            var gameEventId = await _gameEventService.CreateGameEventAsync(dto);

            return Created(string.Empty, $"Game event {gameEventId} created");
        }

        /// <summary>
        /// Update game event
        /// </summary>
        [HttpPatch]
        [Route("UpdateGameEventById")]
        public async Task<IActionResult> UpdateGameEventById(GameEventDto dto)
        {
            var gameEventId = await _gameEventService.UpdateGameEventAsync(dto);

            if (gameEventId == 0)
                return BadRequest("Game event is not exist");

            return Ok($"Game event {gameEventId} updated");
        }

        /// <summary>
        /// Delete game event
        /// </summary>
        [HttpDelete]
        [Route("DeleteGameEventById")]
        public async Task<IActionResult> DeleteGameEventById(int gameEventId)
        {
            var result = await _gameEventService.DeleteGameEventAsync(gameEventId);

            if (result == 0)
                return BadRequest("Game event is not exist");

            return Ok(result);
        }
    }
}
