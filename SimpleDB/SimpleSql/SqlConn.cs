using System.Data.SqlClient;

namespace SimpleSql
{
    public static class SqlConn
    {
        // <summary>Build a new SqlConnection object</summary>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlConnection object</param>
        // <return>SqlConnection</return>
        public static SqlConnection New(string ConnectionString)
        {
            return new SqlConnection(ConnectionString); // Return new SqlConnection using ConnectionString
        }

        // <summary>Build a new SqlConnection object</summary>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlConnection object</param>
        // <param name="Credential" type="SqlCredential">The credentials to utilize in the new SqlConnection object</param>
        // <return>SqlConnection</return>
        public static SqlConnection New(string ConnectionString, SqlCredential Credential)
        {
            return new SqlConnection(ConnectionString, Credential); // Return new SqlConnection using ConnectionString and Credential
        }

        // <summary>Build a new SqlConnection object</summary>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlConnection object</param>
        // <param name="Username" type="string">The username of credentails to utilize in the new SqlConnection object</param>
        // <param name="Password" type="SecureString">The ciphertext password of credentials to utilize in the new SqlConnection object</param>
        // <return>SqlConnection</return>
        public static SqlConnection New(string ConnectionString, string Username, System.Security.SecureString Password)
        {
            // Make a SqlCredential using the provided Username and ciphertext Password
            var Credential = new SqlCredential(Username, Password);
            return new SqlConnection(ConnectionString, Credential); // Return new SqlConnection using ConnectionString and Credential
        }

        // <summary>Build a new SqlConnection object</summary>
        // <param name="ConnectionString" type="string">The connection string to utilize in the new SqlConnection object</param>
        // <param name="Username" type="string">The username of credentails to utilize in the new SqlConnection object</param>
        // <param name="Password" type="string">The plaintext password of credentials to utilize in the new SqlConnection object</param>
        // <return>SqlConnection</return>
        public static SqlConnection New(string ConnectionString, string Username, string Password)
        {
            // Convert the plaintext Password in to a SecureString object
            var Ciphertext = new System.Security.SecureString();
            foreach (char c in Password)
                Ciphertext.AppendChar(c);
            Ciphertext.MakeReadOnly();
            // Make a SqlCredential using the provided Username and ciphertext Password
            var Credential = new SqlCredential(Username, Ciphertext);
            return new SqlConnection(ConnectionString, Credential); // Return new SqlConnection using ConnectionString and Credential
        }
    }
}
