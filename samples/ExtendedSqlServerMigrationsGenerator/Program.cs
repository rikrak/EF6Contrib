namespace ExtendedSqlServerMigrationsGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Pluralization;
    using System.Data.Entity.SqlServer;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CRMContext
        :DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }

    public class Configuration
        :DbConfiguration
    {
        public Configuration()
        {

            SetMigrationSqlGenerator("System.Data.SqlClient", () =>
            {
                return new ExtendedSqlServerMigrationSqlGenerator();
            });

            SetDefaultConnectionFactory(new SqlConnectionFactory(@"Server=.\SQLEXPRESS;Integrated Security=true"));
        }
    }

   
}
