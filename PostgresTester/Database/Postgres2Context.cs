using Microsoft.EntityFrameworkCore;

namespace PostgresTester.Database
{
    public class Postgres2Context : DbContext
    {
        public Postgres2Context(DbContextOptions<Postgres2Context> contextOptions) : base(contextOptions)
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
