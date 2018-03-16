namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represent a drop view operation
    /// </summary>
    public class DropViewOperation
        :MigrationOperation
    {
        /// <summary>
        /// Get the view name to drop
        /// </summary>
        public string ViewName { get; private set; }

        /// <summary>
        /// Create a new View
        /// </summary>
        /// <param name="viewName">the view name to drop</param>
        public DropViewOperation(string viewName)
            :base(null)
        {
            Check.NotEmpty(viewName, "viewName");

            ViewName = viewName;
        }

        /// <inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return true; }
        }
    }
}
