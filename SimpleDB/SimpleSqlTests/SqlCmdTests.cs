using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSql;

namespace SimpleSqlTests
{
    [TestClass]
    public class SqlCmdTests
    {
        // Create fake connection strings and credentials to test with
        const string ConStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=DefaultDatabase;Integrated Security=True;MultipleActiveResultSets=True";
        const string ConStr2 = @"Data Source=ServerName;Initial Catalog=DatabaseName;Integrated Security=False;MultipleActiveResultSets=True";
        const string Command = "SELECT * FROM [dbo].[table] WHERE [Id] = 1;";
        const string Command2 = "DECLARE @Id INT; EXEC [dbo].[SPROC] 'Input', @Id OUTPUT;";
        const string Command3 = "[dbo].[Another_SPROC]";
        const string UsrNm = "john.smith";
        const string Pwd = "password";

        // Create SqlConnection object to test with
        SqlConnection con = new SqlConnection(ConStr);
        SqlConnection con2 = new SqlConnection(ConStr2);

        [TestMethod]
        public void NewCommandValidInput()
        {
            // Build test variables for later
            var Con = new SqlConnection(ConStr);
            var Con2 = new SqlConnection(ConStr2);
            var Params = new SqlCommand().Parameters;
            var Params2 = new List<SqlParameter>();
            SqlTransaction Trans;
            for (int i = 0; i < 3; i++)
            {
                Params.AddWithValue('@' + i.ToString(), i);
                Params2.Add(new SqlParameter('@' + i.ToString(), i + 1));
            }
            // Create fake credentials to test with
            var Cipher = new System.Security.SecureString();
            foreach (char c in Pwd)
                Cipher.AppendChar(c);
            Cipher.MakeReadOnly();
            var Cred = new SqlCredential(UsrNm, Cipher);

            // Test1 - CommandText
            var Cmd = SqlCmd.New(Command);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.IsNull(Cmd.Connection);
            Cmd = null;

            // Test2 - CommandText, CommandType
            Cmd = SqlCmd.New(Command, CommandType.Text);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.AreEqual(CommandType.Text, Cmd.CommandType);
            Assert.IsNull(Cmd.Connection);
            Cmd = null;

            // Test3 - CommandText, Timeout
            Cmd = SqlCmd.New(Command, 3);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.AreEqual(3, Cmd.CommandTimeout);
            Assert.IsNull(Cmd.Connection);
            Cmd = null;

            // Test4 - CommandText, SqlConnection
            Cmd = SqlCmd.New(Command, Con);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.IsNotNull(Cmd.Connection);
            Assert.AreEqual(Con, Cmd.Connection);
            Cmd = null;

            // Test5 - CommandText, SqlParameterCollection
            Cmd = SqlCmd.New(Command3, Params);
            Assert.AreEqual(Command3, Cmd.CommandText);
            Assert.IsNotNull(Cmd.Parameters);
            for (int i = 0, j = Cmd.Parameters.Count; i < j; i++)
            {
                Assert.AreEqual(Params[i].ParameterName, Cmd.Parameters[i].ParameterName);
                Assert.AreEqual(Params[i].Value, Cmd.Parameters[i].Value);
            }
            Cmd = null;

            // Test6 - CommandText, ConnectionString
            Cmd = SqlCmd.New(Command, ConStr);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.IsNotNull(Cmd.Connection);
            Assert.AreEqual(ConStr, Cmd.Connection.ConnectionString);
            Cmd = null;

            // Test7 - CommandText, List<SqlParameter>
            Cmd = SqlCmd.New(Command3, Params2);
            Assert.AreEqual(Command3, Cmd.CommandText);
            for (int i = 0, j = Cmd.Parameters.Count; i < j; i++)
            {
                Assert.AreEqual(Params2[i].ParameterName, Cmd.Parameters[i].ParameterName);
                Assert.AreEqual(Params2[i].Value, Cmd.Parameters[i].Value);
            }

            // Skipping 16

            // Test20 - CommandText, ConnectionString, SqlCredential
            Cmd = SqlCmd.New(Command2, ConStr2, Cred);
            Assert.AreEqual(Command2, Cmd.CommandText);
            Assert.IsNotNull(Cmd.Connection);
            Assert.IsNotNull(Cmd.Connection.Credential);
            Assert.AreEqual(ConStr2, Cmd.Connection.ConnectionString);
            Assert.AreEqual(Cred, Cmd.Connection.Credential);
            Cmd = null;

            // Test24 - CommandText, CommandType, Timeout, SqlParameterCollection
            Cmd = SqlCmd.New(Command3, CommandType.StoredProcedure, 15, Params);
            Assert.AreEqual(Command3, Cmd.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, Cmd.CommandType);
            Assert.AreEqual(15, Cmd.CommandTimeout);
            for (int i = 0, j = Cmd.Parameters.Count; i < j; i++)
            {
                Assert.AreEqual(Params[i].ParameterName, Cmd.Parameters[i].ParameterName);
                Assert.AreEqual(Params[i].Value, Cmd.Parameters[i].Value);
            }
            Cmd = null;

            // Test49 - CommandText, ConnectionString, Username, Password
            Cmd = SqlCmd.New(Command, ConStr2, UsrNm, Pwd);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.IsNotNull(Cmd.Connection);
            Assert.IsNotNull(Cmd.Connection.Credential);
            Assert.AreEqual(ConStr2, Cmd.Connection.ConnectionString);
            Assert.AreEqual(UsrNm, Cmd.Connection.Credential.UserId);
            Assert.AreEqual(8, Cmd.Connection.Credential.Password.Length);
            Cmd = null;

            // Test87 - CommandText, ConnectionString, SqlCredentail, CommandType, Timeout, List<SqlParameter>
            Cmd = SqlCmd.New(Command, ConStr2, Cred, CommandType.Text, 30, Params2);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.AreEqual(CommandType.Text, Cmd.CommandType);
            Assert.IsNotNull(Cmd.Connection);
            Assert.IsNotNull(Cmd.Connection.Credential);
            Assert.AreEqual(ConStr2, Cmd.Connection.ConnectionString);
            Assert.AreEqual(Cred, Cmd.Connection.Credential);
            Assert.AreEqual(30, Cmd.CommandTimeout);
            for (int i = 0, j = Cmd.Parameters.Count; i < j; i++)
            {
                Assert.AreEqual(Params2[i].ParameterName, Cmd.Parameters[i].ParameterName);
                Assert.AreEqual(Params2[i].Value, Cmd.Parameters[i].Value);
            }
            Cmd = null;

            // Test116 - CommandText, ConnectionString, Username, Password, CommandType, Timeout, List<SqlParameter>
            Cmd = SqlCmd.New(Command, ConStr2, UsrNm, Pwd, CommandType.Text, 120, Params2);
            Assert.AreEqual(Command, Cmd.CommandText);
            Assert.AreEqual(CommandType.Text, Cmd.CommandType);
            Assert.IsNotNull(Cmd.Connection);
            Assert.IsNotNull(Cmd.Connection.Credential);
            Assert.AreEqual(ConStr2, Cmd.Connection.ConnectionString);
            Assert.AreEqual(UsrNm, Cmd.Connection.Credential.UserId);
            Assert.AreEqual(8, Cmd.Connection.Credential.Password.Length);
            Assert.AreEqual(120, Cmd.CommandTimeout);
            for (int i = 0, j = Cmd.Parameters.Count; i < j; i++)
            {
                Assert.AreEqual(Params2[i].ParameterName, Cmd.Parameters[i].ParameterName);
                Assert.AreEqual(Params2[i].Value, Cmd.Parameters[i].Value);
            }
            Cmd = null;

            // Skiping 131
        }

        [TestMethod]
        public void NewCommandInvalidInput()
        {
            // Not Implemented Yet
        }
    }
}
