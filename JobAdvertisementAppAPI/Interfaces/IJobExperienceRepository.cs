using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IJobExperienceRepository
    {
        IEnumerable<JobExperience> GetUserJobExperiences(int userId);
        IEnumerable<JobExperience> GetJobExperiences();
        bool CreateJobExperience(JobExperience jobExperience);
        bool UpdateJobExperience(JobExperience jobExperience);
        bool DeleteJobExperience(JobExperience jobExperience);
        bool Save();
    }
}
