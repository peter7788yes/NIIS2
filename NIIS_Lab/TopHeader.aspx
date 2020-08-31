<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopHeader.aspx.cs" Inherits="TopHeader" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ MasterType virtualpath="~/MasterPage/MasterPage.master" %>
   
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
        <%=HeadScript %>
</asp:Content> 
    
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
         <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content> 

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>
        
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
        <section class="logo"><img src="/images/top_logo.gif" alt="全國性預防接種資訊管理系統" /></section>
        <section class="toplink">
                <ul>
                        <li><a href="http://web.emeeting.hyweb.com.tw/Paperless_PC/setup.exe">連線速率程式安裝</a></li>
                        <li><a href="http://web.emeeting.hyweb.com.tw/Paperless_PC/setup.exe">離線預注程式下載</a></li>
                </ul>
        </section>
        <section class="user">
                <span class="name"><%:txtLogin1 %></span> 
                <%:txtLogin2 %> <label id="lblOnlineUser"><%:txtLogin3 %></label>
        </section>
        <section class="logout" runat="server">
            <form runat="server" autocomplete="off">
                <asp:LinkButton ID="btnLogout" OnClick="btnLogout_Click" Text="登出"  ClientIDMode="Static" runat="server" />
            </form>
        </section>
</asp:Content>

<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
      <%:Scripts.Render("~/bundles/TopHeader_JS")%>
</asp:Content> 