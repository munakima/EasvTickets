using Elements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using sqlDbType = System.Data.SqlDbType;

namespace DAL.Core
{
    internal static class SqlHelper
    {
        #region constants and global variables
        public const string GetLastModifiedTableQuery = " SELECT CAST(scope_identity() AS int)";

        private const string _identifier = "@";

        /// <summary>
        /// Maps the Type to a sqlDbType
        /// </summary>
        private static Dictionary<Type, sqlDbType> _typeDictionary = new Dictionary<Type, sqlDbType>()
        {
            { typeof(int),  sqlDbType.Int},
            { typeof(string), sqlDbType.NVarChar },
            { typeof(decimal), sqlDbType.Decimal },
            { typeof(DateTime), sqlDbType.DateTime },
            { typeof(bool), sqlDbType.Bit }
        };


        /// <summary>
        /// Maps the input type to SqlDataReader on a given position 
        /// </summary>
        private static Dictionary<Type, Func<IDataReader, int, object>> _readerMethodDictionary = new Dictionary<Type, Func<IDataReader, int, object>>()
        {
            { typeof(int), (sqlReader, position) => { return sqlReader.GetInt32(position); }},
            { typeof(string), (sqlReader, position) => { return sqlReader.GetString(position); }},
            { typeof(decimal), (sqlReader, position) => { return sqlReader.GetDecimal(position); }},
            { typeof(DateTime), (sqlReader, position) => { return sqlReader.GetDateTime(position); }},
            { typeof(bool), (sqlReader, position) => { return sqlReader.GetBoolean(position); }}
        };
        #endregion

        #region Builders

        public static void UseSqlConnection(Action<SqlConnection> action)
        {
            var sqlConnection = new SqlConnection(Constants.ConnectionString);
            sqlConnection.Open();
            action(sqlConnection);
            sqlConnection.Close();
        }

        /// <summary>
        /// Creates a SqlCommand with the given <paramref name="query"/> on
        /// the given sql <paramref name="connection"/>.
        /// The @ID tag in <paramref name="query"/> 
        /// is replaced with the actual value from <paramref name="id"/> 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <param name="connection"></param>
        /// <returns><typeparamref name="SqlCommand"/> with ready query</returns>
        public static SqlCommand IdToSqlCommandBuilder(int id, string query, SqlConnection connection)
        {
            if (id < 1 || string.IsNullOrWhiteSpace(query) || connection == null) throw new ArgumentNullException();
            var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add($"{_identifier}Id", sqlDbType.Int).Value = id;
            return sqlCommand;
        }

        /// <summary>
        /// Creates a SqlCommand with the given <paramref name="query"/> on
        /// the given sql <paramref name="connection"/>.
        /// The tags corresponding to the Type T <paramref name="model"/> in <paramref name="query"/> 
        /// is replaced with the actual value from <paramref name="model"/> 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="query"></param>
        /// <param name="connection"></param>
        /// <returns><typeparamref name="SqlCommand"/> with ready query</returns>
        public static SqlCommand ModelToSqlCommandBuilder<T>(T model, string query, SqlConnection connection)
        {
            if (model == null || string.IsNullOrWhiteSpace(query) || connection == null) throw new ArgumentNullException();

            var sqlCommand = new SqlCommand(query, connection);
            var type = model.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                var value = type.GetProperty(name).GetValue(model);

                var sqlType = _typeDictionary[property.PropertyType];

                sqlCommand.Parameters.Add(_identifier + name, sqlType).Value = value;
            }
            return sqlCommand;
        }

        public static SqlCommand SqlCommandWithQueryBuilder(string query, SqlConnection connection)
        {
            return new SqlCommand(query, connection);
        }

        public static SqlCommand SqlCommandWithEventIdAndStudentId(string query, SqlConnection connection, int eventId, int studentId)
        {
            var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add($"{_identifier}EventId", sqlDbType.Int).Value = eventId;
            sqlCommand.Parameters.Add($"{_identifier}StudentId", sqlDbType.Int).Value = studentId;
            return sqlCommand;
        }


        /// <summary>
        /// Iterates through the results of <paramref name="sqlReader"/>
        /// and builds a new Type T <paramref name="T"/> with the actual
        /// values from the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlReader"></param>
        /// <returns></returns>
        public static T SqlReaderToModelBuilder<T>(IDataReader sqlReader)
            where T : new()
        {
            if(sqlReader == null) throw new ArgumentNullException();
            var model = new T();
            var properties = model.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var type = property.PropertyType;
                var name = property.Name;
                var value = _readerMethodDictionary[type].Invoke(sqlReader, i);
                property.SetValue(model, value);               
            }
            return model;
        }

        #endregion
        
        #region CRUD Queries
        /// <summary>
        /// Creates a query to get the specified Type T <paramref name="T"/>. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetQuery<T>()
        {
            var query = string.Format("SELECT * FROM [{0}] WHERE [{0}].Id = {1}Id", typeof(T).Name, _identifier).ToString();
            return query;
        }

        /// <summary>
        /// Creates a query to create the specified Type T <paramref name="T"/> with 
        /// all the properties of <paramref name="T"/>, but the ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string CreateQuery<T>()
        {
            var type = typeof(T);
            var name = type.Name;
            var properties = type.GetProperties();

            var sb = new System.Text.StringBuilder();
            sb.Append($"INSERT INTO [{name}] VALUES (");

            for (int i = 0; i < properties.Length; i++)
            {
                var propertyName = properties[i].Name;
                if (propertyName.ToUpper().Equals("Id".ToUpper())) continue;

                sb.Append(_identifier).Append($"{propertyName}");

                if (i != (properties.Length - 1))
                {
                    sb.Append(",");
                }
            }

            sb.Append(");");
            
            return sb.ToString(); ;
        }

        /// <summary>
        /// Creates a query to update the specified Type T <paramref name="T"/> with 
        /// all the properties of <paramref name="T"/>
        /// </summary>
        /// <returns></returns>
        public static string UpdateQuery<T>()
        {
            var type = typeof(T);
            var name = type.Name;
            var properties = type.GetProperties();
            
            var sb = new System.Text.StringBuilder($"UPDATE [{name}] SET ");

            for (int i = 0; i < properties.Length; i++)
            {
                var propertyName = properties[i].Name;
                if (propertyName.ToUpper().Equals("Id".ToUpper())) continue;
                sb.Append(propertyName).Append("=").Append(_identifier).Append(propertyName);
                if (i != (properties.Length - 1))
                {
                    sb.Append(",");
                }
            }

            sb.Append($" WHERE Id=@Id;");
            sb.Append($"SELECT * FROM [{name}] WHERE Id=@Id;");
            return sb.ToString();
        }

        /// <summary>
        /// Creates a query to delete the specified Id in the
        /// table corresponding to Type T <paramref name="T"/> 
        /// </summary>
        /// <returns></returns>
        public static string DeleteQuery<T>()
        {
            var name = typeof(T).Name;
            return $"DELETE FROM [{name}] WHERE Id=@Id";
        }
        #endregion

        #region Custom Queries
        public static string GetFutureEventsQuery()
        {
            return "SELECT * FROM [Event] WHERE[Event].Date >= GETDATE() ORDER BY [Event].Date ASC;";
        }

        public static string CanStudentBuyStudentTicketQuery()
        {
            return "SELECT CASE WHEN EXISTS (SELECT* FROM [Ticket] WHERE[Ticket].StudentId = @StudentId AND[Ticket].EventId = @EventId AND[Ticket].TicketTypeId = 1) THEN CAST (0 AS BIT) ELSE CAST (1 AS BIT) END";
        }
        #endregion
    }
}
