<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCheck2_Detail.aspx.cs" Inherits="System_AccountM_AccountCheck2_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
     <section class="Content2" ng-app="MyApp" ng-controller="MyController">
        <h2>查看結果</h2>
         <div class="formTb formTb2">
            <table>
                <tr>
                    <td id="on_cy">
                    </td>
                </tr>
                </table>
           </div>
        <page-nav ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;"  class="listTb" >
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">單位</th>
                    <th scope="col">姓名</th>
                    <th scope="col">帳號</th>
                    <th scope="col">帳號狀態</th>
                    <th scope="col">角色</th>
                    <th scope="col">是否續用</th>
                    <th scope="col">不續用的原因說明</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]'></td>
                    <td class="aCenter" ng-bind='record["ON"]'></td>
                    <td class="aCenter" ng-bind='record["UN"]'></td>
                    <td class="aCenter" ng-bind='record["LN"]'></td>
                    <td class="aCenter" ng-bind='record["ES"]'></td>
                    <td class="aCenter" ng-bind='record["RN"]'></td>
                    <td class="aCenter" ng-bind='record["IBC"]'></td>
                    <td class="aCenter" ng-bind='record["INC"]'></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var i = <%:AccountCheckID%>;
    </script>
     <%:Scripts.Render("~/bundles/AccountCheck2_Detail_JS")%>
</asp:Content>

