<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="LogImportSetting.aspx.cs" Inherits="LogManage_LogImportSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
    <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
 

<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    <form id="form1" runat ="server"  >
   <section class="Content2" style="width:100%">
  <h2>RIS介接設定</h2> 
       
       <div class="formTb" >
    <asp:DropDownList ID="ddl_LogItem" runat="server" 
           AutoPostBack ="true"      onselectedindexchanged="ddl_LogItem_SelectedIndexChanged">
    </asp:DropDownList>
    <div id="LogItemMainSettingDIV" visible ="false"  runat ="server"  >
     <h3>主資料設定</h3>
       <hr /> 
      <table  border ="1" style="background-color:#F5F5DC">  
       <tr><th style="width:150px">設定欄位</th><th style="width:600px">值</th></tr>
         <tr><td>ID</td><td> <asp:Literal ID="ltID" runat="server"></asp:Literal>
       </td>
       </tr>

       <tr><td>項目代號</td><td><asp:TextBox ID="tbLogItemName" runat="server"  ></asp:TextBox>
       (填英文/數字  -- 與 log table 名稱相關)
       </td>
       </tr>
       <tr><td>中文描述</td><td><asp:TextBox ID="tbLogItemCName" runat="server"  ></asp:TextBox>
       </td>
       </tr>

        <tr><td>依序欄位長度</td><td><asp:TextBox ID="tbColLen" runat="server" Width="550px"></asp:TextBox>
        (如果已經是有資料了 最好不要改順序)
         <br/> <asp:TextBox ID="tbAddRange"  Visible ="false" runat="server" Width="50px"></asp:TextBox>


            <asp:Button ID="btnAlterTable" runat="server" Text="重設欄位長度" 
                onclick="btnAlterTable_Click"></asp:Button>(會新增修改刪除欄位 請小心使用  刪除欄位之後要拿掉)
        </td></tr> 
        <tr><td>依序欄位名稱</td><td><asp:TextBox ID="tbColName" runat="server" Width="550px"></asp:TextBox>
           (如果已經是有資料了 最好不要改順序)
        
        </td></tr>
      <tr><td>主鍵Key(身份證號)<br/>欄位序號(第幾欄)</td><td><asp:TextBox ID="tbKey" runat="server" Width="50px"></asp:TextBox>依照此欄來比對應更新的個案
      <br/> (如果已經是有資料了 最好不要改順序)
      </td></tr>
       <tr><td>主鍵Key(身份證號)<br/>不存在是否新增</td>
       <td><asp:DropDownList ID="ddlOp" runat="server"><asp:ListItem Value="0" Text="否"></asp:ListItem><asp:ListItem Value="1" Text="是"></asp:ListItem></asp:DropDownList>
      
      </td></tr>

      <tr><td>申請日期+時間<br/>欄位序號(第幾欄)</td><td>申請日期<asp:TextBox ID="DateSeq" runat="server" Width="50px"></asp:TextBox> 申請時間<asp:TextBox ID="TimeSeq" runat="server" Width="50px"></asp:TextBox>
       使用 申請日期+申請時間 來比對 是否更新(沒有時間就是00:00:00)
       <br/> (如果已經是有資料了 最好不要改順序)
       </td></tr>
      <tr><td>是否啟用</td><td><asp:DropDownList ID="ddlActive" runat="server"><asp:ListItem Value="0" Text="否"></asp:ListItem><asp:ListItem Value="1" Text="是"></asp:ListItem></asp:DropDownList>
      
      </td></tr>
       <tr><td>每次檢查時必備</td><td><asp:DropDownList ID="ddlMust" runat="server"><asp:ListItem Value="0" Text="否"></asp:ListItem><asp:ListItem Value="1" Text="是"></asp:ListItem></asp:DropDownList>
      
      </td></tr>

      <tr><td>執行順序</td><td> <asp:TextBox ID="tbExecOrder" runat="server" Width="50px"></asp:TextBox>
      (減少錯誤率)
      </td></tr>



      



       <tr><td>檔案名稱格式</td><td><asp:TextBox ID="tbFileNameFormat" runat="server" ></asp:TextBox>20140205_YOBHPA01.txt 
       (可以改成Regex 用設的  但時間有限 先這樣)
       
       </td></tr>
         <tr><td>確認檔案名稱格式</td><td><asp:TextBox ID="tbConfirmFileNameFormat" runat="server" ></asp:TextBox>20140205_YMBHPA01.txt 
       <br/>必備<asp:DropDownList ID="ddlConfirmFileMust" runat="server"><asp:ListItem Value="0" Text="否"></asp:ListItem><asp:ListItem Value="1" Text="是"></asp:ListItem></asp:DropDownList>
       </td></tr>
       <tr><td>使用Table名稱</td><td> <asp:Literal ID="ltLogTableName" runat="server"></asp:Literal>
           <asp:Button ID="btnCreateTable" runat="server" Text="重建table" 
               onclick="btnCreateTable_Click" />(drop  and create- 若有資料請小心使用)
           </td></tr>
     <tr><td>預存程序名稱
     </td><td><asp:TextBox ID="tbSPName" runat="server" ></asp:TextBox> 補足 樓下無法完成的例外需求
     (沒WORK)
     </td></tr>
     
       </table>  
           <div class=" formBtn formBtncenter" >
       <asp:Button ID="btnSaveSetting" runat="server" Text="儲存設定(不會alter log table)" 
               onclick="btnSaveSetting_Click"></asp:Button>

               <asp:Button ID="btnClone" runat="server" Text="Clone一份出來" 
               onclick="btnClone_Click"></asp:Button>

               <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click"></asp:Button>
       </div>
     </div>  
 
    
    </div>
     <br/>  
     <h3>欄位介接設定</h3>
     <hr />
         <div class="formTb" >

    <asp:DropDownList ID="ddl_FromItemColumn" runat="server">
    </asp:DropDownList>
==＞使用funtion

 <asp:DropDownList ID="ddl_UseFun" runat="server"></asp:DropDownList>
     來匯入==＞
    <asp:DropDownList ID="ddl_ToCaseColumn" runat="server"> 
    </asp:DropDownList>
 
    <asp:TextBox ID="TextBox1" Visible ="false"  runat="server"></asp:TextBox>
   <asp:Button ID="btnAdd" runat="server" Text="新增" cssClass="btn" 
               onclick="btnAdd_Click"  />
               <br/>
     <asp:Literal ID="ltTip" runat="server"></asp:Literal>
     <div  CssClass = "listTb" >
    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" 
             onrowdatabound="GridView1_RowDataBound"  >
    <Columns>
<asp:ButtonField  commandname="Del"  ButtonType="Button"
                  text="刪除"  />
</Columns>
    </asp:GridView></div>


    <br/>
     <br/></div>
   </section>
   </form>
   
</asp:Content>
 
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
</asp:Content>

