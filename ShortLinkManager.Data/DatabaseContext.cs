using System.Data.Entity;

namespace ShortLinkManager.Data
{   
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name = ShortLinkTest")
        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());

        }
                
        public virtual DbSet<Models.ShortLink> ShortLinks { get; set; }
        public virtual DbSet<Models.Guest> Guests { get; set; }
        public virtual DbSet<Models.Visits> Visits { get; set; }

    }
}
