using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using StonksCasino.Views.main;

namespace StonksCasino.classes.Main
{
   public class Database
    {
        static SqlConnection _connection = new SqlConnection("Server = tcp:summacasino.database.windows.net, 1433; Initial Catalog = Casino; Persist Security Info=False;user ID = Summacollegeproject3; Password=Casinoproject3;");
        static string _username;

        public string MyUsername
        {
            get { return _username; }
            set { _username = value; }
        }

        static string _password;

        static string MyPassword
        {
            get { return _password; }
            set { _password = value; }
        }
        public static DataTable Accounts()
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
            
                string query = "SELECT * FROM Accounts WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username);
                sqlCmd.Parameters.AddWithValue("@Password", MyPassword);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                result.Load(reader);
            }
            catch (Exception)
            {
                //Problem with the databas
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public static DataTable Tokensadd( int value)
        {
            DataTable dataTable = Accounts();
            int HuidigeTokens = (int)dataTable.Rows[0]["Token"];
            int tokens = HuidigeTokens + value;
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                string query = "UPDATE Accounts SET Token = " + tokens + " WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username);
                sqlCmd.Parameters.AddWithValue("@Password", MyPassword);
                sqlCmd.ExecuteReader();
            }
            catch (Exception)
            {
                //Problem with the databas
            }
            finally
            {
                _connection.Close();
            }
            return result;
            
        }
        public static DataTable Tokensremove(int value)
        {
            DataTable dataTable = Accounts();
            int HuidigeTokens = (int)dataTable.Rows[0]["Token"];
            int tokens = HuidigeTokens - value;
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                 string query = "UPDATE Accounts SET Token = " + tokens + " WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username );
                sqlCmd.Parameters.AddWithValue("@Password", MyPassword);
                sqlCmd.ExecuteReader();
            }
            catch (Exception)
            {
                //Problem with the databas
            }
            finally
            {
                _connection.Close();
            }
            return result;

        }



        public bool Login(string pass)
        {
            DataTable result = new DataTable();
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                String query = "SELECT COUNT(1) FROM Accounts WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", MyUsername );
                sqlCmd.Parameters.AddWithValue("@Password",  pass);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

         
                if (count == 1)
                {
                    _connection.Close();
                    MyPassword = pass;
                    LibraryWindow library = new LibraryWindow();                                       
                    library.Show();
                    return true;
                    
                }
                else
                {
                    MessageBox.Show("Gebruikersnaam of wachtwoord is onjuist.");
                    return false;
                }
            }
            catch 
            {
                MessageBox.Show("Gebruikersnaam of wachtwoord is onjuist.");
                return false;
            }
            finally
            {
                _connection.Close();
            }
       
        }

    }
}
