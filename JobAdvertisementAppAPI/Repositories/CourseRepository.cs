using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext dataContext;

        public CourseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateCourse(Course course)
        {
            dataContext.Course.Add(course);
            return Save();
        }

        public bool DeleteCourse(Course course)
        {
            dataContext.Course.Remove(course);
            return Save();
        }

        public IEnumerable<Course> GetCourses()
        {
            return dataContext.Course.ToList();
        }

        public IEnumerable<Course> GetUserCourses(int userId)
        {
            return dataContext.Course.Where(e => e.User.Id == userId).OrderByDescending(e => e.CourseEnd).ThenByDescending(e => e.CourseStart).ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateCourse(Course course)
        {
            dataContext.Course.Update(course);
            return Save();
        }
    }
}
