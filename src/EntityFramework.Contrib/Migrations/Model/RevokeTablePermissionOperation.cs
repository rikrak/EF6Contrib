namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represent revoke permission in table
    /// </summary>
    public sealed class RevokeTablePermissionOperation
        :TablePermissionOperation
    {
       
        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="table">Get the table you revoke the permission</param>
        /// <param name="user">Get the user you revoke the permission</param>
        /// <param name="permission">The permission to revoke</param>
        public RevokeTablePermissionOperation(string table, string user, TablePermission permission)
            :base(table,user,permission)
        {
          
        }

        /// <inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }

}
