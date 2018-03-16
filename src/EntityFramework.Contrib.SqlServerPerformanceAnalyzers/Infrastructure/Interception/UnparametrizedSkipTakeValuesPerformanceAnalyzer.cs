namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.SqlServerPerformanceAnalyzers.Properties.Resources;
    using System;
    using System.Data.Common;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represent a <see cref="IPerformanceAnalyzer"/> that check if skip and take values are not parametrized
    /// </summary>
    public class UnparametrizedSkipTakeValuesPerformanceAnalyzer
        :IPerformanceAnalyzer
    {
        const string UNPARAMETRIZED_SKIP_TAKE_PATTERN = @"\[row_number\]*>*<*\s{1}\d*";

        ///<inheritdoc/>
        public PerformanceCommandReport Analyze(DbCommand command, TimeSpan executionTime)
        {
            var regex = new Regex(UNPARAMETRIZED_SKIP_TAKE_PATTERN, RegexOptions.Compiled | RegexOptions.Multiline);
            var match = regex.Match(command.CommandText);

            if (match.Success)
            {
                var report = new PerformanceCommandReport(command, 
                    Strings.UnparametrizedSkipOrTake(command.CommandText),
                    PerformanceCommandReportCategory.Warning);

                return report;
            }

            return null;
        }
    }
}
