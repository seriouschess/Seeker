using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options) : base (options) { }
        public DbSet<Subreddit> Subreddits {get;set;}
    }
}