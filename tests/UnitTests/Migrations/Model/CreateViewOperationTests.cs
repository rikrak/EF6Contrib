namespace System.Data.Entity.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using Xunit;

    public class CreateViewOperationTests
    {
        [Fact]
        public void Ctor_throw_if_view_name_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("viewName"),
                Assert.Throws<ArgumentException>(() => new CreateViewOperation(null, "bodysql")).Message);
        }
        [Fact]
        public void Ctor_throw_if_view_name_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("viewName"),
                Assert.Throws<ArgumentException>(() => new CreateViewOperation(string.Empty, "bodysql")).Message);
        }
        [Fact]
        public void Ctor_throw_if_bodySql_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("bodySql"),
                Assert.Throws<ArgumentException>(() => new CreateViewOperation("viewname", null)).Message);
        }
        [Fact]
        public void Ctor_throw_if_bodySql_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("bodySql"),
                Assert.Throws<ArgumentException>(() => new CreateViewOperation("viewname", string.Empty)).Message);
        }

        [Fact]
        public void Can_get_properties()
        {
            var operation = new CreateViewOperation("viewName", "bodySql");

            Assert.Equal("viewName", operation.ViewName);
            Assert.Equal("bodySql", operation.BodySql);
        }
    }
}
