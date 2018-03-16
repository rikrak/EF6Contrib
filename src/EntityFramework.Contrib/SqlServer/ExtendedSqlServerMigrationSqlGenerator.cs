using System.Linq;
using EntityFramework.Contrib.Migrations.Model;

namespace System.Data.Entity.SqlServer
{

    using EntityFramework.Contrib.Properties.Resources;
    using System.Data.Entity.Migrations.Model;
    using System.IO;

    /// <summary>
    /// Extented Sql Server Migrations Sql Generator with more
    /// operations.
    /// <remarks>
    /// For set  this migrations sql generator you need register it in
    /// your DbMigrationsConfiguration{TContext} using the method 
    /// SetSqlGenerator
    /// <example>
    /// SetSqlGenerator("System.Data.SqlClient", new AdvancedSqlServerMigrationSqlGenerator());
    /// </example>
    /// </remarks>
    /// </summary>
    public class ExtendedSqlServerMigrationSqlGenerator
        : SqlServerMigrationSqlGenerator
    {
        /// <inheritdoc/>
        protected override void Generate(MigrationOperation migrationOperation)
        {
            this.Generate((dynamic)migrationOperation);
        }

        /// <summary>
        /// Generate a SQL to grant permission on a table for specified user
        /// </summary>
        /// <param name="grantTablePermissionOperation">The operation to produce sql for.</param>
        public virtual void Generate(GrantTablePermissionOperation grantTablePermissionOperation)
        {
            using (var writer = Writer())
            {
                writer.WriteLine(
            "GRANT {0} ON {1} TO {2}",
            grantTablePermissionOperation.Permission.ToString().ToUpper(),
            grantTablePermissionOperation.Table,
            grantTablePermissionOperation.User);

                this.Statement(writer);
            }
        }

        /// <summary>
        /// Generate a SQL to remove permission on a table for specified user
        /// </summary>
        /// <param name="removeTablePermissionOperation">The operation to produce sql for.</param>
        public virtual void Generate(RevokeTablePermissionOperation removeTablePermissionOperation)
        {
            using (var writer = Writer())
            {
                writer.WriteLine(
            "REVOKE {0} ON {1} TO {2}",
            removeTablePermissionOperation.Permission.ToString().ToUpper(),
            removeTablePermissionOperation.Table,
            removeTablePermissionOperation.User);

                this.Statement(writer);
            }
        }

        /// <summary>
        /// Generate a SQL to alter collation in database
        /// </summary>
        /// <param name="databaseCollationOperation">The operation to produce sql for.</param>
        public virtual void Generate(DatabaseCollationOperation databaseCollationOperation)
        {
            using (var writer = Writer())
            {
                writer.WriteLine(
            "ALTER DATABASE {0} COLLATE {1}",
            databaseCollationOperation.DatabaseName,
            databaseCollationOperation.Collation);

                this.Statement(writer);
            }
        }

        /// <summary>
        /// Generate a SQL to create a new view in database
        /// </summary>
        /// <param name="createViewOperation">The operation to produce sql for.</param>
        public virtual void Generate(CreateViewOperation createViewOperation)
        {
            using (var writer = Writer())
            {
                writer.WriteLine("CREATE VIEW {0} AS {1};", createViewOperation.ViewName, createViewOperation.BodySql);

                this.Statement(writer);
            }
        }

        /// <summary>
        /// Generate a SQL to drop a existing view in database
        /// </summary>
        /// <param name="dropViewOperation">The operation to produce sql for.</param>
        public virtual void Generate(DropViewOperation dropViewOperation)
        {
            using (var writer = Writer())
            {
                writer.WriteLine("DROP VIEW {0} ", dropViewOperation.ViewName);

                this.Statement(writer);
            }
        }

        /// <summary>
        /// Generate a sql from existing sql file
        /// </summary>
        /// <param name="sqlFileOperation">the operation to produce sql for.</param>
        public virtual void Generate(SqlFileOperation sqlFileOperation)
        {
            using (var writer = Writer())
            {
                using (var stream = sqlFileOperation.SqlFileStream)
                using (var reader = new StreamReader(stream))
                {
                    writer.Write(reader.ReadToEnd());

                    this.Statement(writer);
                }
            }
        }

        /// <summary>
        /// Generate sql from the embeded resource.
        /// </summary>
        /// <param name="sqlResourceOperation">The operation to produce sql for.</param>
        public virtual void Generate(SqlResourceOperation sqlResourceOperation)
        {
            using (var writer = Writer())
            {
                var resourceStream = sqlResourceOperation.Assembly
                    .GetManifestResourceStream(sqlResourceOperation.ResourceName);

                if (resourceStream != null)
                {
                    using (var reader = new StreamReader(resourceStream))
                    {
                        writer.Write(reader.ReadToEnd());

                        this.Statement(writer);
                    }
                }
                else
                {
                    throw Error.SqlResourceNotExist(sqlResourceOperation.ResourceName, sqlResourceOperation.Assembly.FullName);
                }
            }
        }

        /// <summary>
        /// Generate a SQL to add  new computed column in a table at database
        /// </summary>       
        /// <param name="addComputedColumnOperation"></param>
        public virtual void Generate(AddComputedColumnOperation addComputedColumnOperation)
        {
            using (var writer = Writer())
            {
                object computedColumn;
                addComputedColumnOperation.AnonymousArguments.TryGetValue(addComputedColumnOperation.AnonymousArguments.FirstOrDefault().Key, out computedColumn);

                writer.WriteLine("ALTER TABLE {0} ADD {1} AS {2};", addComputedColumnOperation.TableName, addComputedColumnOperation.ColumnName, computedColumn);

                this.Statement(writer);
            }
        }

        /// <summary>
        /// Generate a SQL to DROP a computed column in a table at database
        /// </summary>       
        /// <param name="dropComputedColumnOperation"></param>
        public virtual void Generate(DropComputedColumnOperation dropComputedColumnOperation)
        {
            using (var writer = Writer())
            {                
                writer.WriteLine("ALTER TABLE {0} DROP COLUMN {1};", dropComputedColumnOperation.TableName, dropComputedColumnOperation.ColumnName);

                this.Statement(writer);
            }
        }
    }
}
