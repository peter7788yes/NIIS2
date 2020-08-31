<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeFile="RolePowerSetting.aspx.cs" Inherits="RolePowerSetting" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/RolePowerSetting_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    
    <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
        <div class="formBtn formBtnleft">
            <input type="button" id="addBtn" value="新增" class="btn" />
        </div>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">角色名稱</th>
                    <th scope="col">所屬層級</th>
                    <th scope="col">角色說明</th>
                    <th scope="col">權限修改</th>
                    <th scope="col">角色名稱修改</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["RN"]'></td>
                      <td class="aCenter" ng-bind='record["O"]'></td>
                      <td class="aCenter" ng-bind='record["RD"]'></td>
                      
                      <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="changePower(record)"/></a></td>
                      <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="changeName(record)" /></a></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/RolePowerSetting_JS")%>
</asp:Content>




