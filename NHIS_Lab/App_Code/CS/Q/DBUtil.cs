using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using NSDBUtil;
using System.Configuration;

namespace NSDBUtil
{
    public enum TblOpType { Select, Insert, Delete, Update };
    public enum CmdOpType { ExecuteNonQuery, ExecuteReader, ExecuteScalar, ExecuteReaderReturnDataTable };
}

/// <summary>
/// DataBase 操作的class
/// </summary>
public class DBUtil
{

    public static object DBOp(string ConnString, TblOpType tblOperation, string strSelectField, string strTableName, string strWhereClause, string strOrderClause, string[,] strFieldArray, string[] strWhereParameterArray, CmdOpType cmdType)
    {
       string ConnectionString=  ConfigurationManager.ConnectionStrings[ConnString].ToString();
        string strCommandText,
                strSetOperation = "",
                strInsertField = "",
                strValues = "";

        string[] whereParamName = null;
        // 把where 弄出來
        int intWhereArrayRows = (strWhereParameterArray != null && strWhereParameterArray.Length != 0 ? strWhereParameterArray.Length : 0);
        if (strWhereClause != "" && intWhereArrayRows > 0)
        {
            whereParamName = new string[intWhereArrayRows];
            for (int i = 0; i < whereParamName.Length; ++i)
                whereParamName[i] = "@where_" + i;
            strWhereClause = string.Format(strWhereClause, whereParamName);
        }
        // 把insert update 的field 跟資料(@) 放進來
        int intFieldArrayRows = (strFieldArray != null && strFieldArray.Rank != 0 ? strFieldArray.Length / strFieldArray.Rank : 0);
        if (tblOperation == TblOpType.Update && strFieldArray.Length > 0)
        {
            for (int i = 0; i < intFieldArrayRows; ++i)
                strSetOperation += (i > 0 ? ", " : "") + strFieldArray[i, 0] + "=" + (strFieldArray[i, 1] == "NULL" ? "NULL" : "@setOrValue_" + i + "_" + strFieldArray[i, 0]);
        }
        else if (tblOperation == TblOpType.Insert && strFieldArray.Length > 0)
        {
            for (int i = 0; i < intFieldArrayRows; ++i)
            {
                strInsertField += (i > 0 ? ", " : "") + strFieldArray[i, 0];
                strValues += (i > 0 ? ", " : "") + (strFieldArray[i, 1] == "NULL" ? "NULL" : "@setOrValue_" + i + "_" + strFieldArray[i, 0]);
            }
        }

        switch (tblOperation)
        {
            case TblOpType.Select:
                strCommandText = "SELECT " + strSelectField + " FROM " + strTableName + (strWhereClause == "" ? "" : " WHERE " + strWhereClause) + (strOrderClause == "" ? "" : " ORDER BY " + strOrderClause);
                break;
            case TblOpType.Delete:
                strCommandText = "DELETE FROM " + strTableName + (strWhereClause == "" ? "" : " WHERE " + strWhereClause);
                break;
            case TblOpType.Update:
                strCommandText = "UPDATE " + strTableName + " SET " + strSetOperation + (strWhereClause == "" ? "" : " WHERE " + strWhereClause);
                break;
            case TblOpType.Insert:
                strCommandText = "INSERT INTO " + strTableName + "(" + strInsertField + ")VALUES (" + strValues + ")";
                break;
            default:
                strCommandText = "";
                break;
        }

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {

            using (SqlCommand command = new SqlCommand(strCommandText, connection))
            {
                for (int i = 0; i < intFieldArrayRows; ++i)
                    if (strFieldArray[i, 1] != "NULL")
                        command.Parameters.AddWithValue("@setOrValue_" + i + "_" + strFieldArray[i, 0], strFieldArray[i, 1]);
                for (int i = 0; i < intWhereArrayRows; ++i)
                    command.Parameters.AddWithValue(whereParamName[i], strWhereParameterArray[i]);
                command.CommandTimeout = 0;
                connection.Open();
                object sqlResult;
                switch (cmdType)
                {
                    case CmdOpType.ExecuteNonQuery:
                        sqlResult = command.ExecuteNonQuery();
                        break;
                    case CmdOpType.ExecuteScalar:
                        sqlResult = command.ExecuteScalar();
                        break;
                    case CmdOpType.ExecuteReader:
                        sqlResult = command.ExecuteReader();
                        break;
                    case CmdOpType.ExecuteReaderReturnDataTable:
                        sqlResult = command.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load((SqlDataReader)sqlResult);
                        return dt;
                    default:
                        sqlResult = null;
                        break;
                }
                return sqlResult;

            }
        }
    }

