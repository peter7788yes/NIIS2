<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ReadFile.aspx.cs" Inherits="LogManage_ReadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<form runat ="server" id="form1">
    <asp:Button ID="Button1" runat="server" Text="第一次 讀取檔案 由檔案規格 建TABLE" 
        onclick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Text="讀取檔案 bulk insert 至log" 
        onclick="Button2_Click" />
<asp:Button ID="Button3" runat="server" Text="讀取檔案 last line" 
        onclick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="drop table " 
        onclick="Button4_Click" />
		 <asp:Button ID="Button5" runat="server" Text="讀取檔案 PDI" onclick="Button5_Click" 
        />
		
        </form>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
</asp:Content>

