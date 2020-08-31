<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="VisitProfileList.aspx.cs" Inherits="CaseVisit_VisitProfileList" %>
<%@ Import Namespace="System.Web.Optimization" %>
  
<%@ Register src="../CaseMaintain/ucUserProfileList.ascx" tagname="ucUserProfileList" tagprefix="uc1" %>
  
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
   <div class="path"></div>
    <uc1:ucUserProfileList ID="ucUserProfileList1" runat="server" />
 
</asp:Content>
 
