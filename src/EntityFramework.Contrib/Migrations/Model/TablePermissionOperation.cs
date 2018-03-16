namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Base class for table permission operations
    /// </summary>
    public abstract class TablePermissionOperation
        :MigrationOperation
    {
        /// <summary>
        /// Get the table you revoke the permission
        /// </summary>
        public string Table { get; private set; }

        /// <summary>
        /// Get the user you revoke the permission
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// The permission to revoke
        /// </summary>
        public TablePermission Permission { get; private set; }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="table">Get the table you revoke the permission</param>
        /// <param name="user">Get the user you revoke the permission</param>
        /// <param name="permission">The permission to revoke</param>
        public TablePermissionOperation(string table, string user, TablePermission permission)
            :base(null)
        {
            Check.NotEmpty(table,"table");
            Check.NotEmpty(user,"user");

            Table = table;
            User = user;
            Permission = permission;
        }
    }
}
