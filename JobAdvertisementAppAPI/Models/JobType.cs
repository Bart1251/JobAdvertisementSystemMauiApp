using System.Text.Json.Serialization;

namespace JobAdvertisementAppAPI.Models
{
    public class JobType
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        [JsonIgnore]
        public ICollection<Offer>? Offers { get; set; }
    }
}
