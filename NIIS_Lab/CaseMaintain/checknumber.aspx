<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="checknumber.aspx.cs" Inherits="CaseMaintain_checknumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<form runat ="server" id="form1">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1"
        runat="server" Text="Button" onclick="Button1_Click" />
        </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
</asp:Content>

