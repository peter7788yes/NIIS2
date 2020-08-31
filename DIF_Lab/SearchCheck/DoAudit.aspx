<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DoAudit.aspx.cs" Inherits="SearchCheck_DoAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">


 <section class="Content" >
        <div class="path"></div>
        <br/>
        <form id="form1" runat ="server" >
 <div  class="formTb formTb3">
      <table>     <tr>
                <th scope="row">抽查類別</th>
                <td> <asp:Literal ID="ltSearchKindName" runat="server"></asp:Literal>
               
                </td>
              </tr>
              <tr>
                <th scope="row">抽查年月</th>
                <td>
                <asp:Literal ID="ltYearMonth" runat="server"></asp:Literal>
               
                </td>
              </tr>  
            <tr>
                <th scope="row"><span class="must">*</span>狀態</th>
                <td>  
                <asp:DropDownList ID="ddl_AuditStatus" runat="server">
                <asp:ListItem Value="0" Text="請選擇"></asp:ListItem>
                <asp:ListItem Value="1" Text="正常"></asp:ListItem>
                <asp:ListItem Value="2" Text="異常"></asp:ListItem>
                </asp:DropDownList>
                   <asp:RequiredFieldValidator ControlToValidate="ddl_AuditStatus" InitialValue="0" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*必填" ForeColor ="Red"></asp:RequiredFieldValidator>
              
                  </td>
              </tr>  
               <tr>
                <th scope="row"><span class="must">*</span>檔案</th>
                <td>
               <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                <asp:RequiredFieldValidator ControlToValidate="FileUpload1" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*必填" ForeColor ="Red"></asp:RequiredFieldValidator>
                <br/>

                <asp:LinkButton ID="FileLinkButton" CausesValidation="false"  runat="server" onclick="FileLinkButton_Click">LinkButton</asp:LinkButton>
                  </td>
              </tr>  


            
               
      </table>
    </div>  
      <div class="formBtn formBtncenter">
   <asp:Button ID="btnAudit" CssClass="btn" runat="server" Text="確定" 
              onclick="btnAudit_Click"></asp:Button>
    <input type="button"   id="btnReturn" onclick ="javascript:location.href='AuditSearchLogList.aspx';"  value="取消" class="btn" />

    </div>
    </form>
        </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script> 
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

</asp:Content>

