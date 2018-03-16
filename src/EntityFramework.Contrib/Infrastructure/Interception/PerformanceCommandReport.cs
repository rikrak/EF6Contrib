namespace System.Data.Entity.Infrastructure.Interception
{
    using System.Data.Common;
    using System.Data.Entity.Utilities;

    public sealed class PerformanceCommandReport
    {
        public DbCommand Command { get; private set; }

        public string Message { get; private set; }

        public PerformanceCommandReportCategory Category { get; private set; }


        public PerformanceCommandReport(DbCommand command,string message,PerformanceCommandReportCategory category)
        {
            Check.NotNull(command, "command");
            Check.NotNull(message, "message");

            Command = command;
            Message = message;
            Category = category;
        }
    }
}
