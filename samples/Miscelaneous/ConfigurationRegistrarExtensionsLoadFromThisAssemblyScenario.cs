

namespace Miscelaneous
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;

    class ConfigurationRegistrarExtensionsLoadFromThisAssemblyScenario
    {
        public void Start()
        {
            using (var context = new Context())
            {
                context.Customers.Add(new Customer()
                {
                    TheKey = 1,
                    LastName = "Kerry",
                    FirstName ="Jhon"
                });

                context.SaveChanges();
            }
        }

        public class Customer
        {
            public int TheKey { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Context
            :DbContext
        {
            public DbSet<Customer> Customers { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Configurations.AddFromThisAssembly();
            }
        }

        class CustomerConfiguration
            :EntityTypeConfiguration<Customer>
        {
            private CustomerConfiguration()
            {
                HasKey(c => c.TheKey);
            }
        }
    }
}
