namespace System.Data.Entity.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using Xunit;

    public class GrantTablePermissionOperationTests
    {
        [Fact]
        public void Ctor_throw_if_table_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("table"),
                Assert.Throws<ArgumentException>(() => new GrantTablePermissionOperation(null, "user", TablePermission.Select)).Message);
        }
        [Fact]
        public void Ctor_throw_if_table_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("table"),
                Assert.Throws<ArgumentException>(() => new GrantTablePermissionOperation(string.Empty, "user", TablePermission.Select)).Message);
        }
        [Fact]
        public void Ctor_throw_if_user_is_null()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("user"),
                Assert.Throws<ArgumentException>(() => new GrantTablePermissionOperation("tableName", null, TablePermission.Select)).Message);
        }
        [Fact]
        public void Ctor_throw_if_user_is_empty()
        {
            Assert.Equal(Strings.ArgumentIsNullOrWhitespace("user"),
                Assert.Throws<ArgumentException>(() => new GrantTablePermissionOperation("tableName", string.Empty, TablePermission.Select)).Message);
        }
        [Fact]
        public void Can_get_properties()
        {
            var operation = new GrantTablePermissionOperation("dbo.Table", "user", TablePermission.Select);

            Assert.Equal("dbo.Table", operation.Table);
            Assert.Equal("user", operation.User);
            Assert.Equal(TablePermission.Select, operation.Permission);
        }
    }
}
