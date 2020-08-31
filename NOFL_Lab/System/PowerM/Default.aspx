<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="System_PowerM_Default"  MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
   
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
    

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/PowerM_Default_CSS")%>
</asp:Content> 


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server">
     <%=BodyClass %>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
     <h2>test</h2>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/PowerM_Default_JS")%>
</asp:Content> 
   

   