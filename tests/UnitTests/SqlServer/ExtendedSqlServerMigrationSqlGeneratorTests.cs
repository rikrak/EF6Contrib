namespace System.Data.Entity.SqlServer
{
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Utilities;
    using Xunit;


    public class ExtendedSqlServerMigrationSqlGeneratorTests
    {
        [Fact]
        public void Generate_redirect_to_specific_method()
        {
            var sqlGenerator = new AdvancedSqlServerMigrationSqlGeneratorForTests();

            var grantTablePermissionOperation = 
                new GrantTablePermissionOperation("table", "user", TablePermission.Alter);

            sqlGenerator.Generate(
                new MigrationOperation[] { grantTablePermissionOperation }, "2008");

            Assert.True(sqlGenerator.GrantTablePermissionOpeartionCalled);
        }

        [Fact]
        public void Generate_GrantTablePermissionOperation_produce_valid_statement()
        {
            var operation = new GrantTablePermissionOperation("dbo.table", "user", TablePermission.Update);
            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("GRANT UPDATE ON dbo.table TO user", sql);
        }

        [Fact]
        public void Generate_RevokeTablePermissionOperation_produce_valid_statement()
        {
            var operation = new RevokeTablePermissionOperation("dbo.table", "user", TablePermission.Insert);
            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("REVOKE INSERT ON dbo.table TO user", sql);
        }

        [Fact]
        public void Generate_DatabaseCollationOperation_produce_valid_statement()
        {
            var operation = new DatabaseCollationOperation("TestsDB", SqlServerDatabaseCollations.Latin1_General_CI_AS);

            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("ALTER DATABASE TestsDB COLLATE Latin1_General_CI_AS", sql);
        }

        [Fact]
        public void Generate_CreateViewOperation_produce_valid_statement()
        {
            var operation = new CreateViewOperation("ViewName", "SELECT * FROM dbo.TableName");

            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("CREATE VIEW ViewName AS SELECT * FROM dbo.TableName", sql);
        }
        [Fact]
        public void Generate_DropViewOperation_produce_valid_statement()
        {
            var operation = new DropViewOperation("ViewName");

            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("DROP VIEW ViewName", sql);
        }

        [Fact]
        public void Generate_SqlFileOperation_produce_sql_file_statements()
        {
            var operation = new SqlFileOperation(@".\fixtures\SqlFileOperation.sql");

            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("--drop database", sql);
        }

        [Fact]
        public void Generate_SqlResourceOperation_produce_sql_file_statements()
        {
            var operation =
                new SqlResourceOperation(typeof(ExtendedSqlServerMigrationSqlGeneratorTests).Assembly, "UnitTests.fixtures.SqlResourceOperation.sql");

            var sqlGenerator = new ExtendedSqlServerMigrationSqlGenerator();

            var sql = sqlGenerator.Generate(new MigrationOperation[] { operation }, "2008").Join(ss => ss.Sql, Environment.NewLine);

            Assert.Contains("--drop database", sql);
        }
    }

    internal class AdvancedSqlServerMigrationSqlGeneratorForTests
        :ExtendedSqlServerMigrationSqlGenerator
    {
        public bool GrantTablePermissionOpeartionCalled
        {
            get;
            private set;
        }
        public override void Generate(GrantTablePermissionOperation grantTablePermissionOperation)
        {
            GrantTablePermissionOpeartionCalled = true;
        }
    }
}
