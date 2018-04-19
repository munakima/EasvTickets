namespace DAL.Core
{
    internal static class CRUDRepositoryHelper
    {
        public static T Get<T>(int id)
            where T : new()
        {
            var result = default(T);
            SqlHelper.UseSqlConnection((sqlConnection) =>
            {
                var sqlCommand = SqlHelper.IdToSqlCommandBuilder(id, SqlHelper.GetQuery<T>(), sqlConnection);

                var sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    result = SqlHelper.SqlReaderToModelBuilder<T>(sqlReader);
                }
                sqlReader.Close();
            });
            return result;
        }

        public static int Create<T>(T model)
        {
            var result = default(int);
            SqlHelper.UseSqlConnection((sqlConnection) =>
            {
                var query = SqlHelper.CreateQuery<T>() + SqlHelper.GetLastModifiedTableQuery;
                var sqlCommand = SqlHelper.ModelToSqlCommandBuilder<T>(model, query, sqlConnection);
                result = (int)sqlCommand.ExecuteScalar();
            });
            return result;
        }

        public static T Update<T>(T model)
            where T : new()
        {
            var result = default(T);
            SqlHelper.UseSqlConnection((sqlConnection) =>
            {
                var query = SqlHelper.UpdateQuery<T>();
                var sqlCommand = SqlHelper.ModelToSqlCommandBuilder<T>(model, query, sqlConnection);
                var sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    result = SqlHelper.SqlReaderToModelBuilder<T>(sqlReader);
                }
                sqlReader.Close();
            });
            return result;
        }

        public static bool Delete<T>(int id)
        {
            var result = false;
            SqlHelper.UseSqlConnection((sqlConnection) =>
            {
                var query = SqlHelper.DeleteQuery<T>();
                var sqlCommand = SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection);
                var sqlReader = (int)sqlCommand.ExecuteNonQuery();
                result = sqlReader != 0;
            });
            return result;
        }
    }
}
