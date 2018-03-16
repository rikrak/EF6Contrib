namespace EntityFramework.Contrib.Migrations.Model
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represents a computed column being added to a table.
    /// </summary>
    public class AddComputedColumnOperation : MigrationOperation
    {
        /// <summary>
        /// The table name
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// The column name
        /// </summary>
        public string ColumnName { get; private set; }       
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="anonymousArguments"></param>
        public AddComputedColumnOperation(string tableName, string columnName, object anonymousArguments)
            : base(anonymousArguments)
        {
            Check.NotEmpty(tableName, "tableName");
            Check.NotEmpty(columnName, "columnName");            

            this.TableName = tableName;
            this.ColumnName = columnName;            
        }

        /// <inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return false; }
        }

        /// <inheritdoc/>
        public override MigrationOperation Inverse
        {
            get
            {
                return new DropComputedColumnOperation(TableName,ColumnName);
            }
        }
    }
}
