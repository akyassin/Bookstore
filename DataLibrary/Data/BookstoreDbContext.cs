using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region AuthorSeed
            modelBuilder.Entity<Author>().HasData(
                new Author() { AuthorId = 1, FullName = "Ahmad Yassin" },
                new Author() { AuthorId = 2, FullName = "Mohammad Abdulaal" },
                new Author() { AuthorId = 3, FullName = "Yassin Darwish" },
                new Author() { AuthorId = 4, FullName = "Yousef Sabri" },
                new Author() { AuthorId = 5, FullName = "Mikael Bros" });
            #endregion

            modelBuilder.Entity<Book>(
                entity =>
                {
                    entity.HasOne(d => d.Author)
                        .WithMany(p => p.Books)
                        .HasForeignKey("AuthorId");
                });

            #region BookSeed
            modelBuilder.Entity<Book>().HasData(
                new { BookId = 1, Title = "C# For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", AuthorId = 1 },
                new { BookId = 2, Title = "Java For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", AuthorId = 2 },
                new { BookId = 3, Title = "Python For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", AuthorId = 3 },
                new { BookId = 4, Title = "PHP For Programming", Description = "No Desc", ImageName = "", ImageUrl = "", AuthorId = 4 },
                new { BookId = 5, Title = "Clean Code", Description = "No Desc", ImageName = "", ImageUrl = "", AuthorId = 5 });
            #endregion
        }
    }
}
