using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User? GetUser(string email);
        bool UserExists(string email);

        bool CreateUser(User user);
        bool Save();
    }
}
