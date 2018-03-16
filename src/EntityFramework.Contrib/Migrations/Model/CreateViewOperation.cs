namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represent a create view in database
    /// </summary>
    public class CreateViewOperation
        :MigrationOperation
    {
        /// <summary>
        /// The view name
        /// </summary>
        public string ViewName { get; private set; }

        /// <summary>
        /// The view body sql
        /// </summary>
        public string BodySql { get; private set; }


        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="viewName">the view name to create</param>
        /// <param name="bodySql">the view body sql </param>
        public CreateViewOperation(string viewName, string bodySql)
            :base(null)
        {
            Check.NotEmpty(viewName,"viewName");
            Check.NotEmpty(bodySql,"bodySql");

            this.ViewName = viewName;
            this.BodySql = bodySql;
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
                return new DropViewOperation(ViewName);
            }
        }
    }
}
