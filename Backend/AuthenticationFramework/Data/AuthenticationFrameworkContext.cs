using AuthenticationFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationFramework.Data
{
    public class AuthenticationFrameworkContext : DbContext
    {
        public AuthenticationFrameworkContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
