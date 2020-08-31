<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AbnormalVaccinationDetail_Print.aspx.cs" Inherits="Report_VaccinationM_AbnormalVaccinationDetail_Print" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ PreviousPageType VirtualPath="~/Report/VaccinationM/AbnormalVaccinationDetail.aspx" %> 
<%@ Reference VirtualPath="~/Report/VaccinationM/AbnormalVaccinationDetail.aspx" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
   <div id="divReport">
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/AbnormalVaccinationDetail_Print_JS")%>
</asp:Content>