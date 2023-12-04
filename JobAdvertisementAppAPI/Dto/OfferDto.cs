namespace JobAdvertisementAppAPI.Dto
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string? Position { get; set; }
        public decimal MinimumWege { get; set; }
        public decimal MaximumWege { get; set; }
        public string? WorkingDays { get; set; }
        public string? WorkingHours { get; set; }
        public DateTime Expires { get; set; }
        public string? Description { get; set; }
        public int AvailablePosts { get; set; }
        public string? Benefits { get; set; }
        public string? Requirements { get; set; }
        public string? Responsibilities { get; set; }
    }
}
