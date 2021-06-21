using KaspiTest.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace KaspiTest.DataAccess
{
    class PublicationContext : DbContext
    {
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=publicationsDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
