using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IEducationRepository
    {
        IEnumerable<Education> GetUserEducations(int userId);
        IEnumerable<Education> GetEducations();
        bool CreateEducation(Education education);
        bool UpdateEducation(Education education);
        bool DeleteEducation(Education education);
        bool Save();
    }
}
