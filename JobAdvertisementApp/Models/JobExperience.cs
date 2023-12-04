namespace JobAdvertisementApp.Models
{
    public class JobExperience
    {
        public int Id { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public DateTime PeriodOfEmploymentStart { get; set; }
        public DateTime PeriodOdEmploymentEnd { get; set; }
        public string? Responsibilities { get; set; }
    }
}
