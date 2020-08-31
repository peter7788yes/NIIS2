<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SearchLogDetailPDF.aspx.cs" Inherits="SearchCheck_SearchLogDetailPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
     <form id="form1" runat ="server" >
 <div align="center">
 <h1><asp:Literal ID="ltH1" runat="server"></asp:Literal></h1>

 <div>
     <asp:Literal ID="ltCaption" runat="server">受稽核單位： 受稽核人員：王曉明 稽核日期：104年8月14日</asp:Literal>
   </div>
   <div  class="listTb">
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
</asp:Content>

