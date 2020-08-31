<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCheck.aspx.cs" Inherits="System_AccountM_AccountCheck" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
   <link href="/css/design.min.css" rel="stylesheet"/>         
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm">
            <div class="formTb">
                <table>
                    <tr>
                        <th scope="row">帳號或姓名:</th>
                        <td>
                            <input type="text" class="text01" ng-model="VM.Uname" />

                        </td>
                    </tr>
                    <tr>
                        <th scope="row">單位:</th>
                        <td>
                            <input  id="tbLocation" ng-model="VM.Oname"  type="text" class="text03" ng-click="openOrgs()" />
                            <img style="cursor:pointer" src="/images/location.png" ng-click="openOrgs()" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">審核狀態:</th>
                        <td>
                            <select ng-model="VM.selectCheck">
                                <option value="0">全部</option>
                                <option ng-repeat="option in VM.checkAry" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <input type="button" id="searchBtn" value="查詢" class="btn" ng-click="changePage(1)"  />
            </div>
        </form>
        <page-nav ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display: none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">帳號</th>
                    <th scope="col">姓名</th>
                    <th scope="col">單位</th>
                    <th scope="col">申請時間</th>
                    <th scope="col">審核狀態</th>
                    <th scope="col">審核/查看</th>
                </tr>
                <tr  ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]'></td>
                    <td class="aCenter" ng-bind='record["L"]'></td>
                    <td class="aCenter" ng-bind='record["U"]'></td>
                    <td class="aCenter" ng-bind='record["ON"]'></td>
                    <td class="aCenter" ng-bind='record["AD"] | SimpleTaiwanDate'></td>
                    <td class="aCenter" ng-bind='getCheck(record)'></td>
                    <td class="aCenter"><img style="cursor:pointer" ng-click="goDetail(record)" src="/images/icon_maintain.png"  /></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
      <script>
          var roleData = <%=MyRoleData %>;
          var enableData = <%=MyEnableData %>;
          var checkData = <%=MyCheckData %>;
          var logoutData = <%=MyLogoutData %>;
       </script>
    <%:Scripts.Render("~/bundles/AccountCheck_JS")%>
</asp:Content>




