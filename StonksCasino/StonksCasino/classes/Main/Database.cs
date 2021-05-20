using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StonksCasino;
using StonksCasino.Views.main;
using BCrypt.Net;
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
        static bool _logout = true;

        public bool MyLogout
        {
            get { return _logout; }
            set { _logout = value; }
        }
        
        public static DataTable Accounts()
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
           
                string query = "SELECT * FROM accounts WHERE username=@Username";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username);
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
            int HuidigeTokens = (int)dataTable.Rows[0]["token"];
            int tokens = HuidigeTokens + value;
          
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                string query = "UPDATE accounts SET token = " + tokens + " WHERE username=@Username ";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username);
       
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
            long Time = (long)dataTable.Rows[0]["timestamp"];
            if (Time != Properties.Settings.Default.Timestamp)
            {
                _logout = false;

           
            }
            int HuidigeTokens = (int)dataTable.Rows[0]["token"];

            int tokens = HuidigeTokens - value;
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                 string query = "UPDATE accounts SET token = " + tokens + " WHERE username=@Username";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username );
                
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
                bool Ingelogd = (bool)dataTable.Rows[0]["active"];
                if (Ingelogd == false)
                {
                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();
                    String query = "SELECT password FROM accounts WHERE username=@Username";
                    SqlCommand sqlCmd = new SqlCommand(query, _connection);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", MyUsername);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    result.Load(reader);

                    bool verify = BCrypt.Net.BCrypt.Verify(MyPassword, (string)result.Rows[0]["password"]);

                    if (verify)
                    {
                        long timeStamp = GetTimestamp(DateTime.Now);
                        Properties.Settings.Default.Timestamp = timeStamp;
                        Properties.Settings.Default.Save();
                        String query2 = "UPDATE accounts SET active = 1 WHERE username=@Username " +
                                        " UPDATE accounts SET timestamp =@Timestamp WHERE username=@Username ";
                        SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
                        sqlCmd2.CommandType = CommandType.Text;
                        sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);
                 
                        sqlCmd2.Parameters.AddWithValue("@Timestamp", Properties.Settings.Default.Timestamp);
                        sqlCmd2.ExecuteReader();
                        _connection.Close();
                        if (MyRemember)
                        {
                            Properties.Settings.Default.Username = MyUsername;
                            Properties.Settings.Default.Password = pass;
                            Properties.Settings.Default.Save();
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
                if (Ingelogd == true)
                {
                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();
                    String query = "SELECT password FROM accounts WHERE username=@Username";
                    SqlCommand sqlCmd = new SqlCommand(query, _connection);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", MyUsername);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    result.Load(reader);

                    bool verify = BCrypt.Net.BCrypt.Verify(MyPassword, (string)result.Rows[0]["password"]);

                    if (verify)
                    {
                        MessageBoxResult mes = MessageBox.Show("Er is al iemand anders ingelogd op dit account! Als u toch wilt inloggen wordt de ander van uw account afgezet. Let op! Dit kan nadelige gevolgen hebben voor uw account als de persoon die ingelogd is momenteel bezig is met een spel heb je het risico om je inzit kwijt te raken. Wilt u toch inloggen?", "Inloggen", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (mes == MessageBoxResult.Yes)
                        {

                            long timeStamp = GetTimestamp(DateTime.Now);
                            Properties.Settings.Default.Timestamp = timeStamp;
                            Properties.Settings.Default.Save();
                            String query2 = "UPDATE accounts SET timestamp=@Timestamp WHERE username=@Username";
                            SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
                            sqlCmd2.CommandType = CommandType.Text;
                            sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);

                            sqlCmd2.Parameters.AddWithValue("@Timestamp", Properties.Settings.Default.Timestamp);
                            sqlCmd2.ExecuteReader();
                            _connection.Close();
                            if (MyRemember)
                            {
                                Properties.Settings.Default.Username = MyUsername;
                                Properties.Settings.Default.Password = pass;
                                Properties.Settings.Default.Save();
                            }

                            LibraryWindow library1 = new LibraryWindow();
                            library1.Show();
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gebruikersnaam of wachtwoord is onjuist.");
                    }
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
            return false;
        }

        public static long GetTimestamp(DateTime value)
        {
            return long.Parse(value.ToString("yyyyMMddHHmmssffff"));
        }
        public void Logout()
        {
            if (_logout)
            {
                

                DataTable result = new DataTable();
                _connection.Open();
                string query = "SELECT * FROM accounts WHERE username=@Username ";
                SqlCommand sqlCmd = new SqlCommand(query, _connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", _username);
              
                SqlDataReader reader = sqlCmd.ExecuteReader();
                result.Load(reader);
            
                    String query2 = "UPDATE accounts SET active = 0 WHERE username=@Username";
                    SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
                    sqlCmd2.CommandType = CommandType.Text;
                    sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);
                    sqlCmd2.Parameters.AddWithValue("@Password", MyPassword);
                    sqlCmd2.ExecuteReader();
                

                _connection.Close();
            }
        }
        public bool Checkremember()
        {
            if (Properties.Settings.Default.Username != "" && Properties.Settings.Default.Password != "")
            {
                MyUsername = Properties.Settings.Default.Username;
                MyPassword = Properties.Settings.Default.Password;


                DataTable result = new DataTable();
                try
                {
                  
                    DataTable dataTable = Accounts();
                    bool Ingelogd = (bool)dataTable.Rows[0]["active"];
                    if (Ingelogd == false)
                    {
                        if (_connection.State == ConnectionState.Closed)
                            _connection.Open();
                        String query = "SELECT password FROM accounts WHERE username=@Username";
                        SqlCommand sqlCmd = new SqlCommand(query, _connection);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@Username", MyUsername);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        result.Load(reader);

                        bool verify = BCrypt.Net.BCrypt.Verify(MyPassword, (string)result.Rows[0]["password"]);

                        if (verify)
                        { 
                            String query2 = "UPDATE accounts SET active = 1 WHERE username=@Username";
                            SqlCommand sqlCmd2 = new SqlCommand(query2, _connection);
                            sqlCmd2.CommandType = CommandType.Text;
                            sqlCmd2.Parameters.AddWithValue("@Username", MyUsername);
                         
                            sqlCmd2.ExecuteReader();
                            _connection.Close();
               
                            Properties.Settings.Default.Save();
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

