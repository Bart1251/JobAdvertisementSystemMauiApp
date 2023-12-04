namespace JobAdvertisementApp.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public LanguageLevel Level { get; set; }
    }

    public enum LanguageLevel
    {
        A1,
        A2,
        B1,
        B2,
        C1,
        C2
    }
}
