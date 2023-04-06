
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorsBooks>().HasKey(al => new { al.AuthorId, al.BookId });
        }

        public DbSet<Author> Authors {get;set;}
        public DbSet<Book> Books { get;set;}
        public DbSet<Commentary> Commentaries { get; set; }
        public DbSet<AuthorsBooks> AuthorsBooks { get; set;}
    }
}