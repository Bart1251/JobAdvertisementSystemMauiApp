namespace JobAdvertisementApp.Models
{
    public class WorkingShift
    {
        public int Id { get; set; }
        public string? Shift { get; set; }

        public override string ToString()
        {
            return Shift;
        }
    }
}
