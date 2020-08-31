<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReloadEnum.aspx.cs" Inherits="ReloadEnum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <script src="/js/date/calendar.js"></script>
        <script src="/js/date/WdatePicker.js"></script>
        <input type="text" id="d28" class="Wdate" onclick="WdatePicker({ dateFmt: 'yyy/MM/dd',lang:'zh-tw'})"/>

    </div>
    </form>
</body>
</html>
