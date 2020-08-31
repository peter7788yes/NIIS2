<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockQuery.aspx.cs" Inherits="Vaccine_StockManagementM_StockQuery_StockQuery" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="StockQueryApp" ng-controller="StockQueryController" ng-cloak>
        <form name="form1" id="form1" >
            <%if(SearchPower.HasPower){ %>
            <div class="formTb formTb6">
                <table>
                  <tr>
                    <th scope="row">所屬單位：</th>
                    <td colspan="3">
                        <input id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" disabled="disabled"/>
                    </td>
                  </tr>
                  <tr>
                  <th scope="row">疫苗：</th>
                  <td colspan="3">
                    <select ng-model="VM.SelectVaccineID" ng-change="Search(1)">
                    <option ng-selected="option.EV==VM.SelectVaccineID" ng-repeat="option in VM.VaccineID" value="{{option.EV}}" ng-bind="option.EN"></option>
                    </select>
                  </td>
                </tr>
                </table>
            </div>
            <%} %>
         </form>          
         <div class="function explain">
            <%if(PrintPower.HasPower){ %>
            <ul class="button_floatleft">
                <li style="border-style: hidden;"><a href="#" class="print" >列印</a></li>
            </ul>
            <%} %>
            <ul class="button_floatright">
              <li class="redbg" >已屆效</li>
            </ul>
         </div>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="5%">序號</th>
                    <th scope="col" width="25%">疫苗名稱</th>
                    <th scope="col" width="15%">批號</th>
                    <th scope="col" width="15%">批號類型</th>
                    <th scope="col" width="10%">包裝樣式</th>
                    <th scope="col" width="10%">劑量</th>
                    <th scope="col" width="10%">庫存量(劑)</th>
                    <th scope="col" width="10%">庫存量(瓶)</th>
                    <th scope="col" width="10%">有效日期</th>
                    <th scope="col" width="10%">剩餘天數</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index" ng-class="{'aCenter redcolor' : record['c11']<0}">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td ng-bind='record["c3"]'></td>
                    <td class="aCenter" ><a href="javascript:void(0);" ng-bind='record["c4"]' ng-click="VeiwStockQuery(record)"></a></td>
                    <td class="aCenter" ng-bind='record["c5"]'></td>
                    <td class="aCenter" ng-bind='record["c6"]'></td>
                    <td class="aCenter" ng-bind='record["c7"]'></td>
                    <td class="aCenter" ng-bind='record["c8"]'></td>
                    <td class="aCenter" ng-bind='record["c9"]'></td>
                    <td class="aCenter" ng-bind='record["c10"]'></td>
                    <td class="aCenter" ng-bind='record["c11"]'></td>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var VaccineData = <%=Vaccine %>;
         var OrgData = '<%=OrgName %>';
     </script>
     <%:Scripts.Render("~/bundles/StockQuery_JS")%>
</asp:Content> 
