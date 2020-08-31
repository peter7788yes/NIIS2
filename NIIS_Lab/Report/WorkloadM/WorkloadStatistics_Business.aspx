<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkloadStatistics_Business.aspx.cs" Inherits="Report_WorkloadM_WorkloadStatistics_Business" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
     <link href="/css/tab.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
   <section class="Content" ng-app="MyApp" ng-controller="MyController">
     <div class="path"></div>
        <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <%--<div class="tab">
          <ul class="tabul">
            <li><span class="tabA" id="tab1">工作量統計表</span> </li>
            <li><span class="tabA tabHereA">公務報表</span></li>
          </ul>
        </div>--%>
        <div class="formTb">
                         <table>
                             <tr>
                                 <th scope="row">所屬單位:</th>
                                 <td>
                                     <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                                 </td>
                             </tr>
                             <tr>
                                <th scope="row"><span class="must">*</span>報表類型:</th>
                                <td>
                                    <asp:RadioButton GroupName="rbA" ID="rb1" Text="年報" ClientIDMode="Static" runat="server" CssClass="radio01" />
                                    <asp:RadioButton GroupName="rbA" ID="rb2" Text="季報" ClientIDMode="Static" runat="server" CssClass="radio01" />
                                </td>
                              </tr>
                         </table>
                 </div>
        <div class="formBtn">
             <input type="button" class="btn" value="預覽列印"  ng-click="goPrint()" />
             <asp:Button Text="匯出xls" ID="btnDownload" CssClass="btn" ClientIDMode="Static" runat="server" OnClick="btnDownload_Click" />
        </div>
      </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/WorkloadStatistics_Business_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>