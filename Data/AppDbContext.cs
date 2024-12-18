using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; } // Add this line

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity tables are configured

            // Define the relationship between Book and Author
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books) 
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Book and LibraryBranch
            modelBuilder.Entity<Book>()
                .HasOne(b => b.LibraryBranch)
                .WithMany(lb => lb.Books)
                .HasForeignKey(b => b.LibraryBranchId)
                .OnDelete(DeleteBehavior.SetNull);

            // Define the relationship for BorrowedBook
            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.Book)
                .WithMany()
                .HasForeignKey(bb => bb.BookId);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.Customer)
                .WithMany()
                .HasForeignKey(bb => bb.CustomerId);
        }
    }
}