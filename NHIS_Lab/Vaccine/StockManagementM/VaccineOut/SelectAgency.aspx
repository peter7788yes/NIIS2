<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAgency.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineOut_SelectAgency" MasterPageFile="~/MasterPage/MasterPage.master" %>
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

<section class="Content3" ng-app="MyApp" ng-controller="MyController" ng-cloak>
<h2>選擇醫療機構</h2>
         <br/>
        <div class="formTb">
                <table>
                    <tr>
                       <th scope="row">縣市:</th>
                        <td>
                            <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                                 <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">鄉/鎮/區:</th>
                        <td>
                            <select id="SelectTown" ng-model="VM.SelectTown">
                                   <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                         </td>
                    </tr>
                      <tr>
                        <th scope="row">接種單位:</th>
                        <td>
                            <input type="text" value=""  ng-model="VM.AN" />
                            <input type="button" value="查詢" ng-click="changePage(1)" class="btn" />
                         </td>
                    </tr>
                    </table>
        </div>

        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col">選取</th>
                    <th scope="col">醫事機構代碼</th>
                    <th scope="col">地區</th>
                    <th scope="col">醫事機構名稱</th>
                    <th scope="col">狀態</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter"><input type="button" value="選取" ng-click="close(record)" class="btn" /></td>
                      <td class="aCenter" ng-bind='record["C"]'></td>
                      <td class="aCenter" ng-bind='record["Z"]'></td>
                      <td class="aCenter" ng-bind='record["AN"]'></td>
                      <td class="aCenter" ng-bind='record["AS"]'></td>
                </tr>
            </table>
        </div>
  
</section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/VaccineOut_JS")%>
</asp:Content>