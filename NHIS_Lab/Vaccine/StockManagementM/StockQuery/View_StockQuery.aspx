<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_StockQuery.aspx.cs" Inherits="Vaccine_StockManagementM_StockQuery_View_StockQuery" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="StockQueryApp" ng-controller="ViewStockQueryController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="send" ng-click="Return()" value="回上一頁"/>
            </div>
            <div class="formTb formTb6">
                <table>
                  <tr>
                    <th scope="row">疫苗名稱：</th>
                        <td >
                            <label ng-bind="VM.VaccineID"></label>
                        </td>
                    <th scope="row">批號：</th>
                        <td >
                            <label ng-bind="VM.BatchID"></label>
                        </td>
                    <th scope="row">批號類型：</th>
                        <td >
                            <label ng-bind="VM.BatchType"></label>
                        </td>
                    <th scope="row">每單位劑量：</th>
                        <td >
                            <label ng-bind="VM.DosePer"></label>
                        </td>
                </tr>
                <tr>
                    <th scope="row">有效日期：</th>
                        <td >
                            <label ng-bind="VM.AvaDate"></label>
                        </td>
                    <th scope="row">剩餘天數：</th>
                        <td >
                            <label ng-bind="VM.Remaining"></label>(天)
                        </td>
                    <th scope="row">總庫存量(瓶)：</th>
                        <td >
                            <label ng-bind="VM.Storage"></label>(瓶)
                        </td>
                    <th scope="row">總庫存量(劑)：</th>
                        <td >
                            <label ng-bind="VM.DoseStorage"></label>(劑)
                        </td>
                  </tr>
                </table>
            </div>    
         </form>
        <div class="function explain">
            <%if(PrintPower.HasPower){ %>
            <ul class="button_floatleft">
                <li style="border-style: hidden;"><a href="#" class="print" >列印</a></li>
            </ul>
            <%} %>
            <ul class="button_floatright">
              <li class="bluebg">效期提醒</li>
              <li class="orangebg">安全庫存量提醒</li>
            </ul>
         </div>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="5%">序號</th>
                    <th scope="col" width="25%">單位</th>
                    <th scope="col" width="10%">庫存量(劑)</th>
                    <th scope="col" width="10%">庫存量(瓶)</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index" ng-class="{'aCenter bluecolor' : record['c6']<0 ,'aCenter orangecolor' : record['c7']<0}" >
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c2"]'></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/StockQuery_JS")%>
</asp:Content> 

