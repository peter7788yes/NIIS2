<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopHeader.aspx.cs" Inherits="TopHeader" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ MasterType virtualpath="~/MasterPage/MasterPage.master" %>
   
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
        <%=HeadScript %>
</asp:Content> 
    
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
         <%:Styles.Render("~/bundles/Common_CSS")%>
       
</asp:Content> 

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server">
        <%=BodyClass %>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
        <section class="logo"><img src="/images/top_logo.gif" alt="全國性預防接種資訊管理系統" /></section>
        <!--toplink start -->
       
        <!--toplink  end -->

        <!--user start -->
        <section class="user">
                <span class="name"><%=txtLogin1 %></span> 
                <%=txtLogin2 %> <label id="lblOnlineUser"><%=txtLogin3 %></label>
        </section>

        <!--user end -->
        <section class="logout" runat="server">
            <form runat="server" autocomplete="off">
                <asp:LinkButton ID="btnLogout" OnClick="btnLogout_Click" Text="登出"  ClientIDMode="Static" runat="server" />
            </form>
        </section>
</asp:Content>

<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
      <%:Scripts.Render("~/bundles/TopHeader_JS")%>
</asp:Content> 