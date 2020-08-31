<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchLogPdf.aspx.cs" Inherits="SearchCheck_SearchLogPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
  <form id="form1" runat ="server" >
 <div align="center">
 <h1><asp:Literal ID="ltH1" runat="server"></asp:Literal></h1>
<br /> 
<br /> 
 <div>
     <asp:Literal ID="ltCaption" runat="server"></asp:Literal>
   </div>
    <br /> 

   <div  class="listTb">
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>
