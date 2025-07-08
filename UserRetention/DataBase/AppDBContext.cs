using Microsoft.EntityFrameworkCore;
using UserRetention.DataBase.DTO;

namespace UserRetention.DataBase
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
                
        }

        public DbSet<User> TUsers { get; set; }
    }
}
