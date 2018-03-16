namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.SqlServerPerformanceAnalyzers.Properties.Resources;
    using System.Data.Common;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Reprensent a <see cref="IPerformanceAnalyzer"/> that check if the executed queries is not parametrized.
    /// </summary>
    public sealed class UnparametrizedWhereClausesPerformanceAnalyzer
        : IPerformanceAnalyzer
    {
        const string UNPARAMETRIZED_WHERE_CLAUSE_REGEX_PATTERN = @"([@{0}\w\[\]']*)\s*=\s*(['*\w\[\]]*'*)";

        ///<inheritdoc/>
        public PerformanceCommandReport Analyze(DbCommand command, TimeSpan executionTime)
        {
            var regex =
                new Regex(UNPARAMETRIZED_WHERE_CLAUSE_REGEX_PATTERN, RegexOptions.Compiled | RegexOptions.Multiline);

            foreach (Match m in regex.Matches(command.CommandText))
            {
                if (m.Success)
                {
                    var value = m.Groups[1].Value;
                    var columnName = m.Groups[2].Value;

                    if (!String.IsNullOrWhiteSpace(value)
                        &&
                        !String.IsNullOrWhiteSpace(columnName)
                        &&
                        value.IndexOf('@') == -1)
                    {
                        var report = new PerformanceCommandReport(command, 
                            Strings.UnparametrizedWhereClause(command.CommandText), 
                            PerformanceCommandReportCategory.Warning);

                        return report;
                    }
                }
            }

            return null;
        }
    }
}
