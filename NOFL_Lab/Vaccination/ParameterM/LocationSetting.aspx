<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationSetting.aspx.cs" Inherits="Vaccination_ParameterM_LocationSetting" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                <% if (SearchPower.HasPower) { %>
                     <input type="button" id="searchBtn" ng-click="changePage(1)" value="查詢" class="btn" />
                <% } %>
               <%--<input type="button" id="addBtn" value="加入合約院所" class="btn" />--%>
        </div>

        <% if (SearchPower.HasPower) { %>
            <div class="formTb">
                 <table>

                      <tr>
                        <td colspan="2">
                            <input  id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                            <a href="javascript:void(0);"><img  src="/images/location.png" ng-click="openOrgs()" /></a>
                        </td>
                      </tr>
                       <tr>
                          <th scope="row">院所地址：</th>
                          <td>
                          
                                <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                                     <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>

                                <select id="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                       <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>

                                <select id="SelectVillage" ng-model="VM.SelectVillage"  >
                                       <option ng-repeat="option in VM.VillageAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">醫事機構名稱：</th>
                          <td>
                              <input ng-model="VM.agencyObj.AN" id="tbAgency"  class="text02" type="text" ng-click="openSelectAgency()"/>
                              <a href="javascript:void(0);" ng-click="openSelectAgency()">
                                  <img src="/images/icon_agency.png" />
                              </a>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row"><span class="must">*</span>接種單位狀態：</th>
                          <td>
                              <select ng-model="VM.publishState">
                                  <option ng-repeat="item in VM.publishStateAry" value="{{item.EV}}" ng-bind="item.EN"></option>
                              </select>
                          </td>
                        </tr>
                 </table>
            </div>
        <% } %>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">醫事機構代碼</th>
                    <th scope="col">醫事機構名稱</th>
                    <th scope="col">單位狀態</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter"><a href="javascript:void(0);" ng-bind='record["C"]' ng-click="goUpdate(record)"></a></td>
                      <td class="aCenter" ng-bind='record["AN"]'></td>
                      <td class="aCenter" ng-bind='record["AS"]'></td>
                </tr>
            </table>
        </div>

    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var AgState = <%=AgStateJson%>;
    </script>
     <%:Scripts.Render("~/bundles/LocationSetting_JS")%>
</asp:Content>
