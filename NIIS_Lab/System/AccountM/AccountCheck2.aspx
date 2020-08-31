<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCheck2.aspx.cs" Inherits="System_AccountM_AccountCheck2"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
           <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
            <div class="formTb">
                <table>
                    <tr>
                        <th scope="row">所屬單位：</th>
                        <td>
                            <input type="text" class="text03" disabled="disabled" value="<%=DefaultOrgName %>" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">查看單位:</th>
                        <td>
                            <%--<input  id="tbLocation" ng-model="VM.Oname"  type="text" class="text03" ng-click="openOrgs()" />
                            <img style="cursor:pointer" ng-click="openOrgs()" src="/images/location.png" />--%>
                            <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">查看年度:</th>
                        <td>
                             <input id="tbDate" type="text" class="text02"  />
                             &nbsp;&nbsp;
                             <img style="cursor:pointer" onclick="WdatePicker({el:'tbDate',dateFmt: 'yyy年',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                             &nbsp;&nbsp;
                             <select ng-model="VM.selectYearSeason">
                                <option value="">全部</option>
                                <option ng-repeat="option in VM.yearSeasonAry" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">清查結果:</th>
                        <td>
                            <select ng-model="VM.selectCheckResult">
                                <option value="">全部</option>
                                <option ng-repeat="option in VM.checkResultAry" value="{{option.EV}}" ng-bind="option.EN"></option>
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
                    <th scope="col">清查年別</th>
                    <th scope="col">單位</th>
                    <th scope="col">帳號總數</th>
                    <th scope="col">續用</th>
                    <th scope="col">不續用</th>
                    <th scope="col">未確認</th>
                    <th scope="col">執行人員</th>
                    <th scope="col">執行日期</th>
                    <th scope="col">清查進度</th>
                    <th scope="col">查看結果</th>
                    <th scope="col">上傳結果</th>
                    <th scope="col">結果列印</th>
                </tr>
                <tr  ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]'></td>
                    <td class="aCenter" ng-bind='record["CY"]'></td>
                    <td class="aCenter" ng-bind='record["CON"]'></td>
                    <td class="aCenter" ng-bind='record["CN"]'></td>
                    <td class="aCenter" ng-bind='record["BC"]'></td>
                    <td class="aCenter" ng-bind='record["BB"]'></td>
                    <td class="aCenter" ng-bind='record["BUC"]'></td>
                    <td class="aCenter" ng-bind='record["UN"]'></td>
                    <td class="aCenter" ng-bind='record["AD"] | SimpleTaiwanDate'></td>
                    <td class="aCenter" ng-bind='record["CP"]'></td>
                    <td class="aCenter">
                         <img style="cursor:pointer" ng-click="openDetail(record)" src="/images/icon_browse.png"  />
                    </td>
                    <td class="aCenter">
                         <img style="cursor:pointer" ng-click="openUpload(record)" src="/images/icon_maintain.png" />
                    </td>
                    <td class="aCenter">
                         <img style="cursor:pointer" ng-click="goDetail(record)" src="/images/icon_print.png" />
                    </td>
                </tr>
            </table>
        </div>
    </section>
    <script>
          var defaultOrgID="<%=DefaultOrgID %>";
          var defaultOrgName="<%=DefaultOrgName %>";
          var roleData = <%=MyRoleData %>;
          var enableData = <%=MyEnableData %>;
          var checkData = <%=MyCheckData %>;
          var logoutData = <%=MyLogoutData %>;
          var checkResultData = <%=MyCheckResultData %>;
          var yearSeasonData = <%=MyYearSeasonData %>.slice(4);
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>




