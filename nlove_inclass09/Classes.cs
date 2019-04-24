using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace nlove_inclass09

    // usually each class is created in a separate file and called same as class
{
    public class EmptyPasswordException : Exception
    {
        private static string message = "Password cannot be empty.";
        public EmptyPasswordException() : base(message) { }
    }

    public class ConnectionException : Exception
    {
        private static string message = "Issue with the database connection: ";
        public ConnectionException(string _msg) : base(message + _msg) { }
    }

    public class Connection
    {
        private readonly bool _userExists;

        public Connection(string u, string p)
        {
            this._userExists = false;

            SqlConnection conn = new SqlConnection("Data Source=184.168.194.55;" +
                    "Initial Catalog=classroom;" +
                    "Persist Security Info=True;" +
                    "User ID=profmorris;" +
                    "Password=Lec2g#08");

            try
            {
                conn.Open();

                string query = "SELECT COUNT(*) " +
                    "FROM persons " +
                    "WHERE Person_UName = @username " +
                    "AND Person_MD5 = @password ";

                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@username", u);
                comm.Parameters.AddWithValue("@password", p);

                this._userExists = ((int)comm.ExecuteScalar() > 0);
            }
            catch (Exception ex)
            {
                throw (new ConnectionException(ex.Message));
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UserExists
        {
            get { return _userExists; }
        }
    }

    public class PasswordHash
    {
        private string _input;
        private byte[] _inputBytes;
        private byte[] _hashBytes;
        private readonly string _hash;

        public PasswordHash(string input)
        {
            this.Input = input;
            this._inputBytes = Encoding.ASCII.GetBytes(this._input);
            this._hashBytes = GetHashBytes(this._inputBytes);
            this._hash = ConvertHashToString(this._hashBytes);
        }

        public string Input
        {
            get { return this._input; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw (new EmptyPasswordException());
                }
                else
                {
                    this._input = value;
                }
            }
        }


        public string Hash
        {
            get { return this._hash; }
        }

        private byte[] GetHashBytes(byte[] inputBytes)
        {
            byte[] hb;
            MD5Cng m = new MD5Cng();
            hb = m.ComputeHash(inputBytes);
            return hb;
        }

        private string ConvertHashToString(byte[] hashBytes)
        {
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }
    }


}