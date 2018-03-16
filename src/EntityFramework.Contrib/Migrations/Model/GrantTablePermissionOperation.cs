namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represent set permission in database
    /// </summary>
    public sealed class GrantTablePermissionOperation
        :TablePermissionOperation
    {   
        /// <summary>
        /// Create a new instance of this operation
        /// </summary>
        /// <param name="table">The table you specify the permission</param>
        /// <param name="user">The user you specify the permission</param>
        /// <param name="permission">The permission to specify</param>
        public GrantTablePermissionOperation(string table, string user, TablePermission permission)
            :base(table,user,permission)
        {
        }

        /// <inheritdoc />
        public override bool IsDestructiveChange
        {
            get { return false; }
        }

        /// <inheritdoc />
        public override MigrationOperation Inverse
        {
            get
            {
                return new RevokeTablePermissionOperation(Table, User, Permission);
            }
        }
    }
}
