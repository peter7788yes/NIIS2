<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Configuration" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="robots" content="noindex,nofollow" />
    <title><%:WebConfigurationManager.AppSettings["SiteName"] %></title>
    <!--[if lt IE 9]>
        <script src="../bower_components/html5shiv/dist/html5shiv.min.js"></script>
    <![endif]-->
      <%:Scripts.Render("~/bundles/Home_JS")%>
</head>
  <frameset rows="74,*" cols="*" frameborder="no" border="0" framespacing="0">
      <frame src="/TopHeader.aspx" name="topFrame" scrolling="no" noresize="noresize" id="topFrame" title="topFrame" />
      <frameset cols=250,*" border="0" framespacing="0">
        <frame src="/LeftMenu.aspx?<%=UrlParameter %>" id="leftFrame"  name="leftFrame" scrolling="yes" noresize="noresize" title="leftFrame" />
        <frame src="/LoginIndex.htm"   name="mainFrame" id="mainFrame" title="mainFrame" />
      </frameset>
    </frameset>
<noframes><body></body></noframes>
    
</html>
