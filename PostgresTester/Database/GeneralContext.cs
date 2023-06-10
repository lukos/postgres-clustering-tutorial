using Microsoft.EntityFrameworkCore;

namespace PostgresTester.Database
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> contextOptions) : base(contextOptions)
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
