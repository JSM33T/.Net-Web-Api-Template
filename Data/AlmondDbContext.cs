using almondCoveApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace almondCoveApi.Data
{
    public class AlmondDbContext : DbContext
    {
        public AlmondDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Mail>  MailingList { get; set; }
        public DbSet<User> UserProfiles { get; set; }
    }
}
