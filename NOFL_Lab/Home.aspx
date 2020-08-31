<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>全國性預防接種資訊管理系統</title>
</head>
  <frameset rows="74,*" cols="*" frameborder="no" border="0" framespacing="0">
      <frame src="/TopHeader.aspx" name="topFrame" scrolling="no" noresize="noresize" id="topFrame" title="topFrame" />
      <frameset cols="226,*" border="0" framespacing="0">
        <frame src="/LeftMenu.aspx?<%=UrlParameter %>" name="leftFrame" scrolling="yes" noresize="noresize" id="leftFrame" title="leftFrame" />
        <frame src="/LoginIndex.htm"  id="cpIframe" name="mainFrame" id="mainFrame" title="mainFrame" />
      </frameset>
    </frameset>
<noframes><body></body></noframes>
    
</html>
