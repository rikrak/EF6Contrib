namespace System.Data.Entity.Infrastructure.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure.DependencyResolution;
    using System.Data.Entity.Utilities;
    using System.Diagnostics;
    using System.Linq;


    /// <summary>
    /// A  command interceptor that use configured  <see cref="IPerformanceAnalyzer"/> 
    /// to execute performance commmand analyisis.
    /// </summary>
    public sealed class PerformanceInterceptor
        : DbCommandInterceptor
    {
        private readonly Action<PerformanceCommandReport> _output;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly IEnumerable<IPerformanceAnalyzer> _analyzers;


        /// <summary>
        /// Create a new instance of this command interceptor.
        /// </summary>
        /// <param name="output">The delegate to which output will be sent.</param>
        public PerformanceInterceptor(Action<PerformanceCommandReport> output)
            :this(output,Enumerable.Empty<IPerformanceAnalyzer>())
        {
                  
        }

        /// <summary>
        /// Createa a new instance of this command internceptor.
        /// </summary>
        /// <param name="output">The delegate to which output will be sent.</param>
        /// <param name="analyzers">The collection of analyzers to be used.</param>
        public PerformanceInterceptor(Action<PerformanceCommandReport> output, IEnumerable<IPerformanceAnalyzer> analyzers)
        {
            Check.NotNull(output, "output");
            Check.NotNull(analyzers, "analyzers");

            _output = output;
            _analyzers = analyzers;
        }

        /// <summary>
        ///     This method is called before a call to <see cref="DbCommand.ExecuteNonQuery" /> or
        ///     one of its async counterparts is made.
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopWatch.Restart();
        }

        /// <summary>
        ///     This method is called after a call to <see cref="DbCommand.ExecuteNonQuery" />  or
        ///     one of its async counterparts is made.
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            ExecuteAnalyzersForCommand(command);
        }

        /// <summary>
        ///     This method is called before a call to <see cref="DbCommand.ExecuteReader(CommandBehavior)" />  or
        ///     one of its async counterparts is made.
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<Common.DbDataReader> interceptionContext)
        {
            _stopWatch.Restart();
        }

        /// <summary>
        ///     This method is called after a call to <see cref="DbCommand.ExecuteReader(CommandBehavior)" />  or
        ///     one of its async counterparts is made.
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<Common.DbDataReader> interceptionContext)
        {
            ExecuteAnalyzersForCommand(command);
        }

        /// <summary>
        ///     This method is called before a call to <see cref="DbCommand.ExecuteScalar" />  or
        ///     one of its async counterparts is made.
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void ScalarExecuting(Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopWatch.Restart();
        }

        /// <summary>
        ///     This method is called after a call to <see cref="DbCommand.ExecuteScalar" />  or
        ///     one of its async counterparts is made.
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        /// <returns>The result to be used by Entity Framework.</returns>
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            ExecuteAnalyzersForCommand(command);
        }

        private void ExecuteAnalyzersForCommand(DbCommand command)
        {
            _stopWatch.Stop();

            var analyzers = _analyzers.Union(DbConfiguration.DependencyResolver
                .GetServices<IPerformanceAnalyzer>());

            foreach (var item in analyzers)
            {
                var report = item.Analyze(command, _stopWatch.Elapsed);

                if (report != null)
                {
                    _output(report);
                }
            }
        }
    }
}
