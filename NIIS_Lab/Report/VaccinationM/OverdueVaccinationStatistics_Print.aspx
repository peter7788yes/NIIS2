<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverdueVaccinationStatistics_Print.aspx.cs" Inherits="Report_VaccinationM_OverdueVaccinationStatistics_Print" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

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
     <script>
         var rt =<%=ReportType%>;
     </script>
     <%:Scripts.Render("~/bundles/OverdueVaccinationStatistics_Print_JS")%>
</asp:Content>