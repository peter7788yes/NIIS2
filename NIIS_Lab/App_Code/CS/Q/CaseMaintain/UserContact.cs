using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// CaseUserProfile 的摘要描述
/// </summary>
public class UserContact
{
    public UserContact()
    {
        ContactID = 0;
        CaseID = 0;
        RelationShip = 0;
        ContactCaseID = 0;
        CreatedUserID = 0;
        ModifyUserID = 0;
    }

    /// <summary>
    /// 資料identity
    /// </summary>
    public int ContactID { get; set; }

    /// <summary>
    /// 個案ID
    /// </summary>
    public int CaseID { get; set; }

    /// <summary>
    /// 聯絡人個案ID
    /// </summary>
    public int ContactCaseID { get; set; }

    public int CreatedUserID { get; set; }
    public int ModifyUserID { get; set; }

    /// <summary>
    /// 關係
    /// </summary>
    public int RelationShip { get; set; }
    public string RelationShipName
    {
        get { return SystemCode.GetName("CaseUser_ContactRelationShip", RelationShip); }
    }

    /// <summary>
    /// 主要聯絡人
    /// </summary>
    public bool IsMain { get; set; }



    public UserContact(int iContactID)
    {  
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xGetCaseUserContact] {0} ",
              new string[] { iContactID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        if (dt.Rows.Count > 0)
        {

            ContactID = iContactID;
            CaseID = Convert.ToInt32(dt.Rows[0]["CaseID"]);
            RelationShip = Convert.ToInt32(dt.Rows[0]["ContactRelationShip"]);
            ContactCaseID = Convert.ToInt32(dt.Rows[0]["ContactCaseID"]);
            IsMain = (dt.Rows[0]["IsMain"].ToString() == "1" ? true : false);

        }

    }


    public int Add()
    {

        int NewID = 0;
        try
        {
 


            NewID = Convert.ToInt32(

                DBUtil.DBOp("ConnDB",
                          "exec dbo.usp_CaseUser_xAddContactRelationShip {0},{1},{2},{3},{4}  ",
                          new string[] { 
                                CaseID.ToString()
                                ,RelationShip .ToString()               
                                ,ContactCaseID.ToString() 
                                ,(IsMain?"1":"0")
                                 ,AuthServer .GetLoginUser().ID.ToString ()
                           },
                           NSDBUtil.CmdOpType.ExecuteScalar));

             

        }
        catch(Exception ex)
        {
            throw ex;
        }
        return NewID;
    }

    public int Update()
    {

        int rowcount = 0;
        try
        {
            rowcount = Convert.ToInt32(

          DBUtil.DBOp("ConnDB",
                    "exec dbo.usp_CaseUser_xUpdateContactRelationShip {0},{1},{2},{3} ",
                    new string[] { 
                                ContactID.ToString()    
                                ,RelationShip .ToString()               
                                ,(IsMain?"1":"0")
                                 ,AuthServer .GetLoginUser().ID.ToString ()
                           },
                     NSDBUtil.CmdOpType.ExecuteScalar));
        }
        catch
        {
        }
        return rowcount;
    }

    public int Delete()
    {

        int rowcount = 0;
        try
        {
            rowcount = Convert.ToInt32(

          DBUtil.DBOp("ConnDB",
                    @" UPDATE [dbo].[C_CaseUserContact] SET [LogicDel] = 1  ,[ModifyDate] = getdate()  ,[ModifyUserID] = {1} WHERE ContactID= {0};
                        declare @CaseID int  ; 
                        select  @CaseID=CaseID  from [C_CaseUserContact]  where ContactID={0} ;   
                        exec dbo.[usp_CaseUser_xUpdateCaseUserMotherID] @CaseID 
                        select @@rowcount            
                    ",
                    new string[] { 
                                ContactID.ToString()     
                                ,AuthServer .GetLoginUser().ID.ToString ()
                                 },
                     NSDBUtil.CmdOpType.ExecuteScalar));
          
        }
        catch
        {
        }
        return rowcount;
    }
}