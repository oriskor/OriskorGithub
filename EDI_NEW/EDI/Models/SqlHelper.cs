using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

public class SqlHelper
{

    string DBConnectionString = string.Empty;
    static SqlConnection sqlcon;

    public SqlHelper()
    {
        DBConnectionString = ConfigurationManager.ConnectionStrings["EDIConnection"].ConnectionString;
        sqlcon = new SqlConnection(DBConnectionString);
    }

    public void SetDatabaseConnection()
    {
        if (DBConnectionString == string.Empty)
        {
            DBConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }
        sqlcon = new SqlConnection(DBConnectionString);
    }

    public DataSet ExecuteProcudere(string procName, Hashtable parms)
    {
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = procName;
        cmd.CommandType = CommandType.StoredProcedure;
        if (sqlcon == null)
        {
            SetDatabaseConnection();
        }
        cmd.Connection = sqlcon;
        if((parms !=null) && (parms.Count > 0))
        {
            foreach (DictionaryEntry deparams in parms)
            {
                cmd.Parameters.AddWithValue(deparams.Key.ToString(), deparams.Value);
            }
        }
        da.SelectCommand = cmd;
        da.Fill(ds);
        return ds;
    }

    public int ExecuteQuery(string procName, Hashtable parms)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = procName;
        if (parms.Count > 0)
        {
            foreach (DictionaryEntry deparams in parms)
            {
                cmd.Parameters.AddWithValue(deparams.Key.ToString(), deparams.Value);
            }
        }

        if (sqlcon == null)
        {
            SetDatabaseConnection();
        }

        cmd.Connection = sqlcon;
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();

        int result = cmd.ExecuteNonQuery();
        return result;
    }

    public int ExecuteQuerywithOutputparams(SqlCommand cmd)
    {
        if (sqlcon == null)
        {
            SetDatabaseConnection();
        }
        cmd.Connection = sqlcon;
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();

        int result = cmd.ExecuteNonQuery();
        return result;

    }

    public int ExecuteQueryWithOutParam(string procName, Hashtable parms)
    {
        SqlCommand cmd = new SqlCommand();
        SqlParameter sqlparam = new SqlParameter();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = procName;
        if (parms.Count > 0)
        {
            foreach (DictionaryEntry deparams in parms)
            {
                if (deparams.Key.ToString().Contains("_out"))
                {
                    sqlparam = new SqlParameter(deparams.Key.ToString(), deparams.Value);
                    sqlparam.DbType = DbType.Int32;
                    sqlparam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(sqlparam);
                }
                else
                {
                    cmd.Parameters.AddWithValue(deparams.Key.ToString(), deparams.Value);
                }
            }

        }

        if (sqlcon == null)
        {
            SetDatabaseConnection();
        }

        cmd.Connection = sqlcon;
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();

        int result = cmd.ExecuteNonQuery();
        if (sqlparam != null)
            result = Convert.ToInt32(sqlparam.SqlValue.ToString());
        return result;
    }

}