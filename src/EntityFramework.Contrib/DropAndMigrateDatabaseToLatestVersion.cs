namespace System.Data.Entity
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;

    /// <summary>
    /// Database initializer to drop and create a new database
    /// in latest migration state. This initializer also add a Seed method
    /// </summary>
    /// <typeparam name="TContext">The type of context.</typeparam>
    /// <typeparam name="TMigrationsConfiguration">The migration configuration to be used to set database in latest version.</typeparam>
    public class DropAndMigrateDatabaseToLatestVersion<TContext, TMigrationsConfiguration>
        : IDatabaseInitializer<TContext>
        where TContext : DbContext, new()
        where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        TMigrationsConfiguration _config;

        /// <summary>
        /// Create a new instance
        /// </summary>
        public DropAndMigrateDatabaseToLatestVersion()
        {
            _config = new TMigrationsConfiguration();
        }

        /// <inheritdoc/>
        public void InitializeDatabase(TContext context)
        {
            context.Database.Delete();

            var migrator = new DbMigrator(_config);
            migrator.Update();

            
            Seed(context);

        }

        /// <inheritdoc/>
        public virtual void Seed(TContext context) { }
    }
}
