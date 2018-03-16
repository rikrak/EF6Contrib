namespace ExtendedSqlServerMigrationsGenerator.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration 
        : DbMigrationsConfiguration<ExtendedSqlServerMigrationsGenerator.CRMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
            //SetSqlGenerator("System.Data.SqlClient", new ExtendedSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(ExtendedSqlServerMigrationsGenerator.CRMContext context)
        {
            
        }
    }
}
