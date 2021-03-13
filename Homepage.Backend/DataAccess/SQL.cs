using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Backend.DataAccess
{
    class SQL
    {
        /*  
         * Klasse führt folgende SQL Statemantes aus 
         * ExecuteInsert - Fügt mit Parametern einen Eintrag in der Datenbank ein
         * ExecuteSelect - Holt Daten aus einer Datenbank ab | optional mit Parametern
         * ExecuteUpdate - Aktualisiert einen Eintrag aus der Datenbank 
         */

        private string dbConnection = Properties.Settings.Default.DBConnection;

        public bool ExecuteInsert(string commandText, string[] parameter, object[] values)
        {
            return executeInsert(commandText, parameter, values);
        }

        public bool ExecuteUpdate(string commandText, string[] parameter, object[] values)
        {
            return executeUpdate(commandText, parameter, values);
        }

        public DataTable ExecuteSelect(string commandText, string[] parameter, object[] values)
        {
            return executeSelect(commandText, parameter, values);
        }

        /** Das SQL Statement liegt in der Datenbank als Prozedur, der Prozedurname wird hierbei
         * als Parameter "Commandtext" mitgegeben **/
        private DataTable executeSelect(string commandText, string[] parameter, object[] values)
        {
            SqlConnection connection = new SqlConnection();
            SqlDataReader reader = null;

            DataTable table = new DataTable();

            try
            {
                connection.ConnectionString = dbConnection;

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = commandText;

                /** Prüfung ob Parameter mitgegeben wurden **/
                for (int i = 0; i < parameter.Length; i++)
                {
                    /** Hat der Parameter Inhalt? Falls nicht muss 
                     * DBNull mitgegeben werden, sonst entstehen Fehler **/
                    if (values[i] != null)
                    {
                        command.Parameters.AddWithValue(parameter[i], values[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(parameter[i], DBNull.Value);
                    }
                }

                command.Connection.Open();

                reader = command.ExecuteReader();
                table.Load(reader);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return table;
        }

        /** Das SQL Statement liegt in der Datenbank als Prozedur, der Prozedurname wird hierbei
        * als Parameter "Commandtext" mitgegeben **/
        private bool executeUpdate(string commandText, string[] parameter, object[] values)
        {
            SqlConnection connection = new SqlConnection();

            bool result = false;

            try
            {
                connection.ConnectionString = dbConnection;

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = commandText;

                /** Prüfung ob Parameter mitgegeben wurden **/
                for (int i = 0; i < parameter.Length; i++)
                {
                    /** Hat der Parameter Inhalt? Falls nicht muss 
                     * DBNull mitgegeben werden, sonst entstehen Fehler **/
                    if (values[i] != null)
                    {
                        command.Parameters.AddWithValue(parameter[i], values[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(parameter[i], DBNull.Value);
                    }
                }

                command.Connection.Open();

                long? checkId = (long?)command.ExecuteNonQuery();

                if (checkId.HasValue)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /** Das SQL Statement liegt in der Datenbank als Prozedur, der Prozedurname wird hierbei
        * als Parameter "Commandtext" mitgegeben **/
        private bool executeInsert(string commandText, string[] parameter, object[] values)
        {
            SqlConnection connection = new SqlConnection();
            bool result = false;

            try
            {
                connection.ConnectionString = dbConnection;

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = commandText;

                /** Prüfung ob Parameter mitgegeben wurden **/
                for (int i = 0; i < parameter.Length; i++)
                {
                    /** Hat der Parameter Inhalt? Falls nicht muss 
                     * DBNull mitgegeben werden, sonst entstehen Fehler **/
                    if (values[i] != null)
                    {
                        command.Parameters.AddWithValue(parameter[i], values[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(parameter[i], DBNull.Value);
                    }
                }

                command.Connection.Open();

                long? checkId = (long?)command.ExecuteNonQuery();

                if (checkId.HasValue)
                {
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
