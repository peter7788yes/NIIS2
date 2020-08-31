<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="LeftMenu.aspx.cs" Inherits="LeftMenu" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ MasterType virtualpath="~/MasterPage/MasterPage.master" %>
   
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
         <%=HeadScript %>
</asp:Content> 
    

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
          <%:Styles.Render("~/bundles/LeftMenu_CSS")%>
</asp:Content> 


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server">
        <%=BodyClass %>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
  <section class="tree">
       <ul id ="ulRoot">

       </ul>
  </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" Runat="Server">
       <script>
           var data = '<%=MyTreeData %>';
           var p="<%=PageUrl %>";
       </script>
       <%:Scripts.Render("~/bundles/LeftMenu_JS")%>
</asp:Content> 
   

   