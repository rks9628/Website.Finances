using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Website.Finances.Domain.Interfaces;
using Website.Finances.Domain.ValueTypes;
using Website.Finances.Infrastructure.Database;

namespace Website.Finances.Infrastructure.Repositories
{
    public class DatabaseUserRepository : IUserRepository
    {
        public async Task<User> GetByNameAsync(string username)
        {
            using (var dbContext = new UserContext())
            {
                return await dbContext
                    .Users
                    .SingleOrDefaultAsync(u => u.Username.Equals(username));
            }
        }

        public async Task<User> InsertAsync(User user)
        {
            using (var dbContext = new UserContext())
            {
                if (await dbContext.Users.SingleOrDefaultAsync(u => u.Username.Equals(user.Username)) != null)
                    throw new ArgumentException(nameof(user.Username));

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                return user;
            }
        }
    }
}
