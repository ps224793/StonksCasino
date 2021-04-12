using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Main
{
    class Database
    {
        static SqlConnection _connection = new SqlConnection("Server = tcp:summacasino.database.windows.net, 1433; Initial Catalog = Casino; Persist Security Info=False;user ID = Summacollegeproject3; Password=Casinoproject3;");

        public static DataTable Accounts()
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                SqlCommand command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM Accounts WHERE id=1;";
                SqlDataReader reader = command.ExecuteReader();
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

        public static DataTable Tokensadd(int huidige, int value)
        {
            int tokens = huidige + value;
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                SqlCommand command = _connection.CreateCommand();
                command.CommandText = "UPDATE Accounts SET Token = " + tokens + " WHERE id=1;";
                SqlDataReader reader = command.ExecuteReader();
              
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
        public static DataTable Tokensremove(int huidige, int value)
        {
            int tokens = huidige - value;
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                SqlCommand command = _connection.CreateCommand();
                command.CommandText = "UPDATE Accounts SET Token = " + tokens + " WHERE id=1;";
                SqlDataReader reader = command.ExecuteReader();

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

    }
}
