using Microsoft.EntityFrameworkCore;
using ChurrascoChallenge.Models;
using System.Runtime.CompilerServices;

namespace ChurrascoChallenge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(tb => {
                tb.HasKey(col => col.id);
                tb.Property(col => col.id).ValueGeneratedOnAdd();

                tb.Property(col => col.role).HasColumnType("VARCHAR").HasMaxLength(25);

                tb.Property(col => col.active).HasDefaultValue(1);

            });
            modelBuilder.Entity<User>().ToTable("user");

            modelBuilder.Entity<Product>(tb => {
                tb.HasKey(col => col.id);
                tb.Property(col => col.id).ValueGeneratedOnAdd();

                tb.HasIndex(col => col.SKU).IsUnique();
                tb.HasIndex(col => col.code).IsUnique();

                tb.Property(col => col.name).HasColumnType("VARCHAR").HasMaxLength(40);

                tb.Property(col => col.price).HasPrecision(10, 2);

                tb.Property(col => col.currency).HasColumnType("VARCHAR").HasMaxLength(3);

            });
        }
    }
}