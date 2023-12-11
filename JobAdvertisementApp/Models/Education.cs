namespace JobAdvertisementApp.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string? Institution { get; set; }
        public string? Town { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public string? FieldOfStudy { get; set; }
        public DateTime PeriodOfEducationStart { get; set; }
        public DateTime PeriodOfEducationEnd { get; set; }
    }

    public enum EducationLevel
    {
        Podstawowe,
        Zawodowe,
        Średnie,
        Licencjat
    }
}
