namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represent  set database collation
    /// </summary>
    public class DatabaseCollationOperation
        :MigrationOperation
    {
        /// <summary>
        /// The database name you specify the collation
        /// </summary>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// The collation to specify
        /// </summary>
        public string Collation { get; private set; }

        /// <summary>
        /// Createa a new instance
        /// </summary>
        /// <param name="databaseName">the database name you specify the collation</param>
        /// <param name="collation">the collation to specify</param>
        public DatabaseCollationOperation(string databaseName, string collation)
            : base(null)
        {
            Check.NotEmpty(databaseName,"databaseName");
            Check.NotEmpty(collation,"collation");

            DatabaseName = databaseName;
            Collation = collation;
        }

        /// <inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }
}
