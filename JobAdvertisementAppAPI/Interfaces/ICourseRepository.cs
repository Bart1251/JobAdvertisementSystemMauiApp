using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetUserCourses(int userId);
        IEnumerable<Course> GetCourses();
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(Course course);
        bool Save();
    }
}
