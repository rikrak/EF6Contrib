namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.Properties.Resources;
    using Xunit;

    public class PerformanceCommandReportTests
    {
        [Fact]
        public void Ctor_throw_if_command_is_null()
        {
            Assert.Equal(Assert.Throws<ArgumentNullException>(() =>
            {
                new PerformanceCommandReport(null, "", PerformanceCommandReportCategory.Warning);

            }).Message, Error.ArgumentNull("command").Message);
        }
        [Fact]
        public void Ctor_throw_if_report_is_null()
        {
            Assert.Equal(Assert.Throws<ArgumentNullException>(() =>
            {
                new PerformanceCommandReport(DbCommandUtils.CreateCommand("Hi!"), null, PerformanceCommandReportCategory.Warning);

            }).Message, Error.ArgumentNull("message").Message);
        }
    }
}
