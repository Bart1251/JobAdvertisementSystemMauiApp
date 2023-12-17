namespace JobAdvertisementApp.Models
{
    public class JobLevel
    {
        public int Id { get; set; }
        public string? Level { get; set; }
        public override string ToString()
        {
            return Level;
        }
    }
}
