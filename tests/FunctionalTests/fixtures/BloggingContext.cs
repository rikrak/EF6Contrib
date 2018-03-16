

namespace FunctionalTests.fixtures
{
    using System.Data.Entity;

    class BloggingContext
        :DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithRequired()
                .HasForeignKey(p => p.BlogId);
        }
    }
}
