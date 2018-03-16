namespace Miscelaneous
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;

    class ConfigurationRegistrarExtensionsLoadNamedThisAssemblyScenario
    {
        public void Start()
        {
            using (var context = new Context())
            {
                context.Blogs.Add(new Blog()
                {
                    TheKey = 1,
                    Title = "O bruxo mobile"
                });

                context.SaveChanges();
            }
        }

        public class Blog
        {
            public int TheKey { get; set; }
            public string Title { get; set; }
          
        }

        public class Context
            : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Configurations.AddFromNamedAssembly("Miscelaneous");
            }
        }

        class BlogConfiguration
            : EntityTypeConfiguration<Blog>
        {
            private BlogConfiguration()
            {
                HasKey(c => c.TheKey);
            }
        }
    }
}
