<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VaccineUse_BatchList.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineUse_VaccineUse_BatchList" MasterPageFile="~/MasterPage/MasterPage.master"%>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineUseApp" ng-controller="VaccineUseBatchListController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>領用疫苗：</th>
                        <td colspan="3">
                            <select ng-model="VM.VaccineSelect" ng-change="GetVaccineBatch()">
                            <option ng-repeat="option in VM.Vaccine" value="{{option.ID}}" ng-bind="option.VaccineName"></option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>    
         </form>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="5%">選擇</th>
                    <th scope="col" width="20%">批號</th>
                    <th scope="col" width="15%">有效日期</th>
                    <th scope="col" width="15%">包裝樣式</th>
                    <th scope="col" width="15%">庫存量</th>
                    <th scope="col" width="15%">單價</th>
                    <th scope="col" width="15%">批號類型</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ><input type="checkbox" ng-click="TransferNewListData(record)"/></td>
                    <td class="aCenter" ng-bind='record["c2"]'></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <td class="aCenter" ng-bind='record["c5"]'></td>
                    <td class="aCenter" ng-bind='record["c6"]'></td>
                    <td class="aCenter" ng-bind='record["c7"]'></td>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var VaccineData = <%=VaccineData %>;
     </script>
     <%:Scripts.Render("~/bundles/VaccineUse_JS")%>
</asp:Content> 