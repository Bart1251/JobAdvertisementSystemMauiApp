using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public User? GetUser(string email)
        {
            return context.User.Where(e => e.Email == email).FirstOrDefault();
        }

        public bool CreateUser(User user)
        {
            context.Add(user);

            return Save();
        }

        public bool Save()
        {
            return context.SaveChanges() > 0;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.User.ToList();
        }
    }
}
