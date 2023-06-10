using Microsoft.EntityFrameworkCore;

namespace PostgresTester.Database
{
    public class Postgres1Context : DbContext
    {
        public Postgres1Context(DbContextOptions<Postgres1Context> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        #region Datasets
        public DbSet<Book> Books { get; set; }
        #endregion
    }
}
