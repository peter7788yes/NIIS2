<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentViewDownload.aspx.cs" Inherits="DocumentManagementM_DocumentViewDownload" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">發佈日期</th>
                    <th scope="col">標題</th>
                    <th scope="col">檔案數量</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["P"] | SimpleTaiwanDate '></td>
                      <td><span class="replaceA"  ng-bind='record["D"]' ng-click="goDetail(record)"></span></td>
                      <td class="aCenter" ng-bind='record["FC"]'></td>
                </tr>
            </table>
        </div>
        </form>
    </section>
</asp:Content>