<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_VaccineIn.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineIn_Login_VaccineIn" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineInApp" ng-controller="LoginVaccineInController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="send" ng-click="TransferVaccineIn()" value="回上一頁"/>
            </div>
            <div class="formTb formTb5">
                <table>
                  <tr>
                    <th scope="row">撥出日期：</th>
                        <td >
                            <label ng-bind="VM.DealDate"></label>
                        </td>
                    <th scope="row">撥出單位：</th>
                        <td >
                            <label ng-bind="VM.OutOrgName"></label>
                        </td>
                    <th scope="row">撥出總金額：</th>
                        <td >
                            <label ng-bind="VM.TotalCost"></label>
                        </td>
                    <th scope="row">類別：</th>
                        <td >
                            <label ng-bind="VM.DealTypeName"></label>
                        </td>
                  </tr>
                </table>
            </div>    
         </form>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="10%">疫苗<br>代號</th>
                    <th scope="col" width="10%">批號</th>
                    <th scope="col" width="10%">包裝<br>樣式</th>
                    <th scope="col" width="10%">劑量</th>
                    <th scope="col" width="10%">單價</th>
                    <th scope="col" width="10%">數量<br>(瓶)</th>
                    <th scope="col" width="10%">數量<br>(劑)</th>
                    <%if(NewPower.HasPower){ %>
                    <th scope="col" width="10%">撥入</th>
                    <th scope="col" width="10%">退回</th>
                    <%} %>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <td class="aCenter" ng-bind='record["c5"]'></td>
                    <td class="aCenter" ng-bind='record["c6"]'></td>
                    <td class="aCenter" ng-bind='record["c7"]'></td>
                    <td class="aCenter" ng-bind='record["c8"]'></td>
                    <td class="aCenter" ng-bind='record["c9"]'></td>
                    <%if(NewPower.HasPower){ %>
                    <td class="aCenter" ng-show="CheckStatus(record)==false?true:false" ng-bind="GetDealStatus(record)"></td>
                    <td class="aCenter" ng-show="CheckStatus(record)" ><a href="javascript:void(0);" ng-click="LoginConfirm(record)"><img src="/images/icon_maintain.png"/></a></td>
                    <td class="aCenter" ><input class="btn" type="button" value="退回" ng-click="Return(record)" ng-show="CheckStatus(record)"/></td>
                    <%} %>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var DealStatusData = <%=DealStatus %>;
     </script>
     <%:Scripts.Render("~/bundles/VaccineIn_JS")%>
</asp:Content> 
