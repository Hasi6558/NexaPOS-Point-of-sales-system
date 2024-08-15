using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;

public class DB
{
    DataSet ds;

    public DB()
    {
        ds = new DataSet();
    }

    public string QueryScalar(string Query)
    {
        string str = null;
        SqlConnection SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ToString());
        SqlCommand SqlCommand = new SqlCommand(Query, SqlConnection);

        try
        {
            SqlConnection.Open();
            object result = SqlCommand.ExecuteScalar();
            if (result != null)
            {
                str = result.ToString();
            }
            else
            {
                str = "0"; // Default value in case of null
                MessageBox.Show(str);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            Console.WriteLine($"An error occurred: {ex.Message}");
            str = "0"; // Default value in case of an exception
        }
        finally
        {
            SqlCommand.Dispose();
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        return str;
    }


}