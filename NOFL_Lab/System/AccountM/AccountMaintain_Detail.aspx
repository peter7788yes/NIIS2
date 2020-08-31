﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMaintain_Detail.aspx.cs" Inherits="System_AccountM_AccountMaintain_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">

    <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
        <div class="formBtn formBtnleft">
            <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
        <div class="formTb">
            <table>
                <tr>
                    <th scope="row">帳號:</th>
                    <td>
                        <%=VM.LoginName %>
                    </td>
                </tr>
                <tr>
                    <th scope="row">姓名:</th>
                    <td>
                        <%=VM.UserName %>
                    </td>
                </tr>
                <tr>
                    <th scope="row">啟用狀態:</th>
                    <td>
                        <%=VM.IsEnabledString %>
                    </td>
                </tr>
                <tr>
                    <th scope="row">申請日期:</th>
                    <td>
                         <%=ApplyDate %>
                    </td>
                </tr>
            </table>
        </div>

        <page-nav ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display: none;" class="listTb" ng-cloak>
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">登入時間</th>
                    <th scope="col">登入IP</th>
                    <th scope="col">登出時間</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]'></td>
                    <td class="aCenter" ng-bind='record["L"]'></td>
                    <td class="aCenter" ng-bind='record["U"]'></td>
                    <td class="aCenter" ng-bind='record["R"]'></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/AccountMaintain_Detail_JS")%>
</asp:Content>



