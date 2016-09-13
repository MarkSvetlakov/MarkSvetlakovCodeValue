using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

public class DAL
{
    private const string _dbName = "MainDB.mdf";
    private const string _dataPath = "~/App_Data/";


    private string GetConnectionString()
    {
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Append(@"Data Source=(LocalDB)\MSSQLLocalDB;");
        strBuilder.Append($"AttachDbFilename={HttpContext.Current.Server.MapPath(_dataPath)}{_dbName};");
        strBuilder.Append("Integrated Security=True;");

        return strBuilder.ToString();
    }


    public bool ExecuteNonQuery(string sql)
    {
        bool flag = false;
        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        {
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            try
            {
                int updatedRows = command.ExecuteNonQuery();
                if (updatedRows > 0)
                {
                    flag = true;
                }
            }
            catch (SqlException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        return flag;
    }


    public object ExecuteScalar(string sql)
    {
        object val = null;
        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        {
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            val = command.ExecuteScalar();
        }
        return val;
    }


    public DataSet GetDataSet(string sql)
    {
        DataSet dataSet = null;
        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection))
            {
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
            }
        }
        return dataSet;
    }


    public Task<DataSet> AsyncGetDataSet(string sql)
    {
        DataSet dataSet = null;
        return Task<DataSet>.Factory.StartNew(() =>
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection))
                {
                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                }
            }
            return dataSet;
        });
    }
}