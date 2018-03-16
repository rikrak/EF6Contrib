using System.Linq;

namespace UnitTests.Migrations.Model
{
    using System;
    using EntityFramework.Contrib.Migrations.Model;
    using EntityFramework.Contrib.Properties.Resources;
    using Xunit;

    public class AddComputedColumnOperationTests
    {
        [Fact]
        public void Ctor_throw_if_table_name_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("tableName"),
                Assert.Throws<ArgumentException>(() => new AddComputedColumnOperation(null, "columnName", "bodySql")).Message);
        }
        [Fact]
        public void Ctor_throw_if_table_name_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("tableName"),
                Assert.Throws<ArgumentException>(() => new AddComputedColumnOperation(string.Empty, "columnName", "bodySql")).Message);
        }


        [Fact]
        public void Ctor_throw_if_column_name_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("columnName"),
                Assert.Throws<ArgumentException>(() => new AddComputedColumnOperation("tableName", null, "bodySql")).Message);
        }
        [Fact]
        public void Ctor_throw_if_column_name_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("columnName"),
                Assert.Throws<ArgumentException>(() => new AddComputedColumnOperation("tableName", string.Empty, "bodySql")).Message);
        }        

        [Fact]
        public void Can_get_properties()
        {
            var operation = new AddComputedColumnOperation("tableName", "columnName", new { ComputedColumn = "computedColumn" });

            Assert.Equal("tableName", operation.TableName);
            Assert.Equal("columnName", operation.ColumnName);
            
            object computedColumn;
            operation.AnonymousArguments.TryGetValue(operation.AnonymousArguments.FirstOrDefault().Key, out computedColumn);

            Assert.Equal("computedColumn", computedColumn);
        }
    }
}
