using TerfMate.Server.Models;
using TurfMate.Server.Models.DTOs;
using TurfMate.Server.Repositories;
using TurfMate.Server.Services;

namespace TerfMate.Server.Services
{
    public class TurfService : ITurfService
    {
        private readonly ITurfRepository _turfRepository;

        public TurfService(ITurfRepository turfRepository)
        {
            _turfRepository = turfRepository;
        }

        public async Task<List<TurfDto>> GetAllTurfsAsync()
        {
            var turfs = await _turfRepository.GetAllAsync();
            return turfs.Select(MapToDto).ToList();
        }

        public async Task<TurfDto?> GetTurfByIdAsync(int id)
        {
            var turf = await _turfRepository.GetByIdAsync(id);
            return turf == null ? null : MapToDto(turf);
        }

        public async Task<TurfDto> CreateTurfAsync(CreateTurfRequest createTurfDto)
        {
            var turf = new Turf
            {
                Name = createTurfDto.Name,
                Location = createTurfDto.Location,
                Description = createTurfDto.Description,
                PricePerHour = createTurfDto.PricePerHour,
                ImageUrl = createTurfDto.ImageUrl,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdTurf = await _turfRepository.CreateAsync(turf);
            return MapToDto(createdTurf);
        }

        public async Task<TurfDto?> UpdateTurfAsync(int id, UpdateTurfRequest updateTurfDto)
        {
            var existingTurf = await _turfRepository.GetByIdAsync(id);
            if (existingTurf == null) return null;

            var updatedTurf = new Turf
            {
                Name = updateTurfDto.Name ?? existingTurf.Name,
                Location = updateTurfDto.Location ?? existingTurf.Location,
                Description = updateTurfDto.Description ?? existingTurf.Description,
                PricePerHour = updateTurfDto.PricePerHour ?? existingTurf.PricePerHour,
                ImageUrl = updateTurfDto.ImageUrl ?? existingTurf.ImageUrl,
                IsAvailable = updateTurfDto.IsAvailable ?? existingTurf.IsAvailable
            };

            var result = await _turfRepository.UpdateAsync(id, updatedTurf);
            return result == null ? null : MapToDto(result);
        }

        public async Task<bool> DeleteTurfAsync(int id)
        {
            return await _turfRepository.DeleteAsync(id);
        }

        public async Task<List<TurfDto>> GetAvailableTurfsAsync()
        {
            var turfs = await _turfRepository.GetAvailableTurfsAsync();
            return turfs.Select(MapToDto).ToList();
        }

        private static TurfDto MapToDto(Turf turf)
        {
            return new TurfDto
            {
                Id = turf.Id,
                Name = turf.Name,
                Location = turf.Location,
                Description = turf.Description,
                PricePerHour = turf.PricePerHour,
                ImageUrl = turf.ImageUrl,
                IsAvailable = turf.IsAvailable
            };
        }
    }
}