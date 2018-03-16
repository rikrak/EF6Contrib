namespace EntityFramework.Contrib.Migrations.Model
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represent a drop computed column
    /// </summary>
    public class DropComputedColumnOperation : MigrationOperation
    {
        /// <summary>
        /// The table name
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// The olumn name
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        public DropComputedColumnOperation(string tableName, string columnName)
            : base(null)
        {
            Check.NotEmpty(tableName, "tableName");
            Check.NotEmpty(columnName, "columnName");

            this.TableName = tableName;
            this.ColumnName = columnName;

        }

        /// <inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return true; }
        }
    }
}
