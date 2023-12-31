using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Dto
{
    public class LanguageDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public LanguageLevel Level { get; set; }
    }
}
