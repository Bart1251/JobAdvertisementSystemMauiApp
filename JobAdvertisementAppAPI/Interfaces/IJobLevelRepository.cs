using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IJobLevelRepository
    {
        IEnumerable<JobLevel> GetJobLevels();
        bool CreateJobLevel(JobLevel jobLevel);
        bool UpdateJobLevel(JobLevel jobLevel);
        bool DeleteJobLevel(JobLevel jobLevel);
        bool Save();
    }
}
