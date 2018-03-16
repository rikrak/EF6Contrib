namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.SqlServerPerformanceAnalyzers.Properties.Resources;
    using System;
    using Xunit;


    public class UnparametrizedWhereClausesPerformanceAnalyzerTests
    {
        [Fact]
        public void Analyze_return_null_report_for_parametrized_queries()
        {
            var analyzer = new UnparametrizedWhereClausesPerformanceAnalyzer();
            var command = DbCommandUtils.CreateCommand("SELECT * FROM Customers WHERE Id=@Id");

            var report = analyzer.Analyze(command, TimeSpan.FromSeconds(1));

            Assert.Null(report);
        }

        [Fact]
        public void Analyze_return_report_for_query_with_values_with_quotes()
        {
            var commandText = "SELECT * FROM [dbo].Customers WHERE FirstName = 'Unai'";
            var analyzer = new UnparametrizedWhereClausesPerformanceAnalyzer();
            var command = DbCommandUtils.CreateCommand(commandText);

            var report = analyzer.Analyze(command, TimeSpan.FromSeconds(1));

            Assert.NotNull(report);
            Assert.Equal(report.Message, Strings.UnparametrizedWhereClause(commandText));
        }

        [Fact]
        public void Analyze_return_report_for_query_with_values_without_quotes()
        {
            var commandText = "SELECT * FROM [dbo].Customers WHERE Age=  36";
            var analyzer = new UnparametrizedWhereClausesPerformanceAnalyzer();
            var command = DbCommandUtils.CreateCommand(commandText);

            var report = analyzer.Analyze(command, TimeSpan.FromSeconds(1));

            Assert.NotNull(report);
            Assert.Equal(report.Message, Strings.UnparametrizedWhereClause(commandText));
        }

        [Fact]
        public void Analyze_return_report_for_non_parametrized_queries_and_without_formated_whitespaces()
        {
            var commandText = "SELECT * FROM [dbo].Customers WHERE FirstName='Unai'";
            var analyzer = new UnparametrizedWhereClausesPerformanceAnalyzer();
            var command = DbCommandUtils.CreateCommand(commandText);

            var report = analyzer.Analyze(command, TimeSpan.FromSeconds(1));

            Assert.NotNull(report);
            Assert.Equal(report.Message, Strings.UnparametrizedWhereClause(commandText));
        }

        [Fact]
        public void Analyze_return_report_if_exist_any_non_parametrized_parameter()
        {
            var commandText = "SELECT * FROM [dbo].Customers WHERE FirstName = @Unai AND LastName = 'Zorrilla'";
            var analyzer = new UnparametrizedWhereClausesPerformanceAnalyzer();
            var command = DbCommandUtils.CreateCommand(commandText);

            var report = analyzer.Analyze(command, TimeSpan.FromSeconds(1));

            Assert.NotNull(report);
            Assert.Equal(report.Message, Strings.UnparametrizedWhereClause(commandText));
        }
    }
}
