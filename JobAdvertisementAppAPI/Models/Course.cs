namespace JobAdvertisementAppAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Organizer { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }


        public User? User { get; set; }
    }
}
