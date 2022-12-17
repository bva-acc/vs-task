using Microsoft.AspNetCore.Mvc;
using VSporte.Task.API.DTOs;
using VSporte.Task.API.Interfaces;

namespace VSporte.Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        /// <summary>
        /// Get club list
        /// </summary>
        [HttpGet]
        [Route("GetClubList")]
        public async Task<IActionResult> GetClubList()
        {
            var result = await _clubService.GetClubListAsync();

            if (result.Count == 0)
                return BadRequest("Clubs not found");

            return Ok(result);
        }

        /// <summary>
        /// Get club by id
        /// </summary>
        [HttpGet]
        [Route("GetClubById")]
        public async Task<IActionResult> GetClubById(int clubId)
        {
            var result = await _clubService.GetClubByIdAsync(clubId);

            if (result == null)
                return BadRequest("Club not found");

            return Ok(result);
        }

        /// <summary>
        /// Create new club
        /// </summary>
        [HttpPost]
        [Route("CreateClub")]
        public async Task<IActionResult> CreateClub(ClubDto dto)
        {
            var clubId = await _clubService.CreateClubAsync(dto);

            return Created(string.Empty, $"Club {clubId} created");
        }

        /// <summary>
        /// Update club
        /// </summary>
        [HttpPatch]
        [Route("UpdateClubById")]
        public async Task<IActionResult> UpdateClubById(ClubDto dto)
        {
            var clubId = await _clubService.UpdateClubAsync(dto);

            if (clubId == 0)
                return BadRequest("Club is not exist");

            return Ok($"Club {clubId} updated");
        }

        /// <summary>
        /// Delete club
        /// </summary>
        [HttpDelete]
        [Route("DeleteClubById")]
        public async Task<IActionResult> DeleteClubById(int clubId)
        {
            var result = await _clubService.DeleteClubAsync(clubId);

            if (result == 0)
                return BadRequest("Club is not exist");

            return Ok(result);
        }
    }
}
