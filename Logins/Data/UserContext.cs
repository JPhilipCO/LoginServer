using Microsoft.EntityFrameworkCore;
using Logins.Models;

namespace Logins.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
