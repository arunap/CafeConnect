namespace CafeConnect.Application.Features.Cafe.Dtos
{
    public class CafeDto
    {
        public Guid CafeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoPath { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

    }
}