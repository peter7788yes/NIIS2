<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_VaccineOutDataList.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineOut_New_VaccineOutDataList" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineOutApp" ng-controller="NewVaccineOutDataListController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <%if(NewPower.HasPower){ %>
                <input class="btn" type="submit" name="send" ng-click="ConfirmVaccineOut()" value="確認"/>
                <%} %>
                <input class="btn" type="submit" name="send" ng-click="TransferVaccineOut()" value="取消"/>
            </div>
            <div class="formTb">
              <table>
                <tr>
                  <th scope="row"><span class="must">*</span>撥出日期：</th>
                  <td colspan="3">
                      <label ng-bind="VM.DealDate" ></label>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>撥出單位：</th>
                  <td colspan="3">
                      <label ng-bind="VM.OutOrgName" ></label>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>撥入單位：</th>
                  <td colspan="3">
                      <label ng-bind="VM.InOrgName" ></label>
                  </td>
                </tr>
                <tr>
                  <th scope="row">總金額：</th>
                  <td colspan="3">
                    <label ng-bind="CountTotalCost(VM.TotalCost)" ></label>
                  </td>
                </tr>
                <tr>
                  <th scope="row">備註：</th>
                  <td colspan="3">
                    <label ng-bind="VM.Remark" ></label>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>撥出類別：</th>
                  <td>
                      <label ng-bind="VM.DealType" ></label>
                      <label ng-bind="VM.DealHospitalName" ></label>
                  </td>
                </tr>
              </table>
            </div>
         </form>
         <%if(NewPower.HasPower){ %>
         <div class="function">
            <input class="btn" type="submit" name="send" ng-click="TransferNewList()" value="新增撥出疫苗"/>
         </div>
         <%} %>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">疫苗<br>代號</th>
                    <th scope="col">批號</th>
                    <th scope="col">包裝<br>樣式</th>
                    <th scope="col">劑量</th>
                    <th scope="col">單價</th>
                    <th scope="col">數量<br>(瓶)</th>
                    <th scope="col">數量<br>(劑)</th>
                    <th scope="col">冷凍片<br>指數</th>
                    <th scope="col">監視片<br>原片指數</th>
                    <th scope="col">監視片<br>點收指數</th>
                    <%if(DeletePower.HasPower){ %>
                    <th scope="col">刪除</th>
                    <%} %>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ><a href="javascript:void(0);" ng-bind='record["c1"]' ng-click="TransferModifyListData(record)"></a></td>
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
                    <%if(DeletePower.HasPower){ %>
                    <td class="aCenter" ><a href="javascript:void(0);" ng-click="DeleteBatch(record,$index)"><img src="/images/icon_del01.gif" alt="刪除"/></a></td>
                    <%} %>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/VaccineOut_JS")%>
</asp:Content> 
