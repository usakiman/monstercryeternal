using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// SqlDataBase의 요약 설명입니다.
/// </summary>
public class SqlDataBase
{
    public string SQLCONNECTIONSTRING { get; set; }

    private SqlConnection sqlCon;
    private SqlCommand sqlCom;
    //private SqlTransaction sqlTran = null;

    public SqlDataBase()
    {
        SQLCONNECTIONSTRING = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        sqlCon = new SqlConnection(SQLCONNECTIONSTRING);
    }

    #region SQL 커넥션, Open, Close, Transaction
    private void sqlOpen()
    {
        if (sqlCon.State == System.Data.ConnectionState.Closed)
        {
            sqlCon.Open();
            //sqlTran = sqlCon.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }
    }

    private void sqlClose()
    {
        if (sqlCon.State == System.Data.ConnectionState.Open)
        {
            sqlCon.Close();
        }
    }

    private void sqlTranCommit()
    {
        //sqlTran.Commit();
    }

    private void sqlTranRollback()
    {
        //sqlTran.Rollback();
    }
    #endregion

    #region SQL 전용 EXECUTE
    public int sqlExeQuery(string query, List<IDataParameter> parameter, string type)
    {
        int result = 0;

        try
        {
            sqlOpen();

            //sqlCom = new SqlCommand(query, sqlCon, sqlTran);
            sqlCom = new SqlCommand(query, sqlCon);

            if (type == "TEXT") sqlCom.CommandType = CommandType.Text;
            else if (type == "PROCEDURE") sqlCom.CommandType = CommandType.StoredProcedure;

            if (parameter != null)
            {
                foreach (SqlParameter item in parameter)
                {
                    sqlCom.Parameters.Add(item);
                }
            }

            result = sqlCom.ExecuteNonQuery();

            //sqlTranCommit();
        }
        catch (Exception ex)
        {
            //sqlTranRollback();
        }
        finally
        {
            sqlClose();
        }

        return result;
    }
    #endregion

    #region SQL 전용 READER
    public DataSet sqlReaderQuery(string query, List<IDataParameter> parameter, string type)
    {
        DataSet ds = new DataSet();

        try
        {
            sqlOpen();

            sqlCom = new SqlCommand(query, sqlCon);

            if (type == "TEXT") sqlCom.CommandType = CommandType.Text;
            else if (type == "PROCEDURE") sqlCom.CommandType = CommandType.StoredProcedure;

            if (parameter != null)
            {
                foreach (SqlParameter item in parameter)
                {
                    sqlCom.Parameters.Add(item);
                }
            }

            SqlDataAdapter oda = new SqlDataAdapter(sqlCom);
            oda.Fill(ds);
        }
        catch (SqlException ex)
        {
            //sqlTranRollback();
        }
        finally
        {
            //sqlTranCommit();
            sqlClose();
        }

        return ds;
    }
    #endregion
}
