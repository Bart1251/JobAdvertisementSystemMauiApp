namespace JobAdvertisementAppAPI.Models
{
    public class TypeOfContract
    {
        public int Id { get; set; }
        public string? Type { get; set; }


        public ICollection<Offer>? Offers { get; set; }
    }
}
