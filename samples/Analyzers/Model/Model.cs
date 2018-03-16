namespace Analyzers.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Author Author { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }

        public int Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class BlogContext
        :DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
