using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// CaseUserProfile 的摘要描述
/// </summary>
public class UserRemark
{
    public UserRemark()
    { 
        CaseID  = 0 ; 
        RemarkID = 0 ;
        RemarkType = ""; 
        RemarkContent = "";
        CreatedUserID = 0;
        ModifyUserID =0;
    }

    public int RemarkID { get; set; } 
    public int CaseID { get; set; }
    public int CreatedUserID { get; set; } 
    public int ModifyUserID { get; set; } 
    public string RemarkType { get; set; } 
    public string RemarkContent { get; set; } 
    public int FileID { get; set; }


    public string RemarkTypeName {
        get {  return SystemCode.GetName("CaseUser_RemarkType", Convert.ToInt32(RemarkType) );} 
         } 


    public UserRemark(int iRemarkID)
        : base()
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
                                                  " SELECT [ID] ,CaseUserID as CaseID,[RemarkType]  ,CaseRemark,isnull([FileID],0) FileID FROM [dbo].[C_CaseUserRemark] where [LogicDel]=0  and  [ID]={0} ",
                                                   new string[] { iRemarkID.ToString() }
                                                    , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

        if (dt.Rows.Count > 0)
        {
            RemarkID=iRemarkID;
            CaseID = Convert.ToInt32 (dt.Rows[0]["CaseID"]);
            RemarkType = dt.Rows[0]["RemarkType"].ToString(); 
            RemarkContent=dt.Rows[0]["CaseRemark"].ToString();
            FileID = Convert.ToInt32(dt.Rows[0]["FileID"]);
         }

        }


    public int Add()
    {

        int newid = 0;
        try
        {
             

            newid = Convert.ToInt32(DBUtil.DBOp("ConnDB", " exec dbo.usp_CaseUser_xAddCaseUserComment {0},{1},{2},{3},{4},{5}",
                new string[] { CaseID.ToString(), CreatedUserID.ToString(), RemarkType, RemarkContent,FileID .ToString (),(RemarkType=="3" ? "1":"0") }
                  , NSDBUtil.CmdOpType.ExecuteScalar));
        }
        catch { 
        }
        return newid;
    }

    public int Update()
    {

        int rowcount = 0;
        try
        {
            rowcount = Convert.ToInt32(DBUtil.DBOp("ConnDB"
            , " Update  [C_CaseUserRemark] set  [RemarkType]={1}  ,[CaseRemark]={2} ,[ModifyUserID]={3}  where ID={0}  ;select @@rowcount "
            , new string[] { RemarkID.ToString(), RemarkType, RemarkContent, ModifyUserID .ToString ()}
            , NSDBUtil.CmdOpType.ExecuteScalar));
        }
        catch
        {
        }
        return rowcount;
    }
     

}