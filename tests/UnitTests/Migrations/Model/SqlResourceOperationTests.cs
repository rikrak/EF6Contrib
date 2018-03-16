

namespace UnitTests.Migrations.Model
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using System.Data.Entity.Migrations.Model;
    using Xunit;

    public class SqlResourceOperationTests
    {
        [Fact]
        public void Ctor_throw_if_assembly_is_null()
        {
            Assert.Equal(Assert.Throws<ArgumentNullException>(() =>
            {
                new SqlResourceOperation(null, "resourceName");

            }).Message, Error.ArgumentNull("assembly").Message);
        }

        [Fact]
        public void Ctor_throw_if_resource_name_is_null()
        {
            Assert.Equal(Assert.Throws<ArgumentNullException>(() =>
            {
                new SqlResourceOperation(typeof(SqlResourceOperationTests).Assembly, null);

            }).Message, Error.ArgumentNull("resourceName").Message);
        }


        [Fact]
        public void Can_get_properties()
        {
            var operation = new SqlResourceOperation(typeof(SqlResourceOperationTests).Assembly, "resourceName");

            Assert.True(operation.ResourceName == "resourceName");
            Assert.True(operation.Assembly == typeof(SqlResourceOperationTests).Assembly);
        }
    }
}
