<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InoculationRecordTable_Print.aspx.cs" Inherits="Report_VaccinationM_InoculationRecordTable_Print" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
   <section class="Content4" ng-app="MyApp" ng-controller="MyController">
    <h2>BCG未完成報表</h2>
    <div id="tmBlock" style="display:none;" class="listTb">
        <table>
            <tr>
                <th scope="col">姓名</th>
                <th scope="col">出生日期</th>
                <th scope="col">身分證號</th>
                <th scope="col">戶籍村里</th>
                <th scope="col">母親姓名</th>
                <th scope="col">母親身分證號</th>
            </tr>
            <tr ng-repeat="record in TM.data track by $index">
                 <td class="aCenter" ng-bind='record["CN"]'>李黃明</td>
                 <td class="aCenter" ng-bind='record["BD"] | SimpleTaiwanDate'>李黃明</td>
                 <td class="aCenter" ng-bind='record["RI"]'>李黃明</td>
                 <td ng-bind='record["LV"]'>李黃明</td>
                 <td class="aCenter" ng-bind='record["MN"]'>李黃明</td>
                 <td class="aCenter" ng-bind='record["MR"]'>李黃明</td>
             </tr>
        </table>
    </div>
</section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var tbData=<%=tbData%>;
    </script>
     <%:Scripts.Render("~/bundles/InoculationRecordTable_Print_JS")%>
</asp:Content>