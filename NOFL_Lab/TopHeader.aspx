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
        <!--section class="toplink">
                <ul>
                        <li><a href="http://web.emeeting.hyweb.com.tw/Paperless_PC/setup.exe">連線速率程式安裝</a></li>
                        <li><a href="http://web.emeeting.hyweb.com.tw/Paperless_PC/setup.exe">離線預注程式下載</a></li>
                </ul>
        </section-->
        <!--toplink  end -->

        <!--user start -->
        <section class="user">
                <span class="name"><%=txtLongin1 %></span> 
                <%=txtLongin2 %>
        </section>

        <!--user end -->
        <section class="logout" runat="server">
            <form runat="server">
                <asp:LinkButton ID="btnLogout" OnClick="btnLogout_Click" Text="登出" runat="server" />
            </form>
        </section>
</asp:Content>

<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">

</asp:Content> 