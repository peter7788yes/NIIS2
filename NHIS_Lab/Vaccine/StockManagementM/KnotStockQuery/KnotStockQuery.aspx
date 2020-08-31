<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KnotStockQuery.aspx.cs" Inherits="Vaccine_StockManagementM_KnotStockQuery_KnotStockQuery" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server" >
     <%=BodyClass %>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="KnotStockQueryApp" ng-controller="KnotStockQueryController" ng-cloak>
        <%if(SearchPower.HasPower){ %>
        <form name="form1" id="form1" >
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row"><span class="must">*</span>結存單位: </th>
                        <td>
                            <input id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" disabled="disabled"/>
                        </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>結存區間: </th>
                        <td>
                            <input id="StartDeal" ng-model="VM.StartDeal" type="text" name="StartDeal" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" onchange="SetSearchDate()" class="text02" required="required"/>
                            <a href="#"><img onclick="WdatePicker({el:'StartDeal',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png" /></a>
                            至
                            <input id="EndDeal" ng-model="VM.EndDeal" type="text" name="EndDeal" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" onchange="SetSearchDate()" class="text02" required="required"/>
                            <a href="#"><img onclick="WdatePicker({el:'EndDeal',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png" /></a>
                        </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>疫苗: </th>
                        <td>
                            <div style="float:left">
                            <select  ng-model="VM.SelectVaccine" name="SelectVaccine" size="5" style="width:250px;" multiple="multiple" required="required">
                               <option ng-repeat="option in VM.Vaccine" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                            </div>
                            <span class="wordred">(複選)</span>
                            <div>
                                <br/>
                                <input ng-click="AllSelect()" type="button" value="全選" class="btn" /><br/>
                                <input ng-click="AllCancel()" type="button" value="取消選取" class="btn" />
                            </div>
                        </td>
                  </tr>
                </table>
            </div>
            <div class="formBtn ">
                <input class="btn" type="button" ng-click="Search(1)" name="send" value="查詢"/>
            </div>   
         </form>
         <%} %>
         <%if(PrintPower.HasPower){ %>
         <div class="function">
            <ul>
              <li><a href="#" class="print">列印</a></li>
            </ul>
        </div>
        <%} %>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">疫苗<br/>名稱</th>
                    <th scope="col">疫苗<br/>批號</th>
                    <th scope="col">包裝<br/>樣式</th>
                    <th scope="col">有效<br/>期限</th>
                    <th scope="col">期間前<br/>結存</th>
                    <th scope="col">撥出</th>
                    <th scope="col">撥入</th>
                    <th scope="col">領用</th>
                    <th scope="col">損毀</th>
                    <th scope="col">退貨</th>
                    <th scope="col">期間後<br/>結存</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c2"]'></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <td class="aCenter" ng-bind='record["c5"]'></td>
                    <td class="aCenter" ng-bind='record["c6"]'></td>
                    <td class="aCenter" ng-bind='record["c7"]'></td>
                    <td class="aCenter" ng-bind='record["c8"]'></td>
                    <td class="aCenter" ng-bind='record["c9"]'></td>
                    <td class="aCenter" ng-bind='record["c10"]'></td>
                    <td class="aCenter" ng-bind='record["c11"]'></td>
                    <td class="aCenter" ng-bind='record["c12"]'></td>
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
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/KnotStockQuery_JS")%>
</asp:Content> 
    

