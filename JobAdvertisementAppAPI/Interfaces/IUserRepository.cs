using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User? GetUser(string email);
        User? GetUser(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool Save();
    }
}
