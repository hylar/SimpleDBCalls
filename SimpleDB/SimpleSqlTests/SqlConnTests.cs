using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSql;

namespace SimpleSqlTests
{
    [TestClass]
    public class SqlConnTests
    {
        // Create fake connection strings and credentials to test with
        const string ConStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=DefaultDatabase;Integrated Security=True;MultipleActiveResultSets=True";
        const string ConStr2 = @"Data Source=ServerName;Initial Catalog=DatabaseName;Integrated Security=False;MultipleActiveResultSets=True";
        const string UsrNm = "john.smith";
        const string Pwd = "password";

        [TestMethod]
        public void NewConectionValidInput()
        {
            // Test the creation of a basic SqlConnection object
            var Con = SqlConn.New(ConStr);
            Assert.AreEqual(ConStr, Con.ConnectionString);
            Assert.IsNull(Con.Credential);
            Con = null;

            // Create fake credentials to test with
            var Cipher = new System.Security.SecureString();
            foreach (char c in Pwd)
                Cipher.AppendChar(c);
            Cipher.MakeReadOnly();
            var Cred = new SqlCredential(UsrNm, Cipher);

            // Test the creation of a SqlConnection object with credentials
            Con = SqlConn.New(ConStr2, Cred);
            Assert.AreEqual(ConStr2, Con.ConnectionString);
            Assert.IsNotNull(Con.Credential);
            Assert.AreEqual(UsrNm, Con.Credential.UserId);
            Assert.AreEqual(Cipher, Con.Credential.Password);
            Con = null;

            // Test the creation of a SqlConnection object with Username and Secure Password
            Con = SqlConn.New(ConStr2, UsrNm, Cipher);
            Assert.AreEqual(ConStr2, Con.ConnectionString);
            Assert.IsNotNull(Con.Credential);
            Assert.AreEqual(UsrNm, Con.Credential.UserId);
            Assert.AreEqual(Cipher, Con.Credential.Password);
            Con = null;

            // Test the creation of a SqlConnection object with Username and Plaintext Password
            Con = SqlConn.New(ConStr2, UsrNm, Pwd);
            Assert.AreEqual(ConStr2, Con.ConnectionString);
            Assert.IsNotNull(Con.Credential);
            Assert.AreEqual(UsrNm, Con.Credential.UserId);
            Assert.AreNotEqual(Cipher, Con.Credential.Password);
        }

        [TestMethod]
        public void NewConectionInvalidInput()
        {
            // Create fake credentials to test with
            var Cipher = new System.Security.SecureString();
            foreach (char c in Pwd)
                Cipher.AppendChar(c);

            // Test the attempt to create without ReadOnly password
            try
            {
                SqlConn.New(ConStr2, UsrNm, Cipher);
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("password must be marked as read only.", ae.Message);
            }

            // Make ReadOnly and continue testing
            Cipher.MakeReadOnly();
            var Cred = new SqlCredential(UsrNm, Cipher);

            // Test the attempt to create with IntegratedSecurity = True and Credentials
            try
            {
                SqlConn.New(ConStr, Cred);
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("Cannot use Credential with Integrated Security connection string keyword.", ae.Message);
            }

            // Test the attempt to create with null Credentials
            try
            {
                SqlConn.New(ConStr2, null, "");
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: userId", ae.Message);
            }
            try
            {
                SqlConn.New(ConStr2, null, new System.Security.SecureString());
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: userId", ae.Message);
            }
        }
    }
}
