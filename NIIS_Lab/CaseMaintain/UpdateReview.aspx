<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateReview.aspx.cs" Inherits="CaseMaintain_UpdateReview" %>
<%@ PreviousPageType VirtualPath="~/CaseMaintain/UserProfile.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="../css/design.css" rel="stylesheet" type="text/css" /> 

 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
      
  
 <section class="Content"  > 
  <!--路徑 start -->
  <div class="path"></div>
  <!--路徑 end --> 
  <!-- form start-->
  <form runat ="server" id="form1" enctype="multipart/form-data" >
 
    <div class="formTb formTb3">
      <table>
              <tr>  
              <th scope="row">身分證號：</th>
                <td style ="width:200px"><asp:Literal ID="ltIdNo" runat="server"></asp:Literal></td>

                <th scope="row">姓名：</th>
                <td style ="width:200px"><asp:Literal ID="ltName" runat="server"></asp:Literal></td> 
                <th scope="row">戶籍地址：</th>
                <td><asp:Literal ID="ltResAddr" runat="server"></asp:Literal></td>  
              </tr>
            </table> 
    </div>

  <!-- form end-->  
  <div class="formTb formTb3">
  
   <asp:Literal ID="ltUpdateFields" runat="server"></asp:Literal>
   </div>

          <div class="formBtn formBtncenter">
  <input type="button" name="btnSave" id="btnSave"   value="確定" class="btn" style="display:none" /> 
  <asp:Button ID="Button1" runat="server" Text="確定" CssClass="btn" 
                  onclick="Button1_Click"   ></asp:Button>
   
    </div>  

  </form>
  <!--表格 end --> 
</section>






</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

 


    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script> 
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script> 



</asp:Content>

