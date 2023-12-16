using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IJobLevelRepository
    {
        IEnumerable<JobLevel> GetJobLevels();
        public JobLevel? GetJobLevel(int id);
        bool CreateJobLevel(JobLevel jobLevel);
        bool UpdateJobLevel(JobLevel jobLevel);
        bool DeleteJobLevel(JobLevel jobLevel);
        bool Save();
    }
}
