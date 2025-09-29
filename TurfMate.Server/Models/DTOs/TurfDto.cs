namespace TurfMate.Server.Models.DTOs
{
    public class TurfDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }

    public class CreateTurfRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class UpdateTurfRequest
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public decimal? PricePerHour { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsAvailable { get; set; }
    }
}