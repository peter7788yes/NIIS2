<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="GetIdNoFromUploadFile.aspx.cs" Inherits="CaseMaintain_GetIdNoFromUploadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">


<section class="Content2" style="width:500px">
  <h2>上傳身份證號</h2> 
      
        <!-- form start-->
  <form id="form1" runat ="server">
  
    <div  class="formTb formTb2">
      <table>
      <tr>  <td> 
      <p>請下載此<a href="上傳身份證號範例.xls">範例檔案</a>, 修改後再上傳</p>
       </td> </tr>
        <tr>  <td> <asp:FileUpload ID="fu_Excel" runat="server"></asp:FileUpload><asp:RequiredFieldValidator ID="rfvfu_Excel" runat="server" ErrorMessage="*必填"  ForeColor="Red"  ControlToValidate ="fu_Excel"></asp:RequiredFieldValidator>
          <div class="formBtn" style="display:inline">
        <asp:Button ID="btnUpload" runat="server" Text="確定" onclick="btnUpload_Click" CssClass ="btn"></asp:Button>
        </div>
        </td>    </tr>
        </table>
      </div>
 </form>
 </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">


    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

    <script type="text/javascript">
 
    
    </script>

</asp:Content>

