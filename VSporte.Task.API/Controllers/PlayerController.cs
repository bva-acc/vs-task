using Microsoft.AspNetCore.Mvc;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;

namespace VSporte.Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Get player list
        /// </summary>
        [HttpGet]
        [Route("GetPlayerList")]
        public async Task<IActionResult> GetPlayerList()
        {
            var result = await _playerService.GetPlayerListAsync();

            if (result.Count == 0)
                return BadRequest("Players not found");

            return Ok(result);
        }

        /// <summary>
        /// Get player by id
        /// </summary>
        [HttpGet]
        [Route("GetPlayerById")]
        public async Task<IActionResult> GetPlayerById(int playerId)
        {
            var result = await _playerService.GetPlayerByIdAsync(playerId);

            if (result == null)
                return BadRequest("Player not found");

            return Ok(result);
        }

        /// <summary>
        /// Create new player
        /// </summary>
        [HttpPost]
        [Route("CreatePlayer")]
        public async Task<IActionResult> CreatePlayer(PlayerDto dto)
        {
            var playerId = await _playerService.CreatePlayerAsync(dto);

            return Created(string.Empty, $"Player {playerId} created");
        }

        /// <summary>
        /// Update player
        /// </summary>
        [HttpPatch]
        [Route("UpdatePlayerById")]
        public async Task<IActionResult> UpdatePlayerById(PlayerDto dto)
        {
            var playerId = await _playerService.UpdatePlayerAsync(dto);

            if (playerId == 0)
                return BadRequest("Player is not exist");

            return Ok($"Player {playerId} updated");
        }

        /// <summary>
        /// Delete player
        /// </summary>
        [HttpDelete]
        [Route("DeletePlayerById")]
        public async Task<IActionResult> DeletePlayerById(int playerId)
        {
            var result = await _playerService.DeletePlayerAsync(playerId);

            if (result == 0)
                return BadRequest("Player is not exist");

            return Ok(result);
        }
    }
}
