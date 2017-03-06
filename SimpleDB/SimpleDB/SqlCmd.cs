using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimpleSql
{
    public static class SqlCmd
    {
        // <summary>Add the parameter collection to the SqlCommand object</summary>
        // <param name="cmd" type="SqlCommand">The reference of SqlCommand object to mutate</param>
        // <param name="pc" type="SqlParameterCollection">The reference of SqlParameterCollection object to inject</param>
        private static void AddCollection(ref SqlCommand cmd, ref SqlParameterCollection pc)
        {
            foreach (SqlParameter p in pc)
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value).SqlDbType = p.SqlDbType;
        }

        // <summary>Add the parameter collection to the SqlCommand object</summary>
        // <param name="cmd" type="SqlCommand">The reference of SqlCommand object to mutate</param>
        // <param name="lp" type="List<SqlParameter>">The reference of List<SqlParameter> object to inject</param>
        private static void AddCollection(ref SqlCommand cmd, ref List<SqlParameter> lp)
        {
            foreach (SqlParameter p in lp)
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value).SqlDbType = p.SqlDbType;
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText)
        {
            return new SqlCommand(CommandText); // Return new SqlCommand object to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection)
        {
            return new SqlCommand(CommandText, Connection); // Return new SqlCommand object using Connection to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString)); // Return new SqlCommand object using ConnectionString to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)); // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))); // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))); // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, CommandType Type)
        {
            return new SqlCommand(CommandText) { CommandType = Type }; // Return new SqlCommand object with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, CommandType Type)
        {
            return new SqlCommand(CommandText, Connection) { CommandType = Type }; // Return new SqlCommand object using Connection with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, CommandType Type)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandType = Type }; // Return new SqlCommand object using ConnectionString with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, CommandType Type)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandType = Type }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, CommandType Type)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandType = Type }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, CommandType Type)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandType = Type }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, CommandType Type, int Timeout)
        {
            return new SqlCommand(CommandText) { CommandType = Type, CommandTimeout = Timeout }; // Return new SqlCommand object with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, CommandType Type, int Timeout)
        {
            return new SqlCommand(CommandText, Connection) { CommandType = Type, CommandTimeout = Timeout }; // Return new SqlCommand object using Connection with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, CommandType Type, int Timeout)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandType = Type, CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, CommandType Type, int Timeout)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandType = Type, CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString and Credentials with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, CommandType Type, int Timeout)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandType = Type, CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString and Credentials with specified CommandType to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, CommandType Type, int Timeout)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandType = Type, CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, int Timeout)
        {
            return new SqlCommand(CommandText) { CommandTimeout = Timeout }; // Return new SqlCommand object to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, int Timeout)
        {
            return new SqlCommand(CommandText, Connection) { CommandTimeout = Timeout }; // Return new SqlCommand object using Connection to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, int Timeout)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, int Timeout)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, int Timeout)
        {
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, int Timeout)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            return new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandTimeout = Timeout }; // Return new SqlCommand object using ConnectionString and Credentials to execute CommandText
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, CommandType Type, int Timeout, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, CommandType Type, int Timeout, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, Connection) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, CommandType Type, int Timeout, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, CommandType Type, int Timeout, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, CommandType Type, int Timeout, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, CommandType Type, int Timeout, SqlParameterCollection Params)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, CommandType Type, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, CommandType Type, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, Connection) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, CommandType Type, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, CommandType Type, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, CommandType Type, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, CommandType Type, SqlParameterCollection Params)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText);
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, Connection);
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, SqlParameterCollection Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password)));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, SqlParameterCollection Params)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext)));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, CommandType Type, int Timeout, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="SqlParameterCollection">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, CommandType Type, int Timeout, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, Connection) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, CommandType Type, int Timeout, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, CommandType Type, int Timeout, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, CommandType Type, int Timeout, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Timeout" type="int">The number of second until timing out to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, CommandType Type, int Timeout, List<SqlParameter> Params)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandType = Type, CommandTimeout = Timeout };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, CommandType Type, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, CommandType Type, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, Connection) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, CommandType Type, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString)) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, CommandType Type, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials)) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, CommandType Type, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password))) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Type" type="CommandType">The CommandType object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, CommandType Type, List<SqlParameter> Params)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext))) { CommandType = Type };
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText);
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="Connection" type="SqlConnection">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, SqlConnection Connection, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, Connection);
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The SqlConnection object to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Credentials" type="SqlCredentials">The credentials to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, SqlCredential Credentials, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, Credentials));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="SecureString">The password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, System.Security.SecureString Password, List<SqlParameter> Params)
        {
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Password)));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }

        // <summary>Build a new SqlCommand object</summary>
        // <param name="CommandText" type="string">The text of the command to execute in the new SqlCommand object</param>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlCommand object</param>
        // <param name="Username" type="string">The username of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Password" type="string">The plaintext password of the credentials to utilize in the new SqlCommand object</param>
        // <param name="Params" type="List<SqlParameter>">The collection of SqlParameters to utilize in the new SqlCommand object</param>
        // <return>SqlCommand</return>
        public static SqlCommand New(string CommandText, string ConnectionString, string Username, string Password, List<SqlParameter> Params)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Create SqlCommand object
            var Command = new SqlCommand(CommandText, new SqlConnection(ConnectionString, new SqlCredential(Username, Ciphertext)));
            AddCollection(ref Command, ref Params); // Mutate the SqlCommand object with the parameter collection
            return Command; // Return new SqlCommand object
        }
    }
}
