<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalCode.aspx.cs" Inherits="System_CodeM_MedicalCode"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm">
            <% if (SearchPower.HasPower) { %>
                <div class="formTb">
                     <table>
                           <tr>
                              <th scope="row">縣市鄉鎮：</th>
                              <td>
                                    <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                                         <option value="">全部</option>
                                         <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                                    </select>

                                    <select id="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                           <option value="">全部</option>
                                           <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                                    </select>

                                    <select id="SelectVillage" ng-model="VM.SelectVillage"  >
                                           <option value="">全部</option>
                                           <option ng-repeat="option in VM.VillageAry" value="{{option.I}}" ng-bind="option.N"></option>
                                    </select>
                              </td>
                            </tr>
                            <tr>
                              <th scope="row">代碼或名稱：</th>
                              <td>
                                  <input ng-model="VM.AN" id="tbAgency"  class="text02" type="text" />
                              </td>
                            </tr>
                            <tr>
                              <th scope="row">開業狀態：</th>
                              <td>
                                  <select ng-model="VM.businessState">
                                      <option value="">請選擇</option>
                                      <option ng-repeat="item in VM.businessStateAry" value="{{item.EV}}" ng-bind="item.EN"></option>
                                  </select>
                              </td>
                            </tr>
                     </table>
                </div>
            <% } %>
           <div class="formBtn">
                    <% if (SearchPower.HasPower) { %>
                         <input type="button" id="searchBtn" ng-click="changePage(1)" value="查詢" class="btn" />
                    <% } %>
                  <input type="button" id="clearBtn" value="清空" ng-click="doReset()" class="btn" />
                   <%--<input type="button" id="addBtn" value="加入合約院所" class="btn" />--%>
            </div>
        </form>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">機構代碼</th>
                    <th scope="col">機構名稱</th>
                    <th scope="col">開業狀態</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter"><span class="replaceA" ng-bind='record["C"]' ng-click="goDetail(record)"></span></td>
                      <td ng-bind='record["AN"]'></td>
                      <td class="aCenter" ng-bind='record["BS"]'></td>
                </tr>
            </table>
        </div>
    </section>
    <script>
        var BgState = <%=BgStateJson%>;
    </script>
</asp:Content>
