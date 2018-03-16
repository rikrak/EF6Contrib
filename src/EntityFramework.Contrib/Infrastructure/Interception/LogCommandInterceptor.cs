namespace System.Data.Entity.Infrastructure.Interception
{
    using EntityFramework.Contrib.Properties.Resources;
    using global::Common.Logging;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Utilities;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A logging command interceptor based on Common Logging for
    /// log all commands using your favourite logger ( Log4Net,NLog,EntiLib etc )
    /// </summary>
    public  class LogCommandInterceptor
        :DbCommandInterceptor
    {
        private readonly DbContext _context;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly ILog _log;

        /// <summary>
        /// Get the common logging reference
        /// </summary>
        public ILog Log
        {
            get
            {
                return _log;
            }
        }

        /// <summary>
        ///     Create a new Logging command interceptor based on Commonn.Logging
        ///     dependency.
        /// </summary>
        /// <param name="context">
        /// The context for which commands should be logged.
        ///  </param>
        public LogCommandInterceptor(DbContext context)
            :this()
        {
            Check.NotNull(context, "context");

            _context = context;
        }

        /// <summary>
        ///     Create a new Logging command interceptor based on Commonn.Logging
        ///     dependency.
        /// </summary>
        public LogCommandInterceptor()
        {
            _log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        ///     The context for which commands are being logged, or null if commands from all contexts are
        ///     being logged.
        /// </summary>
        public DbContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// The stop watch used to time executions. This stop watch is started at the end of
        /// <see cref="NonQueryExecuting" />, <see cref="ScalarExecuting" />, and <see cref="ReaderExecuting" />
        /// methods and is stopped at the beginning of the <see cref="NonQueryExecuted" />, <see cref="ScalarExecuted" />,
        /// and <see cref="ReaderExecuted" /> methods. If these methods are overridden and the stop watch is being used
        /// then the overrides should either call the base method or start/stop the watch themselves.
        /// </summary>
        protected internal Stopwatch Stopwatch
        {
            get { return _stopwatch; }
        }

        /// <summary>
        ///     This method is called before a call to <see cref="DbCommand.ExecuteNonQuery" /> or
        ///     one of its async counterparts is made.
        ///     The default implementation calls <see cref="Executing" />
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            Executing(command, interceptionContext);
            Stopwatch.Restart();
        }
       
        
        /// <summary>
        ///     This method is called after a call to <see cref="DbCommand.ExecuteNonQuery" />  or
        ///     one of its async counterparts is made.
        ///     The default implementation calls <see cref="Executed" />
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            Stopwatch.Stop();
            Executed(command, interceptionContext);
        }
        

        /// <summary>
        ///     This method is called before a call to <see cref="DbCommand.ExecuteReader(CommandBehavior)" />  or
        ///     one of its async counterparts is made.
        ///     The default implementation calls <see cref="Executing" />
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public  override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            Executing(command, interceptionContext);
            Stopwatch.Restart();
        }

        /// <summary>
        ///     This method is called after a call to <see cref="DbCommand.ExecuteReader(CommandBehavior)" />  or
        ///     one of its async counterparts is made.
        ///     The default implementation calls <see cref="Executed" />
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override  void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            Stopwatch.Stop();
            Executed(command, interceptionContext);
            
        }

        /// <summary>
        ///     This method is called before a call to <see cref="DbCommand.ExecuteScalar" />  or
        ///     one of its async counterparts is made.
        ///     The default implementation calls <see cref="Executing" />
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            Executing(command, interceptionContext);
            Stopwatch.Restart();
        }
        
        /// <summary>
        ///     This method is called after a call to <see cref="DbCommand.ExecuteScalar" />  or
        ///     one of its async counterparts is made.
        ///     The default implementation calls <see cref="Executed" />
        /// </summary>
        /// <param name="command">The command being executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the call.</param>
        /// <returns>The result to be used by Entity Framework.</returns>
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            Stopwatch.Stop();
            Executed(command, interceptionContext);
        }


        /// <summary>
        /// Called whenever a command is about to be executed. The default implementation of this method
        /// filters by <see cref="DbContext" /> set into <see cref="Context" />, if any, and then calls
        /// <see cref="LogCommand" />. This method would typically only be overridden to change the
        /// context filtering behavior.
        /// </summary>
        /// <param name="command">The command that will be executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the command.</param>
        public virtual void Executing<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            if (Context == null
                || interceptionContext.DbContexts.Contains(Context, ReferenceEquals))
            {
                if (_log.IsInfoEnabled)
                {
                    LogCommand(command, interceptionContext);
                }
            }
        }

        /// <summary>
        /// Called whenever a command has completed executing. The default implementation of this method
        /// filters by <see cref="DbContext" /> set into <see cref="Context" />, if any, and then calls
        /// <see cref="LogResult" />. This method would typically only be overridden to change the context
        /// filtering behavior.
        /// </summary>
        /// <param name="command">The command that was executed.</param>
        /// <param name="interceptionContext">Contextual information associated with the command.</param>
        public virtual void Executed<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            if (Context == null
                || interceptionContext.DbContexts.Contains(Context, ReferenceEquals))
            {
                if (_log.IsInfoEnabled)
                {
                    LogResult(command, interceptionContext);
                }
            }
        }

        /// <summary>
        ///     Called to log a command that is about to be executed. Override this method to change how the
        ///     command is logged into ILog.
        /// </summary>
        /// <param name="command">The command to be logged.</param>
        /// <param name="interceptionContext">Contextual information associated with the command.</param>
        public virtual void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            using (var writer = new StringWriter())
            {
                if (command.CommandText.EndsWith(Environment.NewLine, StringComparison.Ordinal))
                {
                    writer.Write(command.CommandText);
                }
                else
                {
                    writer.WriteLine(command.CommandText);
                }

                foreach (var parameter in command.Parameters.OfType<DbParameter>())
                {
                    LogParameter(command, interceptionContext, parameter);
                }

                if (interceptionContext.IsAsync)
                {
                    writer.WriteLine(Strings.CommandLogAsync);
                }

                _log.Info(writer.ToString());
            }
        }

        /// <summary>
        /// Called by <see cref="LogCommand" /> to log each parameter. This method can be called from an overridden
        /// implementation of <see cref="LogCommand" /> to log parameters, and/or can be overridden to
        /// change the way that parameters are logged.
        /// </summary>
        /// <param name="command">The command being logged.</param>
        /// <param name="interceptionContext">Contextual information associated with the command.</param>
        /// <param name="parameter">The parameter to log.</param>
        public virtual void LogParameter<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext, DbParameter parameter)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");
            Check.NotNull(parameter, "parameter");

            // -- Name: [Value] (Type = {}, Direction = {}, IsNullable = {}, Size = {}, Precision = {} Scale = {})
            var builder = new StringBuilder();
            builder.Append("-- ")
                .Append(parameter.ParameterName)
                .Append(": '")
                .Append((parameter.Value == null || parameter.Value == DBNull.Value) ? "null" : parameter.Value)
                .Append("' (Type = ")
                .Append(parameter.DbType);

            if (parameter.Direction != ParameterDirection.Input)
            {
                builder.Append(", Direction = ").Append(parameter.Direction);
            }

            if (!parameter.IsNullable)
            {
                builder.Append(", IsNullable = false");
            }

            if (parameter.Size != 0)
            {
                builder.Append(", Size = ").Append(parameter.Size);
            }

            if (((IDbDataParameter)parameter).Precision != 0)
            {
                builder.Append(", Precision = ").Append(((IDbDataParameter)parameter).Precision);
            }

            if (((IDbDataParameter)parameter).Scale != 0)
            {
                builder.Append(", Scale = ").Append(((IDbDataParameter)parameter).Scale);
            }

            builder.Append(")").Append(Environment.NewLine);

            _log.Info(builder.ToString());
        }

        /// <summary>
        /// Called to log the result of executing a command. Override this method to change how results are
        /// logged to <see cref="WriteAction" />.
        /// </summary>
        /// <param name="command">The command being logged.</param>
        /// <param name="interceptionContext">Contextual information associated with the command.</param>
        public virtual void LogResult<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            Check.NotNull(command, "command");
            Check.NotNull(interceptionContext, "interceptionContext");

            if (interceptionContext.Exception != null)
            {
                _log.Info(Strings.CommandLogFailed(
                    Stopwatch.ElapsedMilliseconds, interceptionContext.Exception.Message, Environment.NewLine));
            }
            else if (interceptionContext.TaskStatus.HasFlag(TaskStatus.Canceled))
            {
                _log.Info(Strings.CommandLogCanceled(Stopwatch.ElapsedMilliseconds, Environment.NewLine));
            }
            else
            {
                var result = interceptionContext.Result;
                var resultString = (object)result == null
                                       ? "null"
                                       : (result is DbDataReader)
                                             ? result.GetType().Name
                                             : result.ToString();
                _log.Info(Strings.CommandLogComplete(Stopwatch.ElapsedMilliseconds, resultString, Environment.NewLine));
            }

            _log.Info(Environment.NewLine);
        }

    }
}
