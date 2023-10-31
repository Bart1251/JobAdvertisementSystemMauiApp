namespace JobAdvertisementAppAPI.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string? Link { get; set; }


        public User? User { get; set; }
    }
}
