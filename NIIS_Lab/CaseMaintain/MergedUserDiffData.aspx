<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MergedUserDiffData.aspx.cs" Inherits="CaseMaintain_MergedUserDiffData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="../css/design.css" rel="stylesheet" type="text/css" /> 

 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">

 
 <section class="Content"   > 
  <!--路徑 start -->
  <div class="path"></div>
  <!--路徑 end --> 
  <!-- form start-->
  <br/>
  <form runat ="server" id="form1" >
 
   

  <!-- form end-->  
  <div class="formTb formTb3">
  
   <asp:Literal ID="ltDiffFields" runat="server"></asp:Literal>
   </div>

          <div class="formBtn formBtncenter">
   <input type="button" onclick="javascript:location.href='MergedUserList.aspx';" class ="btn" value="確定" />
    </div>  

  </form>
  <!--表格 end --> 
</section>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
 
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script> 
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
  
</asp:Content>

