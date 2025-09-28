using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurfMate.Server.Models.DTOs;
using TurfMate.Server.Services;

namespace TerfMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurfController : ControllerBase
    {
        private readonly ITurfService _turfService;
        private readonly ILogger<TurfController> _logger;

        public TurfController(ITurfService turfService, ILogger<TurfController> logger)
        {
            _turfService = turfService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TurfDto>>> GetAllTurfs()
        {
            try
            {
                var turfs = await _turfService.GetAllTurfsAsync();
                return Ok(turfs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all turfs");
                return StatusCode(500, new { message = "An error occurred while retrieving turfs" });
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<List<TurfDto>>> GetAvailableTurfs()
        {
            try
            {
                var turfs = await _turfService.GetAvailableTurfsAsync();
                return Ok(turfs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available turfs");
                return StatusCode(500, new { message = "An error occurred while retrieving available turfs" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurfDto>> GetTurfById(int id)
        {
            try
            {
                var turf = await _turfService.GetTurfByIdAsync(id);
                if (turf == null)
                {
                    return NotFound(new { message = "Turf not found" });
                }
                return Ok(turf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting turf with ID: {TurfId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the turf" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<TurfDto>> CreateTurf(CreateTurfRequest createTurfDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var turf = await _turfService.CreateTurfAsync(createTurfDto);
                return CreatedAtAction(nameof(GetTurfById), new { id = turf.Id }, turf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating turf");
                return StatusCode(500, new { message = "An error occurred while creating the turf" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<TurfDto>> UpdateTurf(int id, UpdateTurfRequest updateTurfDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var turf = await _turfService.UpdateTurfAsync(id, updateTurfDto);
                if (turf == null)
                {
                    return NotFound(new { message = "Turf not found" });
                }
                return Ok(turf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating turf with ID: {TurfId}", id);
                return StatusCode(500, new { message = "An error occurred while updating the turf" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteTurf(int id)
        {
            try
            {
                var result = await _turfService.DeleteTurfAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Turf not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting turf with ID: {TurfId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the turf" });
            }
        }
    }
}