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
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(tb => {
                tb.HasKey(col => col.IdUser);
                tb.Property(col => col.IdUser).ValueGeneratedOnAdd();

                tb.Property(col => col.Role).HasColumnType("VARCHAR").HasMaxLength(25);

                tb.Property(col => col.Active).HasDefaultValue(1);

            });
            modelBuilder.Entity<User>().ToTable("user");

            modelBuilder.Entity<Product>(tb => {
                tb.HasKey(col => col.IdProduct);
                tb.Property(col => col.IdProduct).ValueGeneratedOnAdd();

                tb.HasIndex(col => col.Sku).IsUnique();
                tb.HasIndex(col => col.Code).IsUnique();

                tb.Property(col => col.Name).HasColumnType("VARCHAR").HasMaxLength(40);

                tb.Property(col => col.Price).HasPrecision(10, 2);

                tb.Property(col => col.Currency).HasColumnType("VARCHAR").HasMaxLength(3);

            });
        }
    }
}