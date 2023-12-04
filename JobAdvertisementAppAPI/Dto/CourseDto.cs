namespace JobAdvertisementAppAPI.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Organizer { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
    }
}
