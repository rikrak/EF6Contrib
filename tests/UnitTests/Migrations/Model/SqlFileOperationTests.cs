
namespace UnitTests.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using System.Data.Entity.Migrations.Model;
    using System.IO;
    using Xunit;

    public class SqlFileOperationTests
    {
        [Fact]
        public void Ctor_throw_if_sql_file_path_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("sqlFilePath"),
              Assert.Throws<ArgumentException>(() => new SqlFileOperation((string)null)).Message);
        }

        [Fact]
        public void Ctor_throw_if_sql_file_path_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("sqlFilePath"),
              Assert.Throws<ArgumentException>(() => new SqlFileOperation(string.Empty)).Message);
        }

        [Fact]
        public void Ctor_throw_if_sql_file_path_not_exist()
        {
            Assert.Equal(Error.SqlFileOperation_the_file_not_exist("ANonExistingFilePath").Message,
              Assert.Throws<InvalidOperationException>(() => new SqlFileOperation("ANonExistingFilePath")).Message);
        }

        [Fact]
        public void Ctor_throw_if_sql_file_string_is_null()
        {
            Assert.Equal(new ArgumentNullException("sqlFileStream").Message,
              Assert.Throws<ArgumentNullException>(() => new SqlFileOperation((Stream)null)).Message);
        }

        [Fact]
        public void Can_get_properties()
        {
            var operation = new SqlFileOperation(@".\fixtures\SqlFileOperation.sql");

            Assert.NotNull(operation.SqlFileStream);
        }
    }
}
