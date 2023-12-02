namespace JobAdvertisementApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Offer>? Offers { get; set; }
    }
}
