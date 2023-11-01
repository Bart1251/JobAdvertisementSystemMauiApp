namespace JobAdvertisementAppAPI.Models
{
    public class Offer
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


        public Company? Company { get; set; }
        public JobLevel? JobLevel { get; set; }
        public TypeOfContract? TypeOfContract { get; set; }
        public JobType? JobType { get; set; }
        public WorkingShift? WorkingShift { get; set; }
        public Category? Category { get; set; }
    }
}