    /// <summary>
    ///    基本的DB Operation。
    /// </summary>
    public static object DBOp(string ConnString, string sql, string[] strParameterArray, CmdOpType cmdType)
    {
        try
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string[] paramName = null;
            // 把where 弄出來
            int intParameterArrayRows = (strParameterArray != null && strParameterArray.Length != 0 ? strParameterArray.Length : 0);
            if (sql != "" && intParameterArrayRows > 0)
            {
                paramName = new string[intParameterArrayRows];
                for (int i = 0; i < paramName.Length; ++i)
                    paramName[i] = "@param_" + i;
                sql = string.Format(sql, paramName);
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    for (int i = 0; i < intParameterArrayRows; ++i)
                        command.Parameters.AddWithValue(paramName[i], strParameterArray[i]);
                    command.CommandTimeout = 0;
                    connection.Open();
                    object sqlResult;
                    switch (cmdType)
                    {
                        case CmdOpType.ExecuteNonQuery:
                            sqlResult = command.ExecuteNonQuery();
                            break;
                        case CmdOpType.ExecuteScalar:
                            sqlResult = command.ExecuteScalar();
                            break;
                        case CmdOpType.ExecuteReader:
                            sqlResult = command.ExecuteReader();
                            break;
                        case CmdOpType.ExecuteReaderReturnDataTable:
                            sqlResult = command.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load((SqlDataReader)sqlResult);
                            return dt;
                        default:
                            sqlResult = null;
                            break;
                    }
                    return sqlResult;
                }
            }
        }
        catch (Exception ex)
        {
            //oUtil.LogWriteLine(ex.Message);
            throw ex;
        }
    }

    /// <summary>
    ///    基本的DB Operation。
    /// </summary>
    public static object DBOpQ(string ConnString, string sql, Queue paramQ, CmdOpType cmdType)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
        string[] paramName = null;
        // 把where 弄出來
        //Console.WriteLine("paramQ.count: 「{0}」", paramQ.Count);
        int intParameterArrayRows = (paramQ != null && paramQ.Count != 0 ? paramQ.Count : 0);
        if (sql != "" && intParameterArrayRows > 0)
        {
            paramName = new string[intParameterArrayRows];
            for (int i = 0; i < paramName.Length; ++i)
                paramName[i] = "@param_" + i;
            sql = string.Format(sql, paramName);
        }
        //Console.WriteLine("DBOperationQ sql: 「{0}」", sql);
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                //string tmpV;
                for (int i = 0; i < intParameterArrayRows; ++i)
                {
                    //tmpV = paramQ.Dequeue().ToString();
                    //Console.WriteLine("command.Parameters.AddWithValue({0},{1})", paramName[i], tmpV);
                    command.Parameters.AddWithValue(paramName[i], paramQ.Dequeue());
                }
                command.CommandTimeout = 0;
                connection.Open();
                object sqlResult;
                switch (cmdType)
                {
                    case CmdOpType.ExecuteNonQuery:
                        sqlResult = command.ExecuteNonQuery();
                        break;
                    case CmdOpType.ExecuteScalar:
                        sqlResult = command.ExecuteScalar();
                        break;
                    case CmdOpType.ExecuteReader:
                        sqlResult = command.ExecuteReader();
                        break;
                    case CmdOpType.ExecuteReaderReturnDataTable:
                        sqlResult = command.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load((SqlDataReader)sqlResult);
                        return dt;
                    default:
                        sqlResult = null;
                        break;
                }
                return sqlResult;
            }
        }
    }

    /// <summary>
    /// 把DataTable 轉成 HTML 格式的Table
    /// </summary>
    /// <param name="dt">要轉的dataTable</param>
    /// <returns>HTML 格式的Table 字串</returns>
    public static string DataTabletoHTMLTable(DataTable dt)
    {
        StringBuilder sb = new StringBuilder("<table><tr>");
        foreach (DataColumn col in dt.Columns)
            sb.AppendFormat("<td>{0}</td>", col.Caption);
        sb.Append("</tr>");
        foreach (DataRow row in dt.Rows)
        {
            sb.Append("<tr>");
            foreach (DataColumn col in dt.Columns)
                sb.AppendFormat("<td>{0}</td>", row[col].ToString());
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        return sb.ToString();
    }

    /// <summary>
    /// 把dataTable 轉成xml
    /// </summary>
    /// <param name="dt">要轉的dataTable</param>
    /// <returns>xml 格式的dataTable</returns>
    public static string ToStringAsXml(DataTable dt)
    {
        StringWriter sw = new StringWriter();
        if (dt.TableName == "") dt.TableName = "Dummy";
        dt.WriteXml(sw, XmlWriteMode.IgnoreSchema);
        return sw.ToString();
    }

    /// <summary>
    /// 把dataTable 轉成xml 的字串轉回 dataTable
    /// </summary>
    /// <param name="dtXML">要轉的xml字串</param>
    /// <returns>無此Table, 回傳null，不然就回傳第一個table</returns>
    public static DataTable ToDataTableFromXML(string dtXML)
    {
        StringReader stream = new StringReader(dtXML);
        DataSet ds = new DataSet();
        ds.ReadXml(stream);
        return (ds.Tables.Count > 0 ? ds.Tables[0] : null);
    }


    /// <summary>
    /// 把DataTable 轉成 字串
    /// </summary>
    /// <param name="dt">要轉的dataTable</param>
    /// <returns>字串</returns>
    public static string DataTableToString(DataTable dt)
    {
        StringBuilder sb = new StringBuilder("");
        foreach (DataColumn col in dt.Columns)
            sb.AppendFormat("{0}    ", col.Caption);

        sb.Append("<br/>");

        foreach (DataRow row in dt.Rows)
        { 
            foreach (DataColumn col in dt.Columns)
                sb.AppendFormat("{0}    ", row[col].ToString().Replace ("<br/>",";"));

            if (dt.Rows.Count>1) sb.Append("<br/>");   
        }
        return sb.ToString();
    }

}
