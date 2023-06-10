using Microsoft.EntityFrameworkCore;

namespace PostgresTester.Database
{
    public class ReadonlyContext : DbContext
    {
        public ReadonlyContext(DbContextOptions<ReadonlyContext> contextOptions) : base(contextOptions)
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
