using Microsoft.AspNetCore.Mvc;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;

namespace VSporte.Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerClubController : ControllerBase
    {
        private readonly IPlayerClubService _playerClubService;

        public PlayerClubController(IPlayerClubService playerClubService)
        {
            _playerClubService = playerClubService;
        }

        /// <summary>
        /// Get player-сlub list
        /// </summary>
        [HttpGet]
        [Route("GetPlayerClubList")]
        public async Task<IActionResult> GetPlayerClubList()
        {
            var result = await _playerClubService.GetPlayerClubListAsync();

            if (result.Count == 0)
                return BadRequest("Player-сlub relation not found");

            return Ok(result);
        }

        /// <summary>
        /// Get player-сlub relation by id
        /// </summary>
        [HttpGet]
        [Route("GetPlayerClubById")]
        public async Task<IActionResult> GetPlayerClubById(int playerClubId)
        {
            var result = await _playerClubService.GetPlayerClubByIdAsync(playerClubId);

            if (result == null)
                return BadRequest("Player-сlub relation not found");

            return Ok(result);
        }

        /// <summary>
        /// Create new player-сlub relation
        /// </summary>
        [HttpPost]
        [Route("CreatePlayerClub")]
        public async Task<IActionResult> CreatePlayerClub(PlayerClubDto dto)
        {
            // Проверка, что игрок не играет в другом клубе
            PlayerClubDto playerClub = await _playerClubService.GetPlayerClubByIdAsync(dto.PlayerId);
            if (playerClub?.ClubId != null)
                return BadRequest("Player is already playing for another club");

            var playerClubId = await _playerClubService.CreatePlayerClubAsync(dto);

            return Created(string.Empty, $"Player-сlub relation {playerClubId} created");
        }

        /// <summary>
        /// Update player-сlub relation
        /// </summary>
        [HttpPatch]
        [Route("UpdatePlayerClubById")]
        public async Task<IActionResult> UpdatePlayerClubById(PlayerClubDto dto)
        {
            var playerClubId = await _playerClubService.UpdatePlayerClubAsync(dto);

            if (playerClubId == 0)
                return BadRequest("Player-сlub relation is not exist");

            return Ok($"Player-сlub relation {playerClubId} updated");
        }

        /// <summary>
        /// Delete player-сlub relation
        /// </summary>
        [HttpDelete]
        [Route("DeletePlayerClubById")]
        public async Task<IActionResult> DeletePlayerClubById(int playerClubId)
        {
            var result = await _playerClubService.DeletePlayerClubAsync(playerClubId);

            if (result == 0)
                return BadRequest("Player-сlub relation is not exist");

            return Ok(result);
        }
    }
}
