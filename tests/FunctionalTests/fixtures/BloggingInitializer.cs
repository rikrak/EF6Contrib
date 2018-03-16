
namespace FunctionalTests.fixtures
{
    using FunctionalTests.fixtures.Migrations;
    using System.Data.Entity;

    class BloggingInitializer
        : DropAndMigrateDatabaseToLatestVersion<BloggingContext, Configuration>
    {
        public BloggingInitializer()
        {
        }
        public override void Seed(BloggingContext context)
        {
            var blog = new Blog
            {
                Title = "Code Complete!"
            };

            blog.Posts.Add(new Post
            {
                Body = "The body for posts1",
                Title = "post1"
            });

            context.Blogs.Add(blog);
            context.SaveChanges();

        }
    }
}
