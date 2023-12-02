namespace JobAdvertisementApp.Models
{
    public class JobType
    {
        public int Id { get; set; }
        public string? Type { get; set; }


        public ICollection<Offer>? Offers { get; set; }
    }
}
