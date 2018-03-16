namespace Analyzers
{
    using Analyzers.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.DependencyResolution;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;


    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BlogContext())
            {
                var blogs = context.Blogs
                    .Where(b => b.Title == "The title!!")
                    .ToList();
            }
            Console.Read();
        }
    }

    public class Configuation
        : DbConfiguration
    {
        public Configuation()
        {
            this.AddDependencyResolver(new CustomResolver());

            this.AddInterceptor(new PerformanceInterceptor((msg=>
            {
                Console.WriteLine(msg.Message);
            })));
        }

        private class CustomResolver
            : IDbDependencyResolver
        {
            public object GetService(Type type, object key)
            {
                return null;
            }

            public IEnumerable<object> GetServices(Type type, object key)
            {
                //you can use here your preferred IoC container
                if (typeof(IPerformanceAnalyzer).IsAssignableFrom(type))
                {
                    return new List<IPerformanceAnalyzer>()
                    {
                        new ExecutionTimePerformanceAnalyzer(TimeSpan.FromSeconds(3)),
                        new UnparametrizedWhereClausesPerformanceAnalyzer(),
                        new TopSlowQueriesPerformanceAnalyzer(10)
                    };
                }


                return Enumerable.Empty<object>();
            }
        }
    }
}
