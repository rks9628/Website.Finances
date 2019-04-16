using Microsoft.EntityFrameworkCore;
using Website.Finances.Domain.ValueTypes;

namespace Website.Finances.Infrastructure.Database
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=Website.Finances;Trusted_Connection=True;");
        }
    }
}
