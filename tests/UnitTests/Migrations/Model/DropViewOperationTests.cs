namespace UnitTests.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using System.Data.Entity.Migrations.Model;
    using Xunit;

    public class DropViewOperationTests
    {
        [Fact]
        public void Ctor_throw_if_database_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("viewName"),
                Assert.Throws<ArgumentException>(() => new DropViewOperation(null)).Message);
        }

        [Fact]
        public void Ctor_throw_if_database_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("viewName"),
                Assert.Throws<ArgumentException>(() => new DropViewOperation(string.Empty)).Message);
        }

        [Fact]
        public void Can_get_properties()
        {
            var operation = new DropViewOperation("viewName");

            Assert.Equal("viewName", operation.ViewName);
        }
    }
}
