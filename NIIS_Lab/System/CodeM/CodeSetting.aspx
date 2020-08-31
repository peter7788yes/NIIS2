﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeSetting.aspx.cs" Inherits="System_CodeM_CodeSetting"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm" autocomplete="off"></form>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">代碼名稱</th>
                    <th scope="col">代碼值數量</th>
                    <th scope="col">代碼值設定</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td ng-bind='record["CD"]'></td>
                      <td class="aCenter" ng-bind='record["R"]'></td>
                      <td class="aCenter"><img style="cursor:pointer" src="/images/icon_maintain.png" ng-click="goDetail(record)" /></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>
