
namespace System.Data.Entitty.Infrastructure.Interception
{
    using EntityFramework.Contrib.SqlServerPerformanceAnalyzers.Properties.Resources;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;


    public class TopSlowQueriesPerformanceAnalyzerTests
    {
        [Fact]
        public void Analyze_return_null_if_query_is_not_in_top_slow_queries()
        {
            var analyzerMock = new Mock<TopSlowQueriesPerformanceAnalyzer>();
            analyzerMock.Setup(tt => tt.GetTopSlowQueries(It.IsAny<String>()))
                .Returns(() => new List<string>()
                {
                    "SELECT * FROM A",
                    "SELECT * FROM B",
                    "SELECT * FROM C",
                });

            var command = new SqlCommand("SELECT * FROM X",new SqlConnection(@"SERVER=.\SQLEXPRESS;Initial Catalog=Master"));
            var analyzer = analyzerMock.Object;

            var report = analyzer
                .Analyze(command, TimeSpan.FromSeconds(1));

            Assert.Null(report);
        }

        [Fact]
        public void Analyze_return_report_if_query_is_not_in_top_slow_queries()
        {
            var analyzerMock = new Mock<TopSlowQueriesPerformanceAnalyzer>();
            analyzerMock.Setup(tt => tt.GetTopSlowQueries(It.IsAny<String>()))
                .Returns(() => new List<string>()
                {
                    "SELECT * FROM A",
                    "SELECT * FROM X",
                    "SELECT * FROM C",
                });

            var command = new SqlCommand("SELECT * FROM X", new SqlConnection(@"SERVER=.\SQLEXPRESS;Initial Catalog=Master"));
            var analyzer = analyzerMock.Object;

            var report = analyzer
                .Analyze(command, TimeSpan.FromSeconds(1));

            Assert.Equal(report.Message, Strings.InTopSlowQueries("SELECT * FROM X"));
        }
    }
}
