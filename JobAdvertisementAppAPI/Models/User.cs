namespace JobAdvertisementAppAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public string? CareerSummary { get; set; }
        public string? Skills { get; set; }


        public ICollection<JobExperience>? JobExperiences { get; set; }
        public ICollection<Education>? Education { get; set; }
        public ICollection<Profile>? Profiles { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
    }
}
