<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationSetting.aspx.cs" Inherits="Vaccination_ParameterM_LocationSetting" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>
<%@ Register Src="~/UC/UC_OpenSelectAgency.ascx" TagPrefix="uc1" TagName="UC_OpenSelectAgency" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
  <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <%-- <%= test() %>
        <%: test() %>--%> 
        <%--<div class="formBtn formBtnleft">
                <% if (SearchPower.HasPower) { %>
                     <input type="button" id="searchBtn" ng-click="changePage(1)" value="查詢" class="btn" />
                <% } %>
                <%--<input type="button" id="addBtn" value="加入合約院所" class="btn" />--%>
        <%--</div>--%>
        <% if (SearchPower.HasPower) { %>
        <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
            <div class="formTb">
                 <table>
                      <tr>
                        <th scope="row">所屬單位：</th>
                        <td>
                            <%-- <input  id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                            <img style="cursor:pointer" ng-click="openOrgs()" src="/images/location.png"  />--%>
                            <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                        </td>
                      </tr>
                       <tr>
                          <th scope="row">院所地址：</th>
                          <td>
                                <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                                     <option value="0">全部</option>
                                     <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>

                                <select id="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                       <option value="0">全部</option>
                                       <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>

                                <select id="SelectVillage" ng-model="VM.SelectVillage">
                                       <option value="0">全部</option>
                                       <option ng-repeat="option in VM.VillageAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">醫事機構名稱：</th>
                          <td>
                              <uc1:UC_OpenSelectAgency runat="server" ID="UC_OpenSelectAgency" />
                              <%--<input ng-model="VM.agencyObj.AN" id="tbAgency"  class="text02" type="text" ng-click="openSelectAgency()"/>
                              <img style="cursor:pointer" ng-click="openSelectAgency()" src="/images/icon_agency.png" />--%>
                              <%--<img class="lazy" data-original="/images/icon_agency.png" width="29" height="24"/>--%>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row"><span class="must">*</span>接種單位狀態：</th>
                          <td>
                              <select id="selectPublishState" required="reqiured" ng-model="VM.publishState">
                                  <option value="">請選擇</option>
                                  <option ng-repeat="item in VM.publishStateAry" value="{{item.EV}}" ng-bind="item.EN"></option>
                              </select>
                          </td>
                        </tr>
                 </table>
            </div>
            <div class="formBtn">
                    <% if (SearchPower.HasPower) { %>
                         <input type="button" id="searchBtn" ng-click="queryClick()" value="查詢" class="btn" />
                    <% } %>
                    <%--<input type="button" id="addBtn" value="加入合約院所" class="btn" />--%>
                    <input type="button" id="clearBtn" value="清空" ng-click="doReset()" class="btn" />
                    <input type="submit" id="btnSubmit" style="display:none;" />
            </div>
        </form>
        <% } %>
      <div id="divFunc" class="function" style="display:none;">
          <ul>
              <li><a href="javascript:(void)" ng-click="goPrint()" class="print">列印</a></li>
          </ul>
      </div>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">醫事機構代碼</th>
                    <th scope="col">醫事機構名稱</th>
                    <th scope="col">單位狀態</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter"><span class="replaceA" ng-bind='record["C"]' ng-click="goDetail(record)"></span></td>
                      <td ng-bind='record["AN"]'></td>
                      <td class="aCenter" ng-bind='record["AS"]'></td>
                </tr>
            </table>
        </div>
    </section>
    <script>
        var defaultOrgID="<%=DefaultOrgID %>";
        var defaultOrgName="<%=DefaultOrgName %>";
        var AgState = <%=AgStateJson%>;
    </script>
</asp:Content>