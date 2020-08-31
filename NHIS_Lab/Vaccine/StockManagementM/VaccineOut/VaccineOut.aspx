<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VaccineOut.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineOut_VaccineOut" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineOutApp" ng-controller="VaccineOutController" ng-cloak>
        <form name="form1" id="form1" >
            <%if(NewPower.HasPower){ %>
            <div class="formBtn formBtnleft">
                <input class="btn" type="button" name="send" ng-click="TransferNewData()" value="新增登錄"/>
            </div>
            <%} %>
            <%if(SearchPower.HasPower){ %>
            <div class="formTb">
              <table>
                <tr>
                  <th scope="row">撥出日期：</th>
                  <td colspan="3">
                      <input id="StartDeal" ng-model="VM.StartDeal" type="text" name="StartDeal" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" onchange="SetSearchDate()" class="text02" required="required"/>
                      <a href="#"><img onclick="WdatePicker({el:'StartDeal',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>至
                      <input id="EndDeal" ng-model="NVMD.EndDeal" type="text" name="EndDeal" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" onchange="SetSearchDate()" class="text02" required="required"/>
                      <a href="#"><img onclick="WdatePicker({el:'EndDeal',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                </tr>
                <tr>
                  <th scope="row">撥出單位：</th>
                  <td colspan="3">
                      <input id="OutOrgName" ng-model="VM.OutOrgName" name="OutOrgName" type="text" value="" class="text03" disabled="disabled"/>
                </tr>
                <tr>
                  <th scope="row">撥入單位：</th>
                  <td colspan="3">
                      <input id="InOrgName" ng-model="VM.InOrgName" name="InOrgName" type="text" value="" class="text03" ng-click="openOrgs(2)" readonly="true"/>
                      <input id="InOrgID" ng-model="VM.InOrgID" name="InOrgID" type="hidden" />
                      <input id="InOrgType" ng-model="VM.InOrgType" name="InOrgType" type="hidden" />
                      <img  src="/images/location.png" ng-click="openOrgs(2)" />
                      <a href="javascript:void(0);" id="InrefreshBtn" ng-click="Inrefresh()"></a>
                      <span class="wordred">(複選)</span></td>
                </tr>
                <tr>
                  <th scope="row">工作人員：</th>
                  <td colspan="3">
                    <select ng-model="VM.SelectStaff">
                    <option ng-selected="option.EV==VM.SelectStaff" ng-repeat="option in VM.Staff" value="{{option.EV}}" ng-bind="option.EN"></option>
                    </select>
                  </td>
                </tr>
                <tr>
                  <th scope="row">疫苗：</th>
                  <td colspan="3">
                    <select ng-model="VM.SelectVaccineID" ng-change="GetBDataByOrg()">
                    <option ng-selected="option.EV==VM.SelectVaccineID" ng-repeat="option in VM.VaccineID" value="{{option.EV}}" ng-bind="option.EN"></option>
                    </select>
                  </td>
                </tr>
                <tr>
                  <th scope="row">批號類型：</th>
                  <td >
                    <select ng-model="VM.SelectBatchType" ng-change="GetBDataByOrg()">
                    <option ng-selected="option.EV==VM.SelectBatchType" ng-repeat="option in VM.BatchType" value="{{option.EV}}" ng-bind="option.EN"></option>
                    </select>
                  </td>
                  <th scope="row">批號：</th>
                  <td >
                    <select ng-model="VM.SelectBatchID">
                    <option ng-selected="option.EV==VM.SelectBatchID" ng-repeat="option in VM.BatchID" value="{{option.EV}}" ng-bind="option.EN"></option>
                    </select>
                  </td>
                </tr>
                <tr>
                  <th scope="row">處理狀態：</th>
                  <td colspan="3">
                    <select ng-model="VM.SelectDealStatus">
                    <option ng-selected="option.EV==VM.SelectDealStatus" ng-repeat="option in VM.DealStatus" value="{{option.EV}}" ng-bind="option.EN"></option>
                    </select>
                  </td>
                </tr>
                <tr>
                  <th scope="row">排序方式：</th>
                  <td colspan="3"><label for="">
                      <input ng-model="VM.Sort" name="" type="radio" value="1"/>撥出日期</label>
                    <label for="">
                      <input ng-model="VM.Sort" name="" type="radio" value="2"/>撥出單位</label></td>
                </tr>
              </table>
            </div>
            <div class="formBtn">
                <input class="btn" type="button" name="send" ng-click="Search(1)" value="查詢"/>
                <input class="btn" type="button" name="send" ng-click="ClearSearch()" value="清空"/>
            </div>
            <%} %>
         </form>
         <div class="function">
            <ul>
                <%if(PrintPower.HasPower){ %>
                <li>
                    <a href="#" class="print">列印</a>
                </li>
                <%} %>
                <%if(DownloadPower.HasPower){ %>
                <li>
                    <a href="#" class="export">匯出(csv)</a>
                </li>
                <%} %>
            </ul>
         </div>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">撥出<br/>日期</th>
                    <th scope="col">撥入<br/>單位</th>
                    <th scope="col">工作<br/>人員</th>
                    <th scope="col">疫苗<br/>代號</th>
                    <th scope="col">批號</th>
                    <th scope="col">類別</th>
                    <th scope="col">包裝<br/>樣式</th>
                    <th scope="col">單價</th>
                    <th scope="col">數量<br/>(瓶)</th>
                    <th scope="col">數量<br/>(劑)</th>
                    <th scope="col">冷凍片<br/>指數</th>
                    <th scope="col">原監視片<br/>原片指數</th>
                    <th scope="col">監視片<br/>點收指數</th>
                    <th scope="col">處理<br/>狀態</th>
                    <%if(DeletePower.HasPower){ %>
                    <th scope="col">刪除</th>
                    <%} %>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ><a href="javascript:void(0);" ng-bind='record["c1"]' ng-click="TransferModifyBatch(record)"></a></td>
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
                    <td class="aCenter" ng-bind='record["c13"]'></td>
                    <td class="aCenter" ng-bind='record["c14"]'></td>
                    <td class="aCenter" ng-bind='record["c15"]'></td>
                    <td class="aCenter" ng-show="CheckReturn(record)==false?true:false" ng-bind="GetDealStatus(record)"></td>
                    <td class="aCenter" ng-show="CheckReturn(record)" ><a href="javascript:void(0);" ng-bind="GetDealStatus(record)" ng-click="TransferBack(record)"></a></td>
                    <%if(DeletePower.HasPower){ %>
                    <td class="aCenter" ng-show="CheckModify(record)"><a href="javascript:void(0);" ng-click="DeleteBatch(record,$index)"><img src="/images/icon_del01.gif" alt="刪除"/></a></td>
                    <td class="aCenter" ng-show="CheckModify(record)==true?false:true" ng-bind="VM.DeleteWord"></td>
                    <%} %>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var BatchTypeData = <%=BatchType %>;
         var DealStatusData = <%=DealStatus %>;
         var OrgData = '<%=OrgName %>';
     </script>
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/VaccineOut_JS")%>
</asp:Content> 
