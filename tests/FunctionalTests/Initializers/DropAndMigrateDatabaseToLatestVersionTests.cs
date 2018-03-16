namespace FunctionalTests.Initializers
{
    using FunctionalTests.fixtures;
    using System.Data.Entity;
    using System.Linq;
    using Xunit;

    public class DropAndMigrateDatabaseToLatestVersionTests
    {
        [Fact]
        public void Initializer_is_stablished_and_seed_is_executed()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(b => b.Posts)
                    .ToList();

                Assert.True(blogs.Count == 1);
                Assert.True(blogs[0].Posts.Count == 1);
            }
        }
    }


}
