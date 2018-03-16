using System.Collections.Generic;
namespace FunctionalTests.fixtures
{

    public class Blog
    {
        public  Blog()
        {
           
        }
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }

        HashSet<Post> _posts;

        public virtual ICollection<Post> Posts
        {
            get
            {
                if (_posts == null)
                    _posts = new HashSet<Post>();

                return _posts;
            }
            set
            {
                _posts = new HashSet<Post>(value);
            }
        }
    }


}
