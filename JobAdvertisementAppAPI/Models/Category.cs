using System.Text.Json.Serialization;

namespace JobAdvertisementAppAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<Offer>? Offers { get; set; }
    }
}
