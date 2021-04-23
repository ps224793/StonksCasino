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

        public string MyPassword
        {
            get { return _password; }
            set { _password = value; }
        }

        private bool remember = false;

        public bool MyRemember
        {
            get { return remember; }
            set { remember = value; } 
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
                sqlCmd.Parameters.AddWithValue("@Password", _password);
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
                sqlCmd.Parameters.AddWithValue("@Password", _password);
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
                sqlCmd.Parameters.AddWithValue("@Password", _password);
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
                MyPassword = pass;
                DataTable dataTable = Accounts();
                bool Ingelogd = (bool)dataTable.Rows[0]["Ingelogd"];
                if (Ingelogd == false)
                {
                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();
                    String query = "SELECT COUNT(1) FROM Accounts WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, _connection);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", MyUsername);
                    sqlCmd.Parameters.AddWithValue("@Password", pass);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());


                    if (count == 1)
                    {
                        String query2 = "UPDATE Accounts SET Ingelogd = 1 WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                        SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
                        sqlCmd2.CommandType = CommandType.Text;
                        sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);
                        sqlCmd2.Parameters.AddWithValue("@Password", pass);
                        sqlCmd2.ExecuteReader();
                        _connection.Close();
                        if (MyRemember)
                        {
                            StonksCasino.Properties.Settings.Default.Username = MyUsername;
                            StonksCasino.Properties.Settings.Default.Password = pass;
                            StonksCasino.Properties.Settings.Default.Save();
                        }
  
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
                else
                {
                    MessageBox.Show("U kunt nu niet inloggen er is al iemand anders ingelogd op dit account");
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
        public void Logout()
        {
            _connection.Open();
            String query2 = "UPDATE Accounts SET Ingelogd = 0 WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
            SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);
            sqlCmd2.Parameters.AddWithValue("@Password", MyPassword);
            sqlCmd2.ExecuteReader();
            _connection.Close();

        }
        public bool Checkremember()
        {
            if (StonksCasino.Properties.Settings.Default.Username != "" && StonksCasino.Properties.Settings.Default.Password != "")
            {
                MyUsername = StonksCasino.Properties.Settings.Default.Username;
                MyPassword = StonksCasino.Properties.Settings.Default.Password;


                DataTable result = new DataTable();
                try
                {
                  
                    DataTable dataTable = Accounts();
                    bool Ingelogd = (bool)dataTable.Rows[0]["Ingelogd"];
                    if (Ingelogd == false)
                    {
                        if (_connection.State == ConnectionState.Closed)
                            _connection.Open();
                        String query = "SELECT COUNT(1) FROM Accounts WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                        SqlCommand sqlCmd = new SqlCommand(query, _connection);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@Username", MyUsername);
                        sqlCmd.Parameters.AddWithValue("@Password", MyPassword);
                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());


                        if (count == 1)
                        {
                            String query2 = "UPDATE Accounts SET Ingelogd = 1 WHERE Gebruikersnaam=@Username AND Wachtwoord=@Password";
                            SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
                            sqlCmd2.CommandType = CommandType.Text;
                            sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);
                            sqlCmd2.Parameters.AddWithValue("@Password", MyPassword);
                            sqlCmd2.ExecuteReader();
                            _connection.Close();
               
                            StonksCasino.Properties.Settings.Default.Save();
                            LibraryWindow library = new LibraryWindow();
                            library.Show();
                            return true;

                        }
                        else
                        {
                            
                            return false;
                        }
                    }
                    else
                    {
                        
                        return false;
                    }

                }
                catch
                {
                 
                    return false;
                }

                finally
                {
                    _connection.Close();
                }
            }
            return false;
        }
    }

    }

