<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchoolCode.aspx.cs" Inherits="System_CodeM_SchoolCode" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
      <form id="MyForm" runat="server" autocomplete="off">
        <div class="formBtn formBtnleft">
                <% if (AddPower.HasPower) { %>
                  <input type="button" id="addBtn" value="新增代碼" class="btn" ng-click="goAdd()" />
                <% } %>
        </div>
        <% if (SearchPower.HasPower) { %>
            <div class="formTb">
                 <table>
                       <tr>
                          <th scope="row">縣市鄉鎮：</th>
                          <td>
                          
                                <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                                        <option value="0">全部</option>
                                        <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>

                                <select id="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                       <option value="0">全部</option>
                                       <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>

                                <select id="SelectVillage" ng-model="VM.SelectVillage"  >
                                       <option value="0">全部</option>
                                       <option ng-repeat="option in VM.VillageAry" value="{{option.I}}" ng-bind="option.N"></option>
                                </select>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">名稱：</th>
                          <td>
                              <input ng-model="VM.SN" id="tbSchool"  class="text02" type="text" />
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">狀態：</th>
                          <td>
                              <select ng-model="VM.enableState">
                                  <option value="0">請選擇</option>
                                  <option ng-repeat="item in VM.enableStateAry" value="{{item.EV}}" ng-bind="item.EN"></option>
                              </select>
                          </td>
                        </tr>
                 </table>
            </div>
        <div class="formBtn">
               <input type="button" id="searchBtn" ng-click="changePage(1)" value="查詢" class="btn" />
               <input type="button" id="clearBtn" value="清空" ng-click="doReset()" class="btn" />
        </div>
        <% } %>
          </form>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">代碼</th>
                    <th scope="col">學校名稱</th>
                    <th scope="col">縣市別</th>
                    <th scope="col">地址</th>
                    <th scope="col">電話</th>
                    <th scope="col">狀態</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter"><span class="replaceA" ng-bind='record["C"]' ng-click="goDetail(record)"></span></td>
                      <td ng-bind='record["SN"]'></td>
                      <td class="aCenter" ng-bind='record["SC"]'></td>
                      <td ng-bind='getAddress(record)'></td>
                      <td class="aCenter" ng-bind='record["SP"]'></td>
                      <td class="aCenter" ng-bind='record["ES"]'></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var EnState = <%=EnStateJson%>;
        <%--var tbData = <%=tbData%>;--%>
    </script>
     <%:Scripts.Render("~/bundles/SchoolCode_JS")%>
</asp:Content>