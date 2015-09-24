using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualPlay.SMTP.Email
{
    public class Log
    {
        public static string ConnectionStrings { get; set; }
        public static string Schema { get; set; }

        public static void Insert(int idWebmail, int idSequence, string mailTo, string errorMessage, string subject, string body, bool sendSucess, string extra, int idSystem, int idUserCreate, string cdTemplate, string cdIdentification1, string cdIdentification2, string cdIdentification3)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            connection = new System.Data.SqlClient.SqlConnection(ConnectionStrings);

            command = new System.Data.SqlClient.SqlCommand(Schema + ".Sys_WebmailLogInsert", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@idWebmail", System.Data.SqlDbType.Int).Value = idWebmail;
            command.Parameters.Add("@idSequence", System.Data.SqlDbType.Int).Value = idSequence;
            command.Parameters.Add("@idSystem", System.Data.SqlDbType.Int).Value = idSystem;
            command.Parameters.Add("@dsMailTo", System.Data.SqlDbType.VarChar, 500).Value = mailTo;
            command.Parameters.Add("@dsError", System.Data.SqlDbType.VarChar, 4500).Value = errorMessage;
            command.Parameters.Add("@dsSubject", System.Data.SqlDbType.VarChar, 500).Value = subject;
            command.Parameters.Add("@dsMessage", System.Data.SqlDbType.VarChar, 4500).Value = body;
            command.Parameters.Add("@dsExtra", System.Data.SqlDbType.VarChar, 4500).Value = extra;
            command.Parameters.Add("@cdTemplate", System.Data.SqlDbType.VarChar, 100).Value = cdTemplate;
            command.Parameters.Add("@cdIdentification1", System.Data.SqlDbType.VarChar, 100).Value = cdIdentification1;
            command.Parameters.Add("@cdIdentification2", System.Data.SqlDbType.VarChar, 100).Value = cdIdentification2;
            command.Parameters.Add("@cdIdentification3", System.Data.SqlDbType.VarChar, 100).Value = cdIdentification3;
            command.Parameters.Add("@flSent", System.Data.SqlDbType.Bit).Value = sendSucess;
            command.Parameters.Add("@idUserCreate", System.Data.SqlDbType.Int).Value = idUserCreate;

            connection.Open();
            command.ExecuteScalar();
        }
    }
}
