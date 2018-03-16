namespace NLog20CommandInterceptor
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {

            DbInterception.Add(new LogCommandInterceptor());

            using (var context = new SomeContext())
            {
                if (!context.Entities.Any())
                {
                    context.Entities.Add(new SomeEntity()
                    {
                        SomeProperty = "property value"
                    });

                    context.SaveChanges();
                }

                var entity = context.Entities.FirstOrDefault();
            }

            Console.ReadLine();
        }
    }

    public class SomeEntity
    {
        public int Id { get; set; }
        public string SomeProperty { get; set; }
    }

    public class SomeContext
        : DbContext
    {
        public DbSet<SomeEntity> Entities { get; set; }
    }

    public class Configuration
        : DbConfiguration
    {
        public Configuration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory(@"Server=.\SQLEXPRESS;Integrated Security=true"));
        }
    }
}
