using System.Threading.Tasks;
using Website.Finances.Domain.ValueTypes;

namespace Website.Finances.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByNameAsync(string username);
        Task<User> InsertAsync(User user);
    }
}
