namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.Properties.Resources;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using Xunit;

    public class PerformanceInterceptorTests
    {
        [Fact]
        public void Ctor_throw_if_output_is_null()
        {
            Assert.Equal(Assert.Throws<ArgumentNullException>(() =>
            {
                new PerformanceInterceptor(null);

            }).Message, Error.ArgumentNull("output").Message);
        }

        [Fact]
        public void Ctor_throw_if_analyzers_is_null()
        {
            Assert.Equal(Assert.Throws<ArgumentNullException>(() =>
            {
                new PerformanceInterceptor((r) => { },null);

            }).Message, Error.ArgumentNull("analyzers").Message);
        }

        [Fact]
        public void NonQueryExecuted_execute_configured_analyzers()
        {
            var command =  DbCommandUtils.CreateCommand("Hi!");
            var analyzer = new Mock<IPerformanceAnalyzer>();

            var analyzers = new List<IPerformanceAnalyzer>
            {
                analyzer.Object
            };

            var performanceInterceptor = new PerformanceInterceptor((report) => { }, analyzers);

            performanceInterceptor.NonQueryExecuted(DbCommandUtils.CreateCommand("Hi!"), new DbCommandInterceptionContext<int>());

            analyzer.Verify(a => a.Analyze(It.Is<DbCommand>(c => c.CommandText =="Hi!"), It.Is<TimeSpan>(t => t.TotalSeconds == 0)),Times.Once());

        }

        [Fact]
        public void ScalarExecuted_execute_configured_analyzers()
        {
            var command = DbCommandUtils.CreateCommand("Hi!");
            var analyzer = new Mock<IPerformanceAnalyzer>();

            var analyzers = new List<IPerformanceAnalyzer>
            {
                analyzer.Object
            };

            var performanceInterceptor = new PerformanceInterceptor((report) => { }, analyzers);

            performanceInterceptor.ScalarExecuted(DbCommandUtils.CreateCommand("Hi!"), new DbCommandInterceptionContext<object>());

            analyzer.Verify(a => a.Analyze(It.Is<DbCommand>(c => c.CommandText == "Hi!"), It.Is<TimeSpan>(t => t.TotalSeconds == 0)), Times.Once());

        }

        [Fact]
        public void ReaderExecuted_execute_configured_analyzers()
        {
            var command = DbCommandUtils.CreateCommand("Hi!");
            var analyzer = new Mock<IPerformanceAnalyzer>();

            var analyzers = new List<IPerformanceAnalyzer>
            {
                analyzer.Object
            };

            var performanceInterceptor = new PerformanceInterceptor((report) => { }, analyzers);

            performanceInterceptor.ReaderExecuted(DbCommandUtils.CreateCommand("Hi!"), new DbCommandInterceptionContext<DbDataReader>());

            analyzer.Verify(a => a.Analyze(It.Is<DbCommand>(c => c.CommandText == "Hi!"), It.Is<TimeSpan>(t => t.TotalSeconds == 0)), Times.Once());

        }

    }
}
