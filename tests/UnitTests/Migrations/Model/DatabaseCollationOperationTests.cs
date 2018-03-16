namespace System.Data.Entity.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using Xunit;

    public class DatabaseCollationOperationTests
    {
        [Fact]
        public void Ctor_throw_if_database_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("databaseName"),
                Assert.Throws<ArgumentException>(() => new DatabaseCollationOperation(null, "collation")).Message);
        }
        [Fact]
        public void Ctor_throw_if_database_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("databaseName"),
                Assert.Throws<ArgumentException>(() => new DatabaseCollationOperation(string.Empty, "collation")).Message);
        }
        [Fact]
        public void Ctor_throw_if_collation_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("collation"),
                Assert.Throws<ArgumentException>(() => new DatabaseCollationOperation("dbname", null)).Message);
        }
        [Fact]
        public void Ctor_throw_if_collation_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("collation"),
                Assert.Throws<ArgumentException>(() => new DatabaseCollationOperation("dbname", string.Empty)).Message);
        }
      
        [Fact]
        public void Can_get_properties()
        {
            var operation = new DatabaseCollationOperation("dbname", "collation");

            Assert.Equal("dbname", operation.DatabaseName);
            Assert.Equal("collation", operation.Collation);
        }
    }
}
