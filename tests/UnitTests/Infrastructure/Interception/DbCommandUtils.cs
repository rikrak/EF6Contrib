namespace System.Data.Entity.Infrastructure.Interception
{
    using Moq;
    using Moq.Protected;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;


    static class DbCommandUtils
    {
        public static DbCommand CreateCommand(string commandText, params DbParameter[] parameters)
        {
            var mockParameters = new Mock<DbParameterCollection>();
            mockParameters.As<IEnumerable>().Setup(m => m.GetEnumerator()).Returns(parameters.GetEnumerator());

            var mockCommand = new Mock<DbCommand>();
            mockCommand.Setup(m => m.CommandText).Returns(commandText);
            

            mockCommand.Protected().Setup<DbParameterCollection>("DbParameterCollection").Returns(mockParameters.Object);

            return mockCommand.Object;
        }

        public static DbParameter CreateParameter(
            string name,
            ParameterDirection direction,
            bool isNullable,
            DbType type,
            int size,
            byte precision,
            byte scale,
            object value)
        {
            var parameter = new Mock<DbParameter>();
            parameter.Setup(m => m.ParameterName).Returns(name);
            parameter.Setup(m => m.Direction).Returns(direction);
            parameter.Setup(m => m.IsNullable).Returns(isNullable);
            parameter.Setup(m => m.DbType).Returns(type);
            parameter.Setup(m => m.Size).Returns(size);
            parameter.As<IDbDataParameter>().Setup(m => m.Precision).Returns(precision);
            parameter.As<IDbDataParameter>().Setup(m => m.Scale).Returns(scale);
            parameter.Setup(m => m.Value).Returns(value);

            return parameter.Object;
        }
    }
}
