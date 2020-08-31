<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReloadEnum.aspx.cs" Inherits="ReloadEnum" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
       <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
    <div>        
        <script src="/js/date/calendar.js"></script>
        <script src="/js/date/WdatePicker.js"></script>
        <input type="text" id="d28" class="Wdate" onclick="WdatePicker({ dateFmt: 'yyy/MM/dd',lang:'zh-tw'})"/>
    </div>
    </form>
</body>
</html>
