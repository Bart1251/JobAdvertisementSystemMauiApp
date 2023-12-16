using System.Text.Json.Serialization;

namespace JobAdvertisementAppAPI.Models
{
    public class JobLevel
    {
        public int Id { get; set; }
        public string? Level { get; set; }

        [JsonIgnore]
        public ICollection<Offer>? Offers { get; set; }
    }
}
