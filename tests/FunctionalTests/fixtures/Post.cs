namespace FunctionalTests.fixtures
{

    public class Post
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Body { get; set; }

        public virtual int BlogId { get; set; }
    }
}
