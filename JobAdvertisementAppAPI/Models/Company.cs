using System.Text.Json.Serialization;

namespace JobAdvertisementAppAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public ICollection<Offer>? Offers { get; set; }
    }
}
