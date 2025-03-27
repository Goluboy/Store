using Microsoft.EntityFrameworkCore;
using Store.Entities;

namespace MarketplaceTest.Data
{
    public class StoreContext : DbContext
    {
        public DbSet<GoodCategory> GoodCategories { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodCategory>()
                .HasOne(gc => gc.ParentCategory)
                .WithMany(gc => gc.ChildCategories)
                .HasForeignKey(gc => gc.ParentID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
