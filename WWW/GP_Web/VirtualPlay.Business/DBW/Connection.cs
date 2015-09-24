using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace VirtualPlay.Business.DBW
{
    public class Database<DbType>
       where DbType : DbConnection, ICloneable, new()
    {
        private readonly string ConnectionString;

        public Database(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("ConnectionString");
            this.ConnectionString = ConnectionString;
        }

        protected DbType Open()
        {
            var conn = new DbType();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            return conn;
        }

        protected bool Close(DbType connection)
        {
            if (connection != null &&
                connection.State == System.Data.ConnectionState.Open)
                connection.Close();
            return false;
        }

        public void QueryReader(string sql, Action<IDataReader> wrapentity)
        {
            if (wrapentity == null)
                throw new ArgumentNullException("wrapentity");

            using (var connection = Open())
            {
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = sql;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.CommandTimeout = 99999;

                    using (var reader = comm.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader != null && reader.HasRows)
                            while (reader.Read())
                                wrapentity(reader);
                    }
                }
            }
        }

        public int ExecuteCommand(string sql)
        {
            using (var connection = Open())
            {
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = sql;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.CommandTimeout = 99999;

                    return comm.ExecuteNonQuery();
                }
            }
        }

        protected int ExecuteScalar(string sql)
        {
            using (var connection = Open())
            {
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = sql;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.CommandTimeout = 99999;

                    var ret = comm.ExecuteScalar();
                    return ret is Int32 ? Convert.ToInt32(ret) : 0;
                }
            }
        }

    }
}
