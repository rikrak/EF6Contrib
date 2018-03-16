
namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.SqlServerPerformanceAnalyzers.Properties.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;


    public class UnparametrizedSkipTakeValuesPerformanceAnalyzerTests
    {
        [Fact]
        public void Analyze_return_report_for_queries_with_unparametrized_skiportake()
        {
            var commandText = "SELECT TOP (20) [Extent1].[Id] AS [Id]"
                + ", [Extent1].[FirstName] AS [FirstName] FROM ( SELECT [Extent1].[Id] AS [Id], [Extent1].[FirstName] AS [FirstName], "
                + " row_number() OVER (ORDER BY [Extent1].[FirstName] ASC) AS [row_number]" 
                + "FROM [dbo].[Customers] AS [Extent1])  AS [Extent1] WHERE [Extent1].[row_number] > 10 ORDER BY [Extent1].[FirstName] ASC";

            var command = DbCommandUtils.CreateCommand(commandText);

            var analyzer = new UnparametrizedSkipTakeValuesPerformanceAnalyzer();
            var report = analyzer.Analyze(command,TimeSpan.FromSeconds(1));

            Assert.NotNull(report);
            Assert.Equal(report.Message, Strings.UnparametrizedSkipOrTake(commandText));
        }
    }
}
