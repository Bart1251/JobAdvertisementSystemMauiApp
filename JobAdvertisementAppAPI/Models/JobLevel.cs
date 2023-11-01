namespace JobAdvertisementAppAPI.Models
{
    public class JobLevel
    {
        public int Id { get; set; }
        public string? Level { get; set; }


        public ICollection<Offer>? Offers { get; set; }
    }
}
