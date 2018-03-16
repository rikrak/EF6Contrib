namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.SqlServerPerformanceAnalyzers.Properties.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Represent a <see cref="IPerformanceAnalyzer"/> that check if query
    /// is on TOP  SLOW queries in database.
    /// </summary>
    public class TopSlowQueriesPerformanceAnalyzer
        : IPerformanceAnalyzer
    {
        int _topQueries;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="topQueries">the number of top queries to be used</param>
        public TopSlowQueriesPerformanceAnalyzer(int topQueries)
        {
            _topQueries = topQueries;
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        public TopSlowQueriesPerformanceAnalyzer()
            : this(5) { }

        ///<inheritdoc/>
        public PerformanceCommandReport Analyze(DbCommand command, TimeSpan executionTime)
        {
            var topSlowQueries = GetTopSlowQueries(command.Connection.ConnectionString);

            if (topSlowQueries.Contains(command.CommandText))
            {
                return new PerformanceCommandReport(command, 
                    Strings.InTopSlowQueries(command.CommandText), 
                    PerformanceCommandReportCategory.Warning);
            }

            return null;
        }

        /// <summary>
        /// Get the top slow queries
        /// </summary>
        /// <param name="connectionString">The connection string to use for get the top slow queries</param>
        /// <returns>A collection with top slow queries</returns>
        public virtual IEnumerable<string> GetTopSlowQueries(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = "SELECT TOP "
    + _topQueries.ToString()
    + " SUBSTRING(qt.text, (qs.statement_start_offset/2)+1, "
    + "((CASE qs.statement_end_offset WHEN -1 THEN DATALENGTH(qt.text) "
    + "                               ELSE qs.statement_end_offset"
    + "  END - qs.statement_start_offset)/2)+1)"
+ " FROM "
    + "sys.dm_exec_query_stats qs"
+ " CROSS APPLY "
    + "sys.dm_exec_sql_text(qs.sql_handle) qt"
+ " CROSS APPLY "
    + "sys.dm_exec_query_plan(qs.plan_handle) qp"
+ " WHERE "
    + "qt.encrypted = 0"
+ " ORDER BY "
    + "qs.total_logical_reads DESC";


                var slowQueriesCommand = connection.CreateCommand();
                slowQueriesCommand.CommandText = commandText;

                connection.Open();

                var reader = slowQueriesCommand
                    .ExecuteReader(CommandBehavior.SequentialAccess);

                while (reader.Read())
                {
                    yield return reader.GetString(0);
                }

                yield break;
            }
        }
    }
}
