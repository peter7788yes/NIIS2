<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCheck.aspx.cs" Inherits="System_AccountM_AccountCheck" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm">
            <div class="formBtn formBtnleft">
                <input type="button" id="Btn_OTP" value="OTP申請審核" class="btn" onclick="location.href='OTPList.aspx'" />
            </div>
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
                            <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">審核狀態:</th>
                        <td>
                            <select ng-model="VM.selectCheck">
                                <option value="">全部</option>
                                <option ng-repeat="option in VM.checkAry" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <input type="button" id="searchBtn" value="查詢" class="btn" ng-click="changePage(1)"  />
                <input type="button" id="clearBtn" value="清空" ng-click="doReset()" class="btn" />
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
                    <td ng-bind='record["L"]'></td>
                    <td ng-bind='record["U"]'></td>
                    <td class="aCenter" ng-bind='record["ON"]'></td>
                    <td class="aCenter" ng-bind='record["AD"] | ShortTaiwanTime'></td>
                    <td class="aCenter" ng-bind='getCheck(record)'></td>
                    <td class="aCenter"><img style="cursor:pointer" ng-click="goDetail(record)" src="/images/icon_maintain.png"  /></td>
                </tr>
            </table>
        </div>
    </section>
    <script>
          var defaultOrgID="<%=DefaultOrgID %>";
          var defaultOrgName="<%=DefaultOrgName %>";
          var roleData = <%=MyRoleData %>;
          var roleData = <%=MyRoleData %>;
          var enableData = <%=MyEnableData %>;
          var checkData = <%=MyCheckData %>;
          var logoutData = <%=MyLogoutData %>;
    </script>
</asp:Content>




