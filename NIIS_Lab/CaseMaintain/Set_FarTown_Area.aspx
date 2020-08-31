<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Set_FarTown_Area.aspx.cs" Inherits="CaseMaintain_Set_FarTown_Area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<form id="form1" runat ="server" >
若個案的戶籍地於以下列表者，則具有【山地離島偏鄉個案】個案身分
<table>
<tr><td>
    <asp:DropDownList ID="ddlCounty" runat="server"  AutoPostBack ="true" 
        onselectedindexchanged="ddlCounty_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlTown" runat="server">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="新增" onclick="Button1_Click" />
</td></tr>
</table>

<div class="listTb">
    <asp:GridView ID="GridView1" runat="server" 
       >
       
    </asp:GridView>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
</asp:Content>

