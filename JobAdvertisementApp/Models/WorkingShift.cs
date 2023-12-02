namespace JobAdvertisementApp.Models
{
    public class WorkingShift
    {
        public int Id { get; set; }
        public string? Shift { get; set; }


        public ICollection<Offer>? Offer { get; set; }
    }
}
