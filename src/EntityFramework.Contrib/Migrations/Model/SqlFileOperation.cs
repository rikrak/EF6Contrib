
namespace System.Data.Entity.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System.Data.Entity.Utilities;
    using System.IO;

    /// <summary>
    /// Represent a execution of sql file into database
    /// </summary>
    public class SqlFileOperation
        :MigrationOperation
    {
        /// <summary>
        /// Get or set the sql file stream to execute
        /// </summary>
        public Stream SqlFileStream { get; private set; }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="sqlFilePath">The path of sql file to execute</param>
        public SqlFileOperation(string sqlFilePath)
            :base(null)
        {
            Check.NotEmpty(sqlFilePath, "sqlFilePath");

            if (File.Exists(sqlFilePath))
            {
                SqlFileStream = File.OpenRead(sqlFilePath);
            }
            else
            {
                throw Error.SqlFileOperation_the_file_not_exist(sqlFilePath);
            }
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="sqlFileStream">The file stream of sql file to execute</param>
        public SqlFileOperation(Stream sqlFileStream)
            :base(null)
        {
            Check.NotNull(sqlFileStream,"sqlFileStream");

            SqlFileStream = sqlFileStream;
        }

        ///<inheritdoc/>
        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }
}
