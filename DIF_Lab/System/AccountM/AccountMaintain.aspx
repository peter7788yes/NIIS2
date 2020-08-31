<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMaintain.aspx.cs" Inherits="System_AccountM_AccountMaintain" MasterPageFile="~/MasterPage/MasterPage.master" %>

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

    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <div class="formBtn formBtnleft">
            <input type="button" id="searchBtn" value="查詢" class="btn" ng-click="changePage(1)"  />
            <input type="button" id="addBtn" value="新增帳號" class="btn" />
        </div>
        <div class="formTb">
            <table>
                <tr>
                    <th scope="row">帳號或姓名:</th>
                    <td>
                        <input name="" type="text" class="text01" ng-model="VM.Uname" />

                    </td>
                </tr>
                <tr>
                    <th scope="row">單位:</th>
                    <td>
                        <input  id="tbLocation" ng-model="VM.Oname"  type="text" class="text03" ng-click="openOrgs()" />
                        <img  style="cursor:pointer" src="/images/location.png" ng-click="openOrgs()" />
                    </td>
                </tr>
                <tr>
                    <th scope="row">角色:</th>
                    <td>
                        <select ng-model="VM.selectRole">
                            <option value="0">全部</option>
                            <option ng-repeat="option in VM.roleAry" value="{{option.I}}" ng-bind="option.R"></option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th scope="row">啟用狀態:</th>
                    <td>
                        <select ng-model="VM.selectEnable">
                            <option value="0">全部</option>
                            <option ng-repeat="option in VM.enableAry" value="{{option.EV}}" ng-bind="option.EN"></option>
                        </select>
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
                <tr>
                    <th scope="row">未登入時間:</th>
                    <td>
                        <select ng-model="VM.selectLogout">
                            <option value="0">全部</option>
                            <option ng-repeat="option in VM.logoutAry" value="{{option.EV}}" ng-bind="option.EN"></option>
                        </select>
                        (1個月以30天計)
                    </td>
                </tr>
            </table>
        </div>

        <page-nav ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display: none;" class="listTb" >
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">帳號</th>
                    <th scope="col">姓名</th>
                    <th scope="col">可使用系統</th>
                    <th scope="col">單位</th>
                    <th scope="col">角色</th>
                    <th scope="col">未登入時間</th>
                    <th scope="col">啟用狀態</th>
                    <th scope="col">審核狀態</th>
                    <th scope="col">登入紀錄</th>
                </tr>
                <tr  ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]'></td>
                    <td class="aCenter" ng-bind='record["L"]'></td>
                    <td class="aCenter" ng-bind='record["U"]'></td>
                    <td class="aCenter" ng-bind='record["SP"]'></td>
                    <td class="aCenter" ng-bind='record["ON"]'></td>
                    <td class="aCenter" ng-bind='record["RN"]'></td>
                    <td class="aCenter" ng-bind='record["N"]'></td>
                    <td class="aCenter" ng-bind='getEnable(record)'></td>
                    <td class="aCenter" ng-bind='getCheck(record)'></td>
                    <td class="aCenter"><img style="cursor:pointer" ng-click="goDetail(record)" src="/images/icon_maintain.png" /></td>
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
    <%:Scripts.Render("~/bundles/AccountMaintain_JS")%>
</asp:Content>




