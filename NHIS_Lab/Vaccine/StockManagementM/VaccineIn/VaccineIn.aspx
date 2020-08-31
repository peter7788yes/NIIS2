<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VaccineIn.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineIn_VaccineIn" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineInApp" ng-controller="VaccineInController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <%if(SearchPower.HasPower){ %>
                <input class="btn" type="submit" name="send" ng-click="TransferSearch()" value="查詢紀錄"/>
                <%} %>
            </div>
            <div class="formTb formTb6">
                <table>
                  <tr>
                    <td>
                        <input id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" disabled="disabled"/>
                    </td>
                  </tr>
                </table>
            </div>
         </form>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <%if(NewPower.HasPower && Check){ %>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="25%">撥出單位</th>
                    <th scope="col" width="10%">撥出日期</th>
                    <th scope="col" width="25%">疫苗種類</th>
                    <th scope="col" width="10%">總金額</th>
                    <th scope="col" width="10%">數量(瓶)</th>
                    <th scope="col" width="10%">登錄</th>
                    <%}else{ %>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="30%">撥出單位</th>
                    <th scope="col" width="10%">撥出日期</th>
                    <th scope="col" width="30%">疫苗種類</th>
                    <th scope="col" width="10%">總金額</th>
                    <th scope="col" width="10%">數量(瓶)</th>
                    <%} %>
                    
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <td class="aCenter" ng-bind='record["c5"]'></td>
                    <td class="aCenter" ng-bind='record["c6"]'></td>
                    <td class="aCenter" ng-bind='record["c7"]'></td>
                    <%if(NewPower.HasPower && Check){ %>
                    <td class="aCenter" >
                        <input class="btn" type="button" value="登錄" ng-click="TransferLogin(record)" />
                    </td>
                    <%} %>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var OrgData = '<%=OrgName %>';
     </script>
     <%:Scripts.Render("~/bundles/VaccineIn_JS")%>
</asp:Content> 