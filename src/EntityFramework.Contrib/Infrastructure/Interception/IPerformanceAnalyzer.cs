namespace System.Data.Entity.Infrastructure.Interception
{
    using System;
    using System.Data.Common;

    public interface IPerformanceAnalyzer
    {
        PerformanceCommandReport Analyze(DbCommand command, TimeSpan executionTime);
    }
}
