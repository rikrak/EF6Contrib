namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.Properties.Resources;
    using System;
    using System.Data.Common;

    /// <summary>
    /// Represent a <see cref="IPerformanceAnalyzer"/> based on command execution time.
    /// This get a report if the command execution time execeeds the maximun allowed execution time.
    /// By default the maximun execution time is 1 second.
    /// </summary>
    public class ExecutionTimePerformanceAnalyzer
        :IPerformanceAnalyzer
    {
        private readonly TimeSpan _maxAllowedExecutionTime;

        /// <summary>
        /// Get the max allowed execution time
        /// </summary>
        public TimeSpan MaxAllowedExecutionTime
        {
            get
            {
                return _maxAllowedExecutionTime;
            }
        }

        /// <summary>
        /// Create a new instance with default maximun allowed execution time as 1 second.
        /// </summary>
        public ExecutionTimePerformanceAnalyzer()
            :this(TimeSpan.FromSeconds(1))
        {
        }

        /// <summary>
        /// Create a new instance and configure the maximun allowed execution time.
        /// </summary>
        /// <param name="maxAllowedExecutionTime">The maximun allowed execution time to set.</param>
        public ExecutionTimePerformanceAnalyzer(TimeSpan maxAllowedExecutionTime)
        {
            _maxAllowedExecutionTime = maxAllowedExecutionTime;
        }

        /// <summary>
        /// Analyze the command and check if the execution time is greather that maximun allowed execution time.
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="executionTime">The command execution time</param>
        /// <returns>A report if execution time execeeds maximun allowed execution time.</returns>
        public virtual PerformanceCommandReport Analyze(DbCommand command, TimeSpan executionTime)
        {
            if (executionTime > _maxAllowedExecutionTime)
            {
                return new PerformanceCommandReport(command, 
                        Strings.ExceedsMaxExecutionTime(executionTime.TotalSeconds,_maxAllowedExecutionTime.TotalSeconds),
                        PerformanceCommandReportCategory.Warning);
            }

            return null;
        }
    }
}
