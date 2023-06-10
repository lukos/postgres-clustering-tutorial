using Microsoft.EntityFrameworkCore;

namespace PostgresTester.Database
{
    public class Postgres3Context : DbContext
    {
        public Postgres3Context(DbContextOptions<Postgres3Context> contextOptions) : base(contextOptions)
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
