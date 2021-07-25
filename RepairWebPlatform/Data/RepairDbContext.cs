namespace RepairWebPlatform.Data
{
    using RepairWebPlatform.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class RepairDbContext : IdentityDbContext
    {
        public RepairDbContext(DbContextOptions<RepairDbContext> options)
            : base(options)
        {
        }

        public  DbSet<Item> ItemDbSet { get; init; }
        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Item>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder
                .Entity<Item>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Categories)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
