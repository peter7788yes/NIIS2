<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ParentChildDetail.aspx.cs" Inherits="CaseMaintain_ParentChildDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
 
<asp:Content ID="Content4"  ContentPlaceHolderID="ctCP" Runat="Server">


<section class="Content2" style="width:500px">
  <h2>子女明細：<asp:Literal ID="ltID" runat="server"></asp:Literal></h2>  
        <!-- form start-->
  <form id="form1" runat ="server">
 
    <div  class="formTb formTb2 listTb">
      <asp:GridView ID="GridView1"  runat="server"></asp:GridView>
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

