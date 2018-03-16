using EntityFramework.Contrib.Migrations.Model;

namespace UnitTests.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using Xunit;

    public class DropComputedColumnOperationTestsationTests
    {
        [Fact]
        public void Ctor_throw_if_table_name_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("tableName"),
                Assert.Throws<ArgumentException>(() => new DropComputedColumnOperation(null, "columnName")).Message);
        }

        [Fact]
        public void Ctor_throw_if_table_name_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("tableName"),
                Assert.Throws<ArgumentException>(() => new DropComputedColumnOperation(string.Empty, "columnName")).Message);
        }

        [Fact]
        public void Ctor_throw_if_column_name_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("columnName"),
                Assert.Throws<ArgumentException>(() => new DropComputedColumnOperation("tableName", null)).Message);
        }

        [Fact]
        public void Ctor_throw_if_column_name_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("columnName"),
                Assert.Throws<ArgumentException>(() => new DropComputedColumnOperation("tableName", string.Empty)).Message);
        }

        [Fact]
        public void Can_get_properties()
        {
            var operation = new DropComputedColumnOperation("tableName","columnName");

            Assert.Equal("tableName", operation.TableName);
            Assert.Equal("columnName", operation.ColumnName);
        }
    }
}
