using System.Threading.Tasks;
using _netCourse.Models;

namespace _netCourse.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UsernameExists(string username);
    }
}