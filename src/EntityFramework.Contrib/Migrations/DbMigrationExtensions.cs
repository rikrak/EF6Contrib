namespace System.Data.Entity.Migrations
{
    using EntityFramework.Contrib.Migrations.Model;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.Entity.Migrations.Model;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// DbMigration extensions methods
    /// </summary>
    public static class DbMigrationExtensions
    {
        /// <summary>
        /// Grant table permission to specified user
        /// </summary>
        /// <param name="migration">The DbMigration</param>
        /// <param name="table">The table you specify the permission</param>
        /// <param name="user">The user you specify the permission</param>
        /// <param name="permission">The permission to specify</param>
        public static void GrantTablePermission(this DbMigration migration, string table, string user, TablePermission permission)
        {
            ((IDbMigration)migration)
              .AddOperation(new GrantTablePermissionOperation(table, user, permission));
        }

        /// <summary>
        /// Remove table permission to specified user
        /// </summary>
        /// <param name="migration">The DbMigration</param>
        /// <param name="table">The table you remove the permission</param>
        /// <param name="user">The user you remove the permission</param>
        /// <param name="permission">The permission to revoke</param>
        public static void RevokeTablePermission(this DbMigration migration, string table, string user, TablePermission permission)
        {
            ((IDbMigration)migration)
              .AddOperation(new RevokeTablePermissionOperation(table, user, permission));
        }

        /// <summary>
        /// Change the collation to specified database
        /// </summary>
        /// <param name="migration">The DbMigration</param>
        /// <param name="databaseName">The name of database you change the collation.</param>
        /// <param name="collation">The collation to set.</param>
        public static void SetDatabaseCollation(this DbMigration migration, string databaseName, string collation)
        {
            ((IDbMigration)migration)
              .AddOperation(new DatabaseCollationOperation(databaseName, collation));
        }


        /// <summary>
        /// Create a new view
        /// </summary>
        /// <param name="migration">The DBMigration</param>
        /// <param name="viewName">The name of view to create</param>
        /// <param name="bodySql">The sql body of view to create</param>
        public static void CreateView(this DbMigration migration, string viewName, string bodySql)
        {
            ((IDbMigration)migration)
              .AddOperation(new CreateViewOperation(viewName, bodySql));
        }

        /// <summary>
        /// Drop existing view
        /// </summary>
        /// <param name="migration">the DBMigration</param>
        /// <param name="viewName">the name of view to drop</param>
        public static void DropView(this DbMigration migration, string viewName)
        {
            ((IDbMigration)migration)
             .AddOperation(new DropViewOperation(viewName));
        }

        /// <summary>
        /// Execute sql scripts from existing sql file.
        /// </summary>
        /// <param name="migration">the DBMigration.</param>
        /// <param name="sqlFilePath">The sql file path with scripts to be executed.</param>
        public static void SqlFile(this DbMigration migration, string sqlFilePath)
        {
            ((IDbMigration)migration)
                .AddOperation(new SqlFileOperation(sqlFilePath));
        }

        /// <summary>
        /// Execute sql scripts from existing stream.
        /// </summary>
        /// <param name="migration">The DBMigration</param>
        /// <param name="sqlFileStream">The stream with sql scripts to be executed.</param>
        public static void SqlFile(this DbMigration migration, Stream sqlFileStream)
        {
            ((IDbMigration)migration)
                .AddOperation(new SqlFileOperation(sqlFileStream));
        }

        /// <summary>
        /// Execute sql scripts from existing resource.
        /// </summary>
        /// <param name="migration">The DbMigration</param>
        /// <param name="assembly">The assembly that contain the resource.</param>
        /// <param name="resourceName">The resource name.</param>

        public static void SqlResource(this DbMigration migration, Assembly assembly, string resourceName)
        {
            ((IDbMigration)migration)
                .AddOperation(new SqlResourceOperation(assembly, resourceName));
        }

        /// <summary>
        /// Add a new computed column
        /// </summary>
        /// <param name="migration">The DBMigration</param>
        /// <param name="columnName">The name of computed column to create</param>
        /// <param name="bodySql">The sql body of view to create</param>
        /// <param name="tableName">The name of table for create computed column</param>
        public static void AddComputedColumn(this DbMigration migration, string tableName, string columnName, string bodySql)
        {
            ((IDbMigration)migration)
                .AddOperation(new AddComputedColumnOperation(tableName, columnName, bodySql));
        }

        /// <summary>
        /// Create a new computed column
        /// </summary>
        /// <param name="migration">The DBMigration</param>                
        /// <param name="tableName">The name of table for create computed column</param>
        /// <param name="columnName">The name of computed column to drop</param>
        public static void DropComputedColumn(this DbMigration migration, string tableName, string columnName)
        {
            ((IDbMigration)migration)
                .AddOperation(new DropComputedColumnOperation(tableName, columnName));
        }
    }
}
