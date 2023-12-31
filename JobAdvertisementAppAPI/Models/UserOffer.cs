namespace JobAdvertisementAppAPI.Models
{
    public class UserOffer
    {
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public User? User { get; set; }
        public Offer? Offer { get; set; }
    }
}
