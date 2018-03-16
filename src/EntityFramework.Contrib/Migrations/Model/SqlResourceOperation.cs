namespace System.Data.Entity.Migrations.Model
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;
    using System.Reflection;

    /// <summary>
    /// Represent sql code stored in embebed resource
    /// </summary>
    public class SqlResourceOperation
        :MigrationOperation
    {
        /// <summary>
        /// Get the resource name with sql to be executed
        /// </summary>
        public string ResourceName { get; private set;}

        /// <summary>
        /// Get the assembly with the resource
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="assembly">The assembly that contains the resource.</param>
        /// <param name="resourceName">The resource name with the sql to be executed.</param>
        public SqlResourceOperation(Assembly assembly,string resourceName)
            :base(null)
        {
            Check.NotNull(assembly,"assembly");
            Check.NotNull(resourceName,"resourceName");

            ResourceName = resourceName;
            Assembly = assembly;
        }

        /// <inheritdoc/>
        public override MigrationOperation Inverse
        {
            get
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }
}
