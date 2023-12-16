using System.Text.Json.Serialization;

namespace JobAdvertisementAppAPI.Models
{
    public class WorkingShift
    {
        public int Id { get; set; }
        public string? Shift { get; set; }

        [JsonIgnore]
        public ICollection<Offer>? Offer { get; set; }
    }
}
