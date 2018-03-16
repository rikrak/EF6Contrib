namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using Xunit;

    public class ExecutionTimePerformanceAnalyzerTests
    {
        [Fact]
        public void Ctor_can_set_the_max_allowed_execution_time()
        {
            TimeSpan maxAllowedExecutionTime = TimeSpan.FromSeconds(4.5);

            var analyzer = new ExecutionTimePerformanceAnalyzer(maxAllowedExecutionTime);

            Assert.Equal(maxAllowedExecutionTime, analyzer.MaxAllowedExecutionTime);
        }

        [Fact]
        public void Analyze_return_null_if_execution_time_is_less_than_max_allowed_execution_time()
        {
            var analyzer = new ExecutionTimePerformanceAnalyzer();

            var report = analyzer.Analyze(DbCommandUtils.CreateCommand("Hi!"), TimeSpan.FromSeconds(0.5));

            Assert.Null(report);
        }

        [Fact]
        public void Analyze_return_report_if_execution_time_exceeds_max_allowed_execution_time()
        {
            var analyzer = new ExecutionTimePerformanceAnalyzer();

            var report = analyzer.Analyze(DbCommandUtils.CreateCommand("Hi!"), TimeSpan.FromSeconds(1.5));

            Assert.NotNull(report);
            Assert.Equal(report.Message, Strings.ExceedsMaxExecutionTime(1.5, 1));
        }
    }
}
