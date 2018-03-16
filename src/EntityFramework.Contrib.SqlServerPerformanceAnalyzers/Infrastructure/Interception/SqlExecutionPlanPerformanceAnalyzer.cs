namespace System.Data.Entity.Infrastructure.Interception
{
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Xml.Linq;

    /// <summary>
    /// Represent the base class for all <see cref="IPerformanceAnalyzer"/> based on
    /// Sql Plan.
    /// </summary>
    public abstract class SqlExecutionPlanPerformanceAnalyzer
        : IPerformanceAnalyzer
    {
        ///<inheritdoc/>
        public PerformanceCommandReport Analyze(DbCommand command, TimeSpan executionTime)
        {
            var plan = GetQueryPlan(command.Connection.ConnectionString, command.CommandText);

            if (plan != null)
            {
                return EvaluatePlan(plan);
            }

            return null;
        }

        /// <summary>
        /// Get the <see cref="PerformanceCommandReport"/> from Sql Plan
        /// </summary>
        /// <param name="sqlPlan">The sql plan to evaluate</param>
        /// <returns>The performance command report or null if no issues is present in plan</returns>
        public abstract PerformanceCommandReport EvaluatePlan(XDocument sqlPlan);

        /// <summary>
        /// Get query plan from specified command using database information
        /// </summary>
        /// <param name="connectionstring">The connection string to use</param>
        /// <param name="commandText">The command text</param>
        /// <returns>The query plan in XML format or null</returns>
        public virtual XDocument GetQueryPlan(string connectionstring, string commandText)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                var queryPlanCommand = connection.CreateCommand();

                queryPlanCommand.CommandText = "SELECT qp"
                    + " FROM sys.dm_exec_cached_plans as cp CROSS APPLY sys.dm_exec_query_plan(plan_handle) as qp"
                    + " CROSS APPLY sys.dm_exec_sql_text(plan_handle) as qt WHERE qt=@qt";

                queryPlanCommand.Parameters.AddWithValue("@qt", commandText);

                var plan = queryPlanCommand.ExecuteScalar();

                if (plan != DBNull.Value)
                {
                    return XDocument.Parse(plan.ToString());
                }

                return null;
            }
        }
    }
}
